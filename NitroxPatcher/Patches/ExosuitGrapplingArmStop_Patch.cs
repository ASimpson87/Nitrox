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
    public class ExosuitGrapplingArmStop_Patch : NitroxPatch
    {
        public static readonly Type TARGET_CLASS = typeof(ExosuitGrapplingArm);
        public static readonly MethodInfo TARGET_METHOD = TARGET_CLASS.GetMethod("IExosuitArm.OnUseUp", BindingFlags.NonPublic | BindingFlags.Instance);

        public static bool Prefix(ExosuitGrapplingArm __instance)
        {
            NitroxServiceLocator.LocateService<ExosuitModuleEvent>().BroadcastGrapplingHookUse(__instance, false);

            return true;
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchPrefix(harmony, TARGET_METHOD);
        }
    }
}

