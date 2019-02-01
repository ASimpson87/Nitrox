using System;
using System.Reflection;
using Harmony;
using NitroxClient.Communication;
using NitroxClient.Communication.Abstract;
using NitroxClient.GameLogic;
using NitroxClient.GameLogic.Helper;
using NitroxModel.Core;
using NitroxModel.Logger;
using NitroxModel.Packets;
using UnityEngine;

namespace NitroxPatcher.Patches
{
    public class ExosuitGrapplingArmStart_Patch : NitroxPatch
    {
        public static readonly Type TARGET_CLASS = typeof(ExosuitGrapplingArm);
        public static readonly MethodInfo TARGET_METHOD = TARGET_CLASS.GetMethod("IExosuitArm.OnUseDown", BindingFlags.NonPublic | BindingFlags.Instance);

        public static bool Prefix(ExosuitGrapplingArm __instance, PacketSuppressor<ItemContainerRemove> __state)
        {
            __state = NitroxServiceLocator.LocateService<IPacketSender>().Suppress<ItemContainerRemove>();
            NitroxServiceLocator.LocateService<ExosuitModuleEvent>().BroadcastGrapplingHookStart(__instance);

            return true;
        }

        public static void Postfix(PacketSuppressor<ItemContainerRemove> __state)
        {
            if (__state != null)
            {
                __state.Dispose();
            }
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchMultiple(harmony, TARGET_METHOD, true, true, false);
        }
    }
}

