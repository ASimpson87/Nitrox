using NitroxClient.Communication.Abstract;
using NitroxClient.GameLogic.Helper;
using NitroxModel.Helper;
using NitroxModel.Logger;
using NitroxModel.Packets;
using UnityEngine;

namespace NitroxClient.GameLogic
{
    public class ExosuitModuleEvent
    {
        private readonly IPacketSender packetSender;

        public ExosuitModuleEvent(IPacketSender packetSender)
        {
            this.packetSender = packetSender;
        }

        public void BroadcastGrapplingHookUse(ExosuitGrapplingArm grapplingArm, bool isStarting)
        {
            if (!string.IsNullOrEmpty(grapplingArm.gameObject.GetGuid()))
            {
                string Guid = GuidHelper.GetGuid(grapplingArm.gameObject);
                ExosuitGrapplingAction Changed = new ExosuitGrapplingAction(Guid, isStarting);
               packetSender.Send(Changed);
            }
        }

        public void BroadcastGrapplingHookHit(ExosuitGrapplingArm grapplingArm)
        {
            if (!string.IsNullOrEmpty(grapplingArm.gameObject.GetGuid()))
            {
                string Guid = GuidHelper.GetGuid(grapplingArm.gameObject);
                ExosuitGrapplingHit Changed = new ExosuitGrapplingHit(Guid, MainCamera.camera.transform.position, MainCamera.camera.transform.forward);
                packetSender.Send(Changed);
            }
        }

        public void BroadcastTorpedoLaunch(ExosuitTorpedoArm torpedoArm, TorpedoType torpedoType, Transform siloTransform, bool verbosed)
        {
            if (!string.IsNullOrEmpty(torpedoArm.gameObject.GetGuid()))
            {
                
                string Guid = GuidHelper.GetGuid(torpedoArm.gameObject);
                ExosuitTorpedoAction Changed = new ExosuitTorpedoAction(torpedoType.techType, Guid, verbosed, Player.main.camRoot.GetAimingTransform().forward, Player.main.camRoot.GetAimingTransform().rotation);
                packetSender.Send(Changed);
            }
        }

        public void BroadcastClawArmUse(ExosuitClawArm clawArm)
        {
            if (!string.IsNullOrEmpty(clawArm.gameObject.GetGuid()))
            {
                string Guid = GuidHelper.GetGuid(clawArm.gameObject);
                ExosuitClawArmAction Changed = new ExosuitClawArmAction(Guid);
                packetSender.Send(Changed);
            }
        }

        public void BroadcastSpawnedArm(Exosuit exo)
        {
            string Guid = GuidHelper.GetGuid(exo.gameObject);
            if (!string.IsNullOrEmpty(Guid))
            {
                string leftArmGuid = "8EBFDCE5-B4D6-4F16-85B8-58BA71ECED77";
                string rightArmGuid = "D9624C09-ABA1-4B9E-AE77-A63F83ACD59A";

                IExosuitArm spawnedRArm = (IExosuitArm)exo.ReflectionGet("rightArm");
                IExosuitArm spawnedLArm = (IExosuitArm)exo.ReflectionGet("leftArm");

                GameObject spawnedRArmOb = spawnedRArm.GetGameObject();
                spawnedRArmOb.SetNewGuid(rightArmGuid);

                GameObject spawnedLArmOb = spawnedLArm.GetGameObject();
                spawnedLArmOb.SetNewGuid(leftArmGuid);

                ExosuitSpawnedArmAction Changed = new ExosuitSpawnedArmAction(Guid, leftArmGuid, rightArmGuid);
                packetSender.Send(Changed);
            }
        }
    }
}
