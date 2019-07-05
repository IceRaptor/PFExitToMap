
using Harmony;
using Kingmaker;
using Kingmaker.Designers.TempMapCode.Capital;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic.Commands;

namespace ExitToMap {

    [HarmonyPatch(typeof(Game), "Initialize")]
    internal static class BindExitKey {

        [HarmonyPostfix]
        public static void BindExit() {
            Game.Instance.Keyboard.Bind("ExitToMap", delegate () {
                if (Game.Instance.CurrentlyLoadedArea.IsCapital) {
                    Game.Instance.State.LoadedAreaState.Area.Get<CapitalCompanionLogic>()
                    .ExitCapital(Game.Instance.BlueprintRoot.GlobalMap.GlobalMapEnterPoint, Kingmaker.EntitySystem.Persistence.AutoSaveMode.AfterEntry);
                    return;
                }
                if (Game.Instance.UI.LootWindowController.WantLootZone() && Game.Instance.UI.LootWindowController.CanLootZone()) {
                    EventBus.RaiseEvent<ILootInterractionHandler>(delegate (ILootInterractionHandler e) {
                        e.HandleZoneLootInterraction(Main.areaTransitionEntered);
                    });
                    return;
                }
                AreaTransitionGroupCommand.ExecuteTransition(Main.areaTransitionEntered);
            });
        }
    }
}
