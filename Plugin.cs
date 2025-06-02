using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using Buttplug.Client;
using System;
using UnityEngine;

namespace OmegaPlug;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInProcess("My Dystopian Robot Girlfriend.exe")]
public class Plugin : BasePlugin
{
    internal static new ManualLogSource Log;

    public static ButtplugClient Client;

    public override void Load()
    {
        Log = base.Log;
        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

        Client = new ButtplugClient("OmegaPlug");

        Patches.Patcher.DoPatching();

        AddComponent<OmegaUpdateComponent>();

        var client = new ButtplugClient("Example Client");
        var connector = new ButtplugWebsocketConnector(new Uri("ws://127.0.0.1:12345"));

        client.ServerDisconnect += ConnectPlug;

        ConnectPlug();
    }

    public static async void ConnectPlug(object sender = null, System.EventArgs e = null)
    {
        if (Client.Connected)
        {
            Log.LogError("Already connected to the server.");
            return;
        }

        try
        {
            await Client.ConnectAsync(new ButtplugWebsocketConnector(new Uri("ws://127.0.0.1:12345")));
            Log.LogInfo("Connected to the Buttplug server successfully.");

            await Plugin.Client.StartScanningAsync();
        }
        catch (System.Exception ex)
        {
            Log.LogError(ex);
        }
    }

    public static void Vibrate(float intensity)
    {
        if (intensity >= 0)
        {
            if (intensity != 0) Log.LogInfo($"~[VC] Vibrating with intensity: {intensity*100}%~");

            foreach (ButtplugClientDevice device in Client.Devices)
            {
                _ = device.VibrateAsync((double)intensity);
            }
        }
    }
}

public class OmegaUpdateComponent : MonoBehaviour
{
    public static OmegaUpdateComponent Instance { get; private set; }

    public static bool isBoyMode = true;

    public void awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Plugin.Log.LogInfo("[VC] VibeController instance created.");
        }
    }

    public void Update()
    {
        //REFS
        //Live2DBlowjobController live2DBlowjobController = GameObject.FindObjectOfType<Live2DBlowjobController>();
        //Live2DFuckController live2DFuckController = GameObject.FindObjectOfType<Live2DFuckController>();
        //Live2DCowgirlController live2DCowgirlController = GameObject.FindObjectOfType<Live2DCowgirlController>();

        if (isBoyMode) {
            Live2DBlowjobController live2DBlowjobController = GameObject.FindObjectOfType<Live2DBlowjobController>();
            if (live2DBlowjobController != null)
            {
                if (live2DBlowjobController.CurrentDepth > 0) Plugin.Log.LogInfo($"[BM][Blowjob] Depth: {live2DBlowjobController.CurrentDepth*100}%");
                Plugin.Vibrate(live2DBlowjobController.CurrentDepth);
            }
        } else {
            Live2DController l2C = GameObject.FindObjectOfType<Live2DController>();
            if (l2C == null) { Plugin.Log.LogWarning("Live2DController not found!"); return; }
            Plugin.Log.LogInfo($"[GM][Perpective] Pleasure: {l2C.CurrentBrain.pleasure}");
            Plugin.Vibrate(l2C.CurrentBrain.pleasure);
        }
    }
}