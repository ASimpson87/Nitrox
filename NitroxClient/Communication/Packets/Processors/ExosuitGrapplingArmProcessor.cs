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
        private static float torpedoStart = 0f;
        private static float torpedoCooldown = 5f;
        private static Timer animStopTimer;

        public ExosuitGrapplingArmProcessor(IPacketSender packetSender)
        {
            this.packetSender = packetSender;
        }
        public override void Process(ExosuitGrapplingAction packet)
        {
            using (packetSender.Suppress<ExosuitTorpedoAction>())
            {

                Log.Info("SHOULDWORK");
            }
        }
    }
}
