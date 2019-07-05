using Harmony;
using Kingmaker.View.MapObjects;
using System;
using System.Reflection;
using UnityEngine;
using UnityModManagerNet;

namespace ExitToMap {
    internal static class Main {

        private static bool Load(UnityModManager.ModEntry modEntry) {
            HarmonyInstance.Create(modEntry.Info.Id).PatchAll(Assembly.GetExecutingAssembly());
            Main.settings = UnityModManager.ModSettings.Load<Settings>(modEntry);
            Main.Logger = modEntry.Logger;
            modEntry.OnToggle = new Func<UnityModManager.ModEntry, bool, bool>(Main.OnToggle);
            modEntry.OnGUI = new Action<UnityModManager.ModEntry>(Main.OnGUI);
            modEntry.OnSaveGUI = new Action<UnityModManager.ModEntry>(Main.OnSaveGUI);
            return true;
        }

        private static bool OnToggle(UnityModManager.ModEntry modEntry, bool value) {
            Main.enabled = value;
            return true;
        }

        private static void OnGUI(UnityModManager.ModEntry modEntry) {
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            GUILayout.Label("Keybind", new GUILayoutOption[]
            {
                GUILayout.ExpandWidth(false)
            });
            Main.settings.Keybinding = GUILayout.TextField(Main.settings.Keybinding, 1, new GUILayoutOption[]
            {
                GUILayout.Width(30f)
            });
            Main.settings.ShiftPressed = GUILayout.Toggle(Main.settings.ShiftPressed, "Shift", Array.Empty<GUILayoutOption>());
            Main.settings.CtrlPressed = GUILayout.Toggle(Main.settings.CtrlPressed, "Ctrl", Array.Empty<GUILayoutOption>());
            Main.settings.AltPressed = GUILayout.Toggle(Main.settings.AltPressed, "Alt", Array.Empty<GUILayoutOption>());
            GUILayout.EndHorizontal();
        }

        private static void OnSaveGUI(UnityModManager.ModEntry modEntry) {
            Main.settings.Save(modEntry);
        }

        public static bool enabled;

        public static Settings settings;

        public static UnityModManager.ModEntry.ModLogger Logger;

        public static AreaTransition areaTransitionEntered;
    }
}
