using NitroxModel.DataStructures.Util;
using NitroxModel.Logger;
using NitroxModel.Packets;
using ProtoBuf;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NitroxModel.DataStructures.GameLogic
{
    [Serializable]
    [ProtoContract]
    public class Exosuit : VehicleModel
    {
        [ProtoMember(1)]
        public ExosuitTorpedoArm[] TorpedoArms { get; }

        [ProtoMember(2)]
        public string[] Guids { get; set; }

        [ProtoMember(3)]
        public string[] SlotIDs { get; set; }

        public Exosuit()
        {

        }

        public Exosuit(ExosuitTorpedoArm[] torpedoArms, string[] guids, string[] slotIDs)
        {
            TorpedoArms = torpedoArms;
            Guids = guids;
            SlotIDs = slotIDs;

        }
    }
}
