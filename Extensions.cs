using BepInEx.Logging;
using Il2CppSystem.Collections.Generic;

namespace com.shroudednight.ChequePlease;

internal static class Extensions
{
    public static void INFO(this ManualLogSource logSource, string data) => logSource.Log(LogLevel.Info, data);
    public static void WARN(this ManualLogSource logSource, string data) => logSource.Log(LogLevel.Warning, data);
    public static void ERROR(this ManualLogSource logSource, string data) => logSource.Log(LogLevel.Error, data);
    public static void FATAL(this ManualLogSource logSource, string data) => logSource.Log(LogLevel.Fatal, data);
    
    internal static void RemoveInspectable(this List<InteractableActionsPreset> actionsPresetList)
    {
        foreach (var actionPreset in actionsPresetList)
        {
            if (!"Inspectable".Equals(actionPreset.name)) continue;
            actionsPresetList.Remove(actionPreset);
            break;
        }
    }

    internal static void AddIfAbscent(
        this List<InteractableActionsPreset> actionsPresetList,
        InteractableActionsPreset candidate)
    {
        if (!actionsPresetList.Contains(candidate))
        {
            actionsPresetList.Add(candidate);
        }
    }
}