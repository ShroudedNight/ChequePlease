using System;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppSystem.Collections.Generic;

namespace com.shroudednight.ChequePlease;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    public override void Load()
    {
        try
        {
            var harmony = new Harmony($"{MyPluginInfo.PLUGIN_GUID}");
            harmony.PatchAll();
        }
        catch (Exception e)
        {
            Log.FATAL($"Plugin {MyPluginInfo.PLUGIN_GUID} failed to load: {e}");
            throw;
        }
        Log.INFO($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
    }
    
    [HarmonyPatch(typeof(Player), nameof(Player.Start))]
    public class Player_PatchStart
    {
        public static bool Prefix(Player __instance)
        {
            var screwDriverPreset = Toolbox.Instance.GetInteractablePreset("Screwdriver");
            var inspectableInventory = screwDriverPreset.actionsPreset.Find(
                new Func<InteractableActionsPreset, bool>(preset => "InspectableInventory".Equals(preset.name)));
            var receiptPreset = Toolbox.Instance.GetInteractablePreset("Receipt");
            receiptPreset.actionsPreset.RemoveInspectable();
            receiptPreset.actionsPreset.AddIfAbscent(inspectableInventory);
            return true;
        }

        public static void Postfix(Player __instance)
        {
        }
    }
}
