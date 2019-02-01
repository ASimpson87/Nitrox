using System.Reflection;
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
    public class ExosuitGrapplingArmProcessor : ClientPacketProcessor<ExosuitGrapplingAction>
    {
        private readonly IPacketSender packetSender;

        public ExosuitGrapplingArmProcessor(IPacketSender packetSender)
        {
            this.packetSender = packetSender;
        }
        public override void Process(ExosuitGrapplingAction packet)
        {
            using (packetSender.Suppress<ExosuitGrapplingAction>())
            {
                GameObject _gameObject = GuidHelper.RequireObjectFrom(packet.Guid);
                ExosuitGrapplingArm grapplingArm = _gameObject.GetComponent<ExosuitGrapplingArm>();
                Exosuit exosuit = grapplingArm.GetComponentInParent<Exosuit>();
                if (packet.Start)
                {
                    grapplingArm.animator.SetBool("use_tool", true);
                    if (!grapplingArm.rope.isLaunching)
                    {
                        grapplingArm.rope.LaunchHook(35f);
                    }
                }
                else
                {
                    grapplingArm.animator.SetBool("use_tool", false);
                    grapplingArm.ReflectionCall("ResetHook");
                }
            }
        }
    }
}
