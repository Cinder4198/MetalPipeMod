using System.Runtime.CompilerServices;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using MetalPipeMod.Patches;
using LCSoundTool;

namespace MetalPipeMod;

[BepInPlugin(modGUID, modName, modVersion)]
[BepInDependency("LCSoundTool", BepInDependency.DependencyFlags.HardDependency)]
public class MetalPipeModBase : BaseUnityPlugin
{

    private const string modGUID = "Eyasu.MetalPipeShovelMod";
    private const string modName = "Metal Pipe Shovel Mod";
    private const string modVersion = "1.0";

    private readonly Harmony harmony = new Harmony(modGUID);

    private static MetalPipeModBase Instance;

    internal ManualLogSource mls;
    public static Mesh newModel;

    public static Material newMaterial;

    void Awake() {

    if (Instance == null) {
        Instance = this;
    }
    

    mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

    mls.LogInfo("Enabling Metal Pipe Shovel Mod...");

    string location = ((BaseUnityPlugin)this).Info.Location;
    string text = "MetalPipeShovelMod.dll";
    string directory = location.TrimEnd(text.ToCharArray());
    string modelFile = directory + "metalpipe";

    //mls.LogInfo("Grabbing Asset Bundle...");
    AssetBundle val = AssetBundle.LoadFromFile(modelFile);
    //mls.LogInfo("Asset Bundle Grabbed!");

    //mls.LogInfo("Loading metalpipe model...");
    newModel = val.LoadAssetWithSubAssets<Mesh>("metalpipe.obj")[0];
    //mls.LogInfo("Model Loaded!");

    //mls.LogInfo("Loading metalpipe material...");
    newMaterial = val.LoadAsset<Material>("Metalpipe.001");
    //mls.LogInfo("Material Loaded!");

    //mls.LogInfo("Replacing Sounds...");
    AudioClip shovelhit;
    //mls.LogInfo("Getting SOund File...");
    //mls.LogInfo("Directory: " + directory.TrimEnd('\\'));
    shovelhit = SoundTool.GetAudioClip(directory.TrimEnd('\\'), "sounds", "metalpipehit.mp3");
    //mls.LogInfo("Replacing hit 1...");
    SoundTool.ReplaceAudioClip("ShovelHitDefault", shovelhit);
    //mls.LogInfo("Replacing hit 2...");
    SoundTool.ReplaceAudioClip("ShovelHitDefault2", shovelhit);
    //mls.LogInfo("Sounds Replaced!");

    harmony.PatchAll(typeof(ShovelPatch));
    mls.LogInfo("Metal Pipe Shovel Mod Loaded!");

    }


}
