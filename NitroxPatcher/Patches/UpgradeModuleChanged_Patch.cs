using System;
using System.Reflection;
using Harmony;
using NitroxClient.GameLogic;
using NitroxModel.Core;
using UnityEngine;

namespace NitroxPatcher.Patches
{
    public class UpgradeModuleChangedPatch : NitroxPatch
    {
        public static readonly Type TARGET_CLASS = typeof(Exosuit);
        public static readonly MethodInfo TARGET_METHOD = TARGET_CLASS.GetMethod("SpawnArm", BindingFlags.NonPublic | BindingFlags.Instance);

        public static void Prefix(Exosuit __instance)
        {
            NitroxServiceLocator.LocateService<ModuleUpgradeEvent>().BroadcastModuleChanged(__instance);
        }

        public override void Patch(HarmonyInstance harmony)
        {
            PatchPrefix(harmony, TARGET_METHOD);
        }
    }
}
