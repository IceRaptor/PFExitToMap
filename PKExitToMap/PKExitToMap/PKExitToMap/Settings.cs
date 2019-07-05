
using UnityModManagerNet;

namespace ExitToMap {

    public class Settings : UnityModManager.ModSettings {
        public override void Save(UnityModManager.ModEntry modEntry) {
            UnityModManager.ModSettings.Save<Settings>(this, modEntry);
        }

        public string Keybinding = "M";

        public bool ShiftPressed = true;

        public bool CtrlPressed;

        public bool AltPressed;
    }
}
