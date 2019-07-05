using Harmony12;
using Kingmaker.PubSubSystem;
using Kingmaker.UI;
using System;
using UnityEngine;

namespace ExitToMap {
    [HarmonyPatch(typeof(KeyboardAccess), "RegisterBuiltinBindings")]
    internal static class RegisterExitKey {
        private static void Postfix(KeyboardAccess __instance) {
            __instance.RegisterBinding(
                "ExitToMap", 
                (KeyCode)Enum.Parse(typeof(KeyCode), Main.settings.Keybinding), 
                KeyboardAccess.GetGameModesArray(0), 
                Main.settings.CtrlPressed, 
                Main.settings.AltPressed, 
                Main.settings.ShiftPressed, 
                false, 
                0);
            EventBus.Subscribe(__instance); 
        }
    }
}
