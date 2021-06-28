using Harmony12;
using Kingmaker;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Utility;
using Kingmaker.View.MapObjects;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ExitToMap
{

    [HarmonyPatch(typeof(Game), "OnAreaLoaded")]
    class AddPointEntered
    {

        public static AreaTransition GetClosestTransition(AreaTransition[] transitions)
        {
            AreaTransition areaTransition = null;
            Vector3 position = Game.Instance.Player.MainCharacter.Value.Position;
            float num = float.PositiveInfinity;
            foreach (AreaTransition areaTransition2 in transitions)
            {
                float num2 = Vector3.Distance(areaTransition2.transform.position, position);
                if (num2 < num && areaTransition2.AreaEnterPoint == Game.Instance.BlueprintRoot.GlobalMap.GlobalMapEnterPoint)
                {
                    areaTransition = areaTransition2;
                    num = num2;
                }
            }
            if (areaTransition == null)
            {
                Main.Logger.Log("Could not find a way to exit the area.");
            }
            return areaTransition;
        }

        [HarmonyPostfix]
        public static void FindEnterTransition()
        {
            Main.areaTransitionEntered = AddPointEntered.GetClosestTransition(LinqExtensions.NotNull<AreaTransition>(from o in Game.Instance.State.MapObjects.All
                                                                                                                     select o.View.GetComponent<AreaTransition>()).ToArray<AreaTransition>());
        }
    }
}
