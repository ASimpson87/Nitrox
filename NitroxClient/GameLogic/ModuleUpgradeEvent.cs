using NitroxClient.Communication.Abstract;
using NitroxClient.GameLogic.Helper;
using NitroxClient.Unity.Helper;
using NitroxModel.Helper;
using NitroxModel.Logger;
using NitroxModel.Packets;
using UnityEngine;

namespace NitroxClient.GameLogic
{
    public class ModuleUpgradeEvent
    {
        private readonly IPacketSender packetSender;

        public ModuleUpgradeEvent(IPacketSender packetSender)
        {
            this.packetSender = packetSender;
        }

        public void BroadcastModuleChanged(Exosuit __instance)
        {
        }
    }
}
