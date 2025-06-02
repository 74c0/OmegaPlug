using HarmonyLib;
using Il2CppSystem;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using static Live2DCowgirlController;

namespace OmegaPlug;

public class Patches
{
    public class Patcher {
        public static void DoPatching() {
            var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            harmony.PatchAll();
        }

        /*
         * Blowjob doesn't have a insertionplanevisualizer, so we patch the BasicInsertionHandleFuckAxis instead
         * hacky-sh way to get the vibe working for blowjob as it doesn't work like the other modes
         */
        [HarmonyPatch(typeof(BasicInsertionHandleFuckAxis), nameof(BasicInsertionHandleFuckAxis.OnAnyMovement))]
        class BasicInsertionHandleFuckAxisPatch_OnAnyMovement
        {
            public static void Postfix(ref BasicInsertionHandleFuckAxis __instance, float before, float after, float input)
            {
                if (OmegaUpdateComponent.curType == OmegaUpdateComponent.CurrentType.Live2DBlowjobController)
                {
                    Plugin.Log.LogInfo($"[Patch][BasicInsertionHandleFuckAxis] vibe -> {after}%");
                    Plugin.Vibrate(Mathf.Clamp(after, 0, 100));
                }
            }
        }

        /*
         * Patch the InsertionPlaneVisualizer Update method to vibrate based on thrust (misdleading, its depth really)
         * This works for all insertion modes, except Blowjob
         */
        [HarmonyPatch(typeof(InsertionPlaneVisualizer), nameof(InsertionPlaneVisualizer.Update))]
        class InsertionPlanePatch
        {
            public static void Postfix(ref InsertionPlaneVisualizer __instance, float thrust, float angleX, float angleY, IPenisInfo penisInfo)
            {
                if (OmegaUpdateComponent.curType != OmegaUpdateComponent.CurrentType.Live2DBlowjobController)
                {
                    Plugin.Log.LogInfo($"[Patch][InsertionPlaneVisualizer] vibe -> {thrust}%");
                    Plugin.Vibrate(Mathf.Clamp(thrust, 0, 100)); // Set the vibe directly based on thrust
                }
            }
        }
    }
}