using System;
using UnityEngine;

namespace NitroxModel.Packets
{
    [Serializable]
    public class ExosuitSlotDownAction : Packet
    {
        public string Guid { get; }
        public bool IsRight { get; }

        public ExosuitSlotDownAction(string guid, bool isRight)
        {
            Guid = guid;
            IsRight = isRight;
        }

        public override string ToString()
        {
            return "[ExosuitSlotDownAction - Guid: " + Guid + "IsRight:" + IsRight + "]";
        }
    }
}
