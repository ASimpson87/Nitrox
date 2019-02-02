using System;
using UnityEngine;

namespace NitroxModel.Packets
{
    [Serializable]
    public class ExosuitGrapplingHit : Packet
    {
        public string Guid { get; }
        public Vector3 Position { get; }
        public Vector3 Forward { get; }

        public ExosuitGrapplingHit(string guid, Vector3 position, Vector3 forward)
        {
            Guid = guid;
            Position = position;
            Forward = forward;
        }

        public override string ToString()
        {
            return "[ExosuitModulesAction - Guid:" + Guid + "Position:" + Position + "Forward: " + Forward + " ]";
        }
    }
}
