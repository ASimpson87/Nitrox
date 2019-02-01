using System;
using UnityEngine;

namespace NitroxModel.Packets
{
    [Serializable]
    public class ExosuitGrapplingAction : Packet
    {
        public string Guid { get; }
        public bool Start { get; }

        public ExosuitGrapplingAction(string guid, bool start)
        {
            Guid = guid;
            Start = start;
        }

        public override string ToString()
        {
            return "[ExosuitModulesAction - Guid:" + Guid + "Start: " + Start +" ]";
        }
    }
}
