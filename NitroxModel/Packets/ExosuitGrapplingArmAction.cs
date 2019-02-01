using System;
using UnityEngine;

namespace NitroxModel.Packets
{
    [Serializable]
    public class ExosuitGrapplingAction : Packet
    {
        public string Guid { get; }

        public ExosuitGrapplingAction(string guid)
        {
            Guid = guid;
        }

        public override string ToString()
        {
            return "[ExosuitModulesAction - Guid:" + Guid + " ]";
        }
    }
}
