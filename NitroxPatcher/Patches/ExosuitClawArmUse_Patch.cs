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
    public class ExosuitClawArmUse_Patch : NitroxPatch
    {
        public static readonly Type TARGET_CLASS = typeof(ExosuitClawArm);
        public static readonly MethodInfo TARGET_METHOD = TARGET_CLASS.GetMethod("TryUse", BindingFlags.NonPublic | BindingFlags.Instance);

        public static void Postfix(ExosuitClawArm __instance)
        {
            NitroxServiceLocator.LocateService<ExosuitModuleEvent>().BroadcastClawArmUse(__instance);
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchPostfix(harmony, TARGET_METHOD);
        }
    }
}

