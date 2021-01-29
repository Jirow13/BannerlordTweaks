using HarmonyLib;
using System;
using System.Reflection;
using TaleWorlds.Localization;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;

// Courtest of Ruihan - Since it costs 0 influence to raise cohesion on all-clan-party armies, even if low-morale or starving, so why make them click and add cohesion?


namespace BannerlordTweaks.Patches
{

    /* 
     ExplainedNumber seems to have changed in 1.5.7. And ExplainedNumber.StatExplainer does not seem to be usable as a param anymore.
    */
    [HarmonyPatch(typeof(DefaultArmyManagementCalculationModel), "CalculateCohesionChange")]
    public class CalculateCohesionChangePatch
    {

        //static bool Prefix(Army army, ref float __result, StatExplainer? explanation = null)
        static bool Prefix(Army army, bool includeDescriptions, ref ExplainedNumber __result)
        {
            int num1 = 0;
            foreach (MobileParty party in army.Parties)
            {
                if (party.LeaderHero.Clan != army.LeaderParty.LeaderHero.Clan)
                {
                    num1++;
                }
            }
            if (num1 > 0)
            {
                return true;
            }
            else
            {
                //if (result is null) result = new ExplainedNumber(-2f, true, null);
                __result.Add(0, new TextObject("Clan-Only Armies Suffer No Cohesion Loss"));
                //ExplainedNumber explainedNumber = new ExplainedNumber(0f, explanation);
                //result = explainedNumber.ResultNumber;
                return false;
            }
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.ClanArmyLosesNoCohesionEnabled;
    }
}
