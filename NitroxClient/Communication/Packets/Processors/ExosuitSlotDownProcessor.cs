using System.Reflection;
using NitroxClient.Communication.Abstract;
using NitroxClient.Communication.Packets.Processors.Abstract;
using NitroxClient.GameLogic.Helper;
using NitroxModel.DataStructures.Util;
using NitroxModel.Helper;
using NitroxModel.Logger;
using NitroxModel.Packets;
using UnityEngine;

namespace NitroxClient.Communication.Packets.Processors
{
    public class ExosuitSlotDownProcessor : ClientPacketProcessor<ExosuitSlotDownAction>
    {
        private readonly IPacketSender packetSender;

        public ExosuitSlotDownProcessor(IPacketSender packetSender)
        {
            this.packetSender = packetSender;
        }
        public override void Process(ExosuitSlotDownAction packet)
        {
            using (packetSender.Suppress<ExosuitClawAction>())
            {
                GameObject _gameObject = GuidHelper.RequireObjectFrom(packet.Guid);
                Exosuit exosuit = _gameObject.GetComponent<Exosuit>();
                Vehicle vehicle = exosuit.GetComponent<Vehicle>();

                exosuit.ReflectionSet("rightButtonDownProcessed", true);

                int slotIndex = exosuit.GetSlotIndex("ExosuitArmRight");
                if (!exosuit.IsPowered())
                {
                    return;
                }
                float coolDown = (float)exosuit.ReflectionCall("GetQuickSlotCooldown", false, false, new object[] { slotIndex });
                if (coolDown != 1f)
                {
                    return;
                }
                TechType techType;
                float num;
                QuickSlotType QuickSlotType = (QuickSlotType)exosuit.ReflectionCall("GetQuickSlotType", false, false, new object[] { slotIndex });
                if (QuickSlotType == QuickSlotType.Selectable)
                {
                    Player.main.playerAnimator.SetBool("exosuit_use_right", true);
                    this.mainAnimator.SetBool("use_tool_right", true);
                    this.quickSlotTimeUsed[slotIndex] = Time.time;
                    this.quickSlotCooldown[slotIndex] = num;
                }
            }
        }
    }
}
