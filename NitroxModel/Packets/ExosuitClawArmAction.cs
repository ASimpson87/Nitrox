using System;
using UnityEngine;

namespace NitroxModel.Packets
{
    [Serializable]
    public class ExosuitClawArmAction : Packet
    {
        public string Guid { get; }

        public ExosuitClawArmAction(string guid)
        {
            Guid = guid;
        }

        public override string ToString()
        {
            return "[ExosuitModulesAction - Guid: " + Guid + "]";
        }
    }
}
