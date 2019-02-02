﻿using System.Reflection;
using System.Timers;
using NitroxClient.Communication.Abstract;
using NitroxClient.Communication.Packets.Processors.Abstract;
using NitroxClient.GameLogic.Helper;
using NitroxModel.DataStructures.Util;
using NitroxModel.Helper;
using NitroxModel.Logger;
using NitroxModel.Packets;
using UnityEngine;



namespace NitroxClient.Communication.Packets.Processors
{
    public class ExosuitTorpedoActionProcessor : ClientPacketProcessor<ExosuitTorpedoAction>
    {
        private readonly IPacketSender packetSender;
        private static float torpedoStart = 0f;
        private static float torpedoCooldown = 5f;
        private static Timer animStopTimer;

        public ExosuitTorpedoActionProcessor(IPacketSender packetSender)
        {
            this.packetSender = packetSender;
        }
        public override void Process(ExosuitTorpedoAction packet)
        {
            using (packetSender.Suppress<ExosuitTorpedoAction>())
            {
                // Copied from TryShoot
                GameObject _gameObject = GuidHelper.RequireObjectFrom(packet.Guid);
                ExosuitTorpedoArm torpedoArm =_gameObject.GetComponent<ExosuitTorpedoArm>();
                Exosuit exosuit = torpedoArm.GetComponentInParent<Exosuit>();

                ItemsContainer storageInSlot = (ItemsContainer)torpedoArm.ReflectionGet("container");
                TorpedoType torpedoType = null;
                for (int i = 0; i < exosuit.torpedoTypes.Length; i++)
                {
                    if (storageInSlot.Contains(exosuit.torpedoTypes[i].techType))
                    {
                        torpedoType = exosuit.torpedoTypes[i];
                        break;
                    }
                }
                
                float timeFirstShot = (float)torpedoArm.ReflectionGet("timeFirstShot");
                float timeSecondShot = (float)torpedoArm.ReflectionGet("timeSecondShot");
                float num = Mathf.Clamp(Time.time - timeFirstShot, 0f, 5f);
                float num2 = Mathf.Clamp(Time.time - timeSecondShot, 0f, 5f);
                float b = 5f - num;
                float b2 = 5f - num2;
                float num3 = Mathf.Min(num, num2);
                bool shotCompleted = false;
                if (num >= 5f)
                {
                    if (TorpedoShot(storageInSlot, torpedoArm, torpedoType, torpedoArm.siloFirst, packet.Forward, packet.Rotation))
                    {
                        torpedoArm.ReflectionSet("timeFirstShot", Time.time);
                        shotCompleted = true;
                    }
                }
                else
                {
                    if (TorpedoShot(storageInSlot, torpedoArm, torpedoType, torpedoArm.siloSecond, packet.Forward, packet.Rotation))
                    {
                        torpedoArm.ReflectionSet("timeSecondShot", Time.time);
                        shotCompleted = true;
                    }
                }
                if(!shotCompleted)
                {

                    torpedoArm.animator.SetBool("use_tool", false);
                }
            }
        }

        //Copied this from the Vehicle class
        public static bool TorpedoShot(ItemsContainer container, ExosuitTorpedoArm torpedoArm, TorpedoType torpedoType, Transform muzzle, Vector3 forward, Quaternion rotation)
        {
            if (torpedoType != null && container.DestroyItem(torpedoType.techType))
            {
                if (Time.time > torpedoStart + torpedoCooldown)
                {
                    torpedoStart = Time.time;
                    SetAnimTimer(torpedoArm);
                    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(torpedoType.prefab);
                    Transform component = gameObject.GetComponent<Transform>();
                    SeamothTorpedo component2 = gameObject.GetComponent<SeamothTorpedo>();
                    Vector3 zero = Vector3.zero;
                    Rigidbody componentInParent = muzzle.GetComponentInParent<Rigidbody>();
                    Vector3 rhs = (!(componentInParent != null)) ? Vector3.zero : componentInParent.velocity;
                    float speed = Vector3.Dot(forward, rhs);
                    component2.Shoot(muzzle.position, rotation, speed, -1f);
                    return true;
                }
            }
            return false;
        }

        // Use timers to control animations instead of the OnUseUp method, Reduces need for extra patches.
        private static void SetAnimTimer(ExosuitTorpedoArm torpedoArm)
        {
            // Create a timer with a two second interval.
            animStopTimer = new Timer(2000);
            // Hook up the Elapsed event for the timer. 

            animStopTimer.Elapsed += delegate { OnTimedEvent(torpedoArm); };
            animStopTimer.AutoReset = false;
            animStopTimer.Enabled = true;
        }

        private static void OnTimedEvent(ExosuitTorpedoArm torpedoArm)
        {
            torpedoArm.animator.SetBool("use_tool", false);

        }

    }
}