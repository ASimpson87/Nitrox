using System;

namespace NitroxModel.Packets
{
    [Serializable]
    public class VehicleUpgradeModuleChange : Packet
    {
        public string VehicleGuid { get; }
        public int SlotID { get; }
        public TechType TechType;
        public bool Added { get; }

        public VehicleUpgradeModuleChange(string vehicleGuid, int slotID, TechType techType, bool added)
        {
            VehicleGuid = vehicleGuid;
            SlotID = slotID;
            Added = added;
            TechType = techType;
        }

        public override string ToString()
        {
            return "[UpgradeModuleChanged - VehicleGuid: " + VehicleGuid + " SlotID: " + SlotID + " TechType: " + TechType + " Added: " + Added + "]";
        }
    }
}
