﻿using NitroxModel.Helper;
using NitroxModel.Logger;
using UnityEngine;

namespace NitroxClient.MonoBehaviours
{
    class MultiplayerExosuit : MultiplayerVehicleControl<Vehicle>
    {
        private bool lastThrottle;
        private float timeJetsChanged;
        private Exosuit exosuit;

        protected override void Awake()
        {
            SteeringControl = exosuit = GetComponent<Exosuit>();
            ArmPositions = exosuit = GetComponent<Exosuit>();
            base.Awake();
        }

        internal override void Enter()
        {
            GetComponent<Rigidbody>().freezeRotation = false;
            exosuit.ReflectionCall("SetIKEnabled", false, false, new object[] { true });
            exosuit.ReflectionSet("thrustIntensity", 0f);
            exosuit.ambienceSound.Play();
            
            base.Enter();
        }

        internal override void Exit()
        {
            GetComponent<Rigidbody>().freezeRotation = true;
            exosuit.ReflectionCall("SetIKEnabled", false, false, new object[] { false });
            exosuit.loopingJetSound.Stop();
            exosuit.fxcontrol.Stop(0);
            exosuit.ambienceSound.Stop();
            base.Exit();
        }

        internal override void SetThrottle(bool isOn)
        {
            if (timeJetsChanged + 3f <= Time.time && lastThrottle != isOn)
            {
                timeJetsChanged = Time.time;
                lastThrottle = isOn;
                if (isOn)
                {
                    exosuit.loopingJetSound.Play();
                    exosuit.fxcontrol.Play(0);
                }
                else
                {
                    exosuit.loopingJetSound.Stop();
                    exosuit.fxcontrol.Stop(0);
                }
            }
        }

        internal override void SetArmPositions(Vector3 leftArmPosition, Vector3 rightArmPosition)
        {
            base.SetArmPositions(leftArmPosition, rightArmPosition);

            Transform leftAim = (Transform)exosuit.ReflectionGet("aimTargetLeft", true);
            Transform rightAim = (Transform)exosuit.ReflectionGet("aimTargetRight", true);
            leftAim.position = leftArmPosition;
            rightAim.position = rightArmPosition;
            exosuit.ReflectionSet("aimTargetLeft", leftAim, true);
            exosuit.ReflectionSet("aimTargetRight", rightAim, true);
        }
    }
}
