using System.Collections.Generic;
using NitroxClient.Communication.Abstract;
using NitroxClient.Communication.Packets.Processors.Abstract;
using NitroxClient.GameLogic.Helper;
using NitroxModel.DataStructures.GameLogic;
using NitroxModel.Logger;
using NitroxModel.Packets;
using UnityEngine;

namespace NitroxClient.Communication.Packets.Processors
{
    public class VehicleModuleChangeProcessor : ClientPacketProcessor<VehicleUpgradeModuleChange>
    {
        private readonly IPacketSender packetSender;

        public VehicleModuleChangeProcessor(IPacketSender packetSender)
        {
            this.packetSender = packetSender;
        }
        public override void Process(VehicleUpgradeModuleChange packet)
        {
            using (packetSender.Suppress<VehicleUpgradeModuleChange>())
            {
                GameObject _gameObject = GuidHelper.RequireObjectFrom(packet.VehicleGuid);
                
                List<InteractiveChildObjectIdentifier> childIdentifiers = VehicleChildObjectIdentifierHelper.ExtractGuidsOfInteractiveChildren(_gameObject);
                VehicleChildUpdate vehicleChildInteractiveData = new VehicleChildUpdate(packet.VehicleGuid, childIdentifiers);
                packetSender.Send(vehicleChildInteractiveData);
            }
        }
    }
}
