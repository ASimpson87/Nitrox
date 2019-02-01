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
        public Vector3 Forward { get; }
        public Quaternion Rotation { get; }

        public ExosuitTorpedoAction(TechType torpedoTechType, string guid, bool verbose, Vector3 forward, Quaternion rotation)
        {
            TorpedoTechType = torpedoTechType;
            Guid = guid;
            Verbose = verbose;
            Forward = forward;
            Rotation = rotation;
        }

        public override string ToString()
        {
            return "[ExosuitModulesAction - torpedoTechType: " + TorpedoTechType + " Guid:" + Guid + " Verbose: " + Verbose + "Forward: " + Forward + "Rotation: " + Rotation + " ]";
        }
    }
}
