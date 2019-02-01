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
    public class ExosuitClawActionProcessor : ClientPacketProcessor<ExosuitClawArmAction>
    {
        private readonly IPacketSender packetSender;

        public ExosuitClawActionProcessor(IPacketSender packetSender)
        {
            this.packetSender = packetSender;
        }
        public override void Process(ExosuitClawArmAction packet)
        {
            using (packetSender.Suppress<ExosuitClawArmAction>())
            {

                GameObject _gameObject = GuidHelper.RequireObjectFrom(packet.Guid);
                ExosuitClawArm clawArm = _gameObject.GetComponent<ExosuitClawArm>();

                Exosuit exosuit = clawArm.GetComponentInParent<Exosuit>();

                Hit(exosuit, clawArm);

            }
        }

        private bool Hit(Exosuit exosuit, ExosuitClawArm leftClawArm)
        {
                Pickupable pickupable = null;
                PickPrefab x = null;
                if (exosuit.GetActiveTarget())
                {
                    pickupable = exosuit.GetActiveTarget().GetComponent<Pickupable>();
                    x = exosuit.GetActiveTarget().GetComponent<PickPrefab>();
                }
                if (pickupable != null && pickupable.isPickupable)
                {
                    if (exosuit.storageContainer.container.HasRoomFor(pickupable))
                    {
                    leftClawArm.animator.SetTrigger("use_tool");
                    TechType pickedUpTechType = pickupable.GetTechType();
                    string pickedUpGuid = pickupable.gameObject.GetGuid();
                    Vector3 pickedUpPosition = pickupable.transform.position;
                    PickupItem pickedUp = new PickupItem(pickedUpPosition, pickedUpGuid, pickedUpTechType);
                    Log.Info("PICKED UP ITEM: " + pickedUpTechType + " " + pickedUpPosition + " " + pickedUpGuid);
                        return true;
                    }
                }
                else
                {
                    if (x != null)
                    {
                    leftClawArm.animator.SetTrigger("use_tool");
                        return true;
                    }
                    leftClawArm.animator.SetTrigger("bash");
                    leftClawArm.fxControl.Play(0);
                    return true;
            }
            return false;
        }
    }
}
