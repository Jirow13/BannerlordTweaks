using HarmonyLib;
using TaleWorlds.Core;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(WeaponComponentData), "CanHitMultipleTargets", MethodType.Getter)]
    public class WeaponComponentDataPatch
    {
        static void Postfix(ref bool __result, WeaponComponentData __instance)
        {
            if (BannerlordTweaksSettings.Instance is { } settings)
            {
                __result = (settings.TwoHandedWeaponsSliceThroughEnabled && __instance.WeaponClass == WeaponClass.TwoHandedAxe || __instance.WeaponClass == WeaponClass.TwoHandedMace ||
                    __instance.WeaponClass == WeaponClass.TwoHandedPolearm || __instance.WeaponClass == WeaponClass.TwoHandedSword) ||
                    (settings.SingleHandedWeaponsSliceThroughEnabled && __instance.WeaponClass == WeaponClass.OneHandedSword ||
                    __instance.WeaponClass == WeaponClass.OneHandedPolearm || __instance.WeaponClass == WeaponClass.OneHandedAxe);
            }
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && (settings.TwoHandedWeaponsSliceThroughEnabled || settings.SingleHandedWeaponsSliceThroughEnabled);
    }
}
