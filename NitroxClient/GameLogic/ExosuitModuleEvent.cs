using NitroxClient.Communication.Abstract;
using NitroxClient.GameLogic.Helper;
using NitroxModel.DataStructures.Util;
using NitroxModel.Helper;
using NitroxModel.Logger;
using NitroxModel.Packets;
using UnityEngine;

namespace NitroxClient.GameLogic
{
    public class ExosuitModuleEvent
    {
        private readonly IPacketSender packetSender;
        private readonly IMultiplayerSession multiplayerSession;

        public ExosuitModuleEvent(IPacketSender packetSender, IMultiplayerSession multiplayerSession)
        {
            this.packetSender = packetSender;
            this.multiplayerSession = multiplayerSession;
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
                Exosuit exosuit = grapplingArm.GetComponentInParent<Exosuit>();
                ushort playerId = multiplayerSession.Reservation.PlayerId;
                ushort pilot = ushort.Parse(exosuit.pilotId);
                if (pilot.Equals(playerId))
                {
                    string Guid = GuidHelper.GetGuid(grapplingArm.gameObject);
                    ExosuitGrapplingHit Changed = new ExosuitGrapplingHit(Guid, MainCamera.camera.transform.position, MainCamera.camera.transform.forward);
                    packetSender.Send(Changed);
                }
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
                IExosuitArm spawnedRArm = (IExosuitArm)exo.ReflectionGet("rightArm");
                IExosuitArm spawnedLArm = (IExosuitArm)exo.ReflectionGet("leftArm");
                
                GameObject spawnedRArmOb = spawnedRArm.GetGameObject();
                string rightArmGuid = GuidHelper.GetGuid(spawnedRArmOb);
                spawnedRArmOb.SetNewGuid(rightArmGuid);

                GameObject spawnedLArmOb = spawnedLArm.GetGameObject();
                string leftArmGuid = GuidHelper.GetGuid(spawnedLArmOb);
                spawnedLArmOb.SetNewGuid(leftArmGuid);

                ExosuitSpawnedArmAction Changed = new ExosuitSpawnedArmAction(Guid, leftArmGuid, rightArmGuid);
                packetSender.Send(Changed);
            }
        }
    }
}
