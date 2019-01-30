using System;
using UnityEngine;

namespace NitroxModel.Packets
{
    [Serializable]
    public class ExosuitTorpedoAction : Packet
    {
        public TechType TorpedoTechType { get; }
        public string Guid { get; }
        public bool Verbose { get; }

        public ExosuitTorpedoAction(TechType torpedoTechType, string guid, bool verbose)
        {
            TorpedoTechType = torpedoTechType;
            Guid = guid;
            Verbose = verbose;
        }

        public override string ToString()
        {
            return "[ExosuitModulesAction - torpedoTechType: " + TorpedoTechType + " Guid:" + Guid + " Verbose: " + Verbose + " ]";
        }
    }
}
