using System.Reflection;
using System.Timers;
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
    public class ExosuitGrapplingHitProcessor : ClientPacketProcessor<ExosuitGrapplingHit>
    {
        private readonly IPacketSender packetSender;

        public ExosuitGrapplingHitProcessor(IPacketSender packetSender)
        {
            this.packetSender = packetSender;
        }
        public override void Process(ExosuitGrapplingHit packet)
        {
            using (packetSender.Suppress<ExosuitGrapplingHit>())
            {
                GameObject _gameObject = GuidHelper.RequireObjectFrom(packet.Guid);
                ExosuitGrapplingArm grapplingArm = _gameObject.GetComponent<ExosuitGrapplingArm>();
                Exosuit exosuit = grapplingArm.GetComponentInParent<Exosuit>();

                GrapplingHook hook = (GrapplingHook)grapplingArm.ReflectionGet("hook");
                Transform front = (Transform)grapplingArm.ReflectionGet("front");

                hook.transform.parent = null;
                hook.transform.position = front.transform.position;
                hook.SetFlying(true);
                GameObject x = null;
                Vector3 a = default(Vector3);
                UWE.Utils.TraceFPSTargetPosition(exosuit.gameObject, 100f, ref x, ref a, false);
                if (x == null || x == hook.gameObject)
                {
                    a = packet.Position + packet.Forward * 25f;
                }
                Vector3 a2 = Vector3.Normalize(a - hook.transform.position);
                hook.rb.velocity = a2 * 25f;

                Vector3 grapplingStartPos = (Vector3)grapplingArm.ReflectionGet("grapplingStartPos");
                global::Utils.PlayFMODAsset(grapplingArm.shootSound, front, 15f);
                grapplingStartPos = exosuit.transform.position;
            }
        }
    }
}
