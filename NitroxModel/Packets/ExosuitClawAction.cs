using System;
using UnityEngine;

namespace NitroxModel.Packets
{
    [Serializable]
    public class ExosuitClawAction : Packet
    {
        public string Guid { get; }

        public ExosuitClawAction(string guid)
        {
            Guid = guid;
        }

        public override string ToString()
        {
            return "[ExosuitModulesAction - Guid: " + Guid + "]";
        }
    }
}
