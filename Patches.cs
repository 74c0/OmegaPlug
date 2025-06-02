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
         * Cowgirl doesn't have a BasicInsertionHandleFuckAxis, it doesn't work with this patch. Might need it later down the line.
         */
        /*[HarmonyPatch(typeof(BasicInsertionHandleFuckAxis), nameof(BasicInsertionHandleFuckAxis.OnAnyMovement))]
        class BasicInsertionHandleFuckAxisPatch_OnAnyMovement
        {
            public static void Postfix(ref BasicInsertionHandleFuckAxis __instance, float before, float after, float input)
            {
                //if (after > 0)  Plugin.Log.LogInfo($"[Patch][BasicInsertionHandleFuckAxis] vibe -> {after}%");
                //Plugin.Vibrate(Mathf.Clamp(after, 0, 1));
            }
        }*/

        /*
         * Patch the InsertionPlaneVisualizer Update method to vibrate based on thrust (misdleading, its depth really). Only issue is blowjob doesn't work with this, but we can run that on Update()
         */
        [HarmonyPatch(typeof(InsertionPlaneVisualizer), nameof(InsertionPlaneVisualizer.Update))]
        class InsertionPlanePatch
        {
            public static void Postfix(ref InsertionPlaneVisualizer __instance, float thrust, float angleX, float angleY, IPenisInfo penisInfo)
            {
                if (thrust > 0) Plugin.Log.LogInfo($"[BM][Patch][InsertionPlaneVisualizer] vibe -> {thrust*100}%");
                Plugin.Vibrate(Mathf.Clamp(thrust, 0, 1)); // Set the vibe directly based on thrust
            }
        }
    }
}