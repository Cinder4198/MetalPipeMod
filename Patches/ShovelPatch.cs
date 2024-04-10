using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;
using Unity.Netcode;
using MetalPipeMod;
using LCSoundTool;
using System.Transactions;


namespace MetalPipeMod.Patches {
    [HarmonyPatch(typeof(GrabbableObject), "Start")]
    internal class ShovelPatch
    {
        public static void Prefix(GrabbableObject __instance)
        {
            
            if ((__instance.GetType() == typeof(Shovel)) && (__instance.gameObject.GetComponentInChildren<ScanNodeProperties>() == null))
            {
                MeshFilter mesh = __instance.gameObject.GetComponentInChildren<MeshFilter>();
                mesh.mesh = MetalPipeModBase.newModel;
                Renderer renderer = __instance.gameObject.GetComponentInChildren<MeshRenderer>();                       //PenEventType[ppeepoopooamogussussyyyyyyyyyyyyy#slayqueen]
                renderer.material = MetalPipeModBase.newMaterial;
                
            } 

        }

    }

}