using HarmonyLib;
using SandBox.TournamentMissions.Missions;
using System.Reflection;
using TaleWorlds.Library;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(TournamentBehavior), "OnPlayerWinTournament")]
    public class OnPlayerWinTournamentPatch
    {
        static bool Prefix(TournamentBehavior __instance)
        {
            if (BannerlordTweaksSettings.Instance is { } settings)
            {
                typeof(TournamentBehavior).GetProperty("OverallExpectedDenars").SetValue(__instance, __instance.OverallExpectedDenars + settings.TournamentGoldRewardAmount);
            }
            return true;
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.TournamentGoldRewardEnabled;
    }


    [HarmonyPatch(typeof(TournamentBehavior), "CalculateBet")]
    public class CalculateBetPatch
    {
        private static PropertyInfo? betOddInfo = null;

        static void Postfix(TournamentBehavior __instance)
        {
            if (BannerlordTweaksSettings.Instance is { } settings)
            {
                betOddInfo?.SetValue(__instance, MathF.Max((float)betOddInfo.GetValue(__instance), settings.MinimumBettingOdds, 0));
            }
        }

        static bool Prepare()
        {
            if (BannerlordTweaksSettings.Instance is { } settings && settings.MinimumBettingOddsTweakEnabled)
            {
                betOddInfo = typeof(TournamentBehavior).GetProperty(nameof(TournamentBehavior.BetOdd), BindingFlags.Public | BindingFlags.Instance);
                return true;
            }
            return false;
        }
    }
}
