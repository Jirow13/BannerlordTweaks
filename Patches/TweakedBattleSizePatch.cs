using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using TaleWorlds.MountAndBlade;
using TaleWorlds.Core;

namespace BannerlordTweaks.Patches
{
    /*
    [HarmonyPatch(typeof(BannerlordConfig), "GetRealBattleSize")]
    public class TweakedBattleSizePatch
    {
        static void Postfix(ref int __result)
        {

            if (BannerlordTweaksSettings.Instance is { } settings && settings.BattleSize > 0)
            {
                __result = BannerlordTweaksSettings.Instance.BattleSize;
                //__result = 1000;
            }

            return;
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.BattleSizeTweakEnabled;
    }
    */

    
    [HarmonyPatch(typeof(MissionAgentSpawnLogic), MethodType.Constructor, new Type[] { typeof(IMissionTroopSupplier[]), typeof(BattleSideEnum) })]
    public class TweakedBattleSizePatch2
    {
        static void Postfix(MissionAgentSpawnLogic __instance, ref int ____battleSize)
        {
            
            if (BannerlordTweaksSettings.Instance is { } settings && settings.BattleSize > 0)
            {
                //DebugHelpers.DebugMessage("MissonAgentSpawnLogic Battle Size Adjustment Triggered");
                ____battleSize = settings.BattleSize;
                DebugHelpers.ColorGreenMessage("Max Battle Size Modified to: "+settings.BattleSize);
            }
                        
            return;
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.BattleSizeTweakEnabled;
    }
    

    /*
    [HarmonyPatch(typeof(BannerlordConfig), "BattleSize", MethodType.Getter)]

    public class BattleSizesPatch
    {
        static bool Prefix(ref int __result)
        {
            if (BannerlordTweaksSettings.Instance is not null)
            {
                __result = BannerlordTweaksSettings.Instance.BattleSize;
                return false;
            }
            return true;
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.BattleSizeTweakEnabled;
    }
    */
}
