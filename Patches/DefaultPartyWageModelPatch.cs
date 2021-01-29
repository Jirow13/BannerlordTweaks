using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Party;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultPartyWageModel), "GetTotalWage")]
    public class DefaultPartyWageModelPatch
    {

        static void Postfix(MobileParty mobileParty, ref ExplainedNumber __result)
        {
            if (BannerlordTweaksSettings.Instance is { } settings && settings.PartyWageTweaksEnabled && mobileParty != null)
            {
                float orig_result = __result.ResultNumber;
                if (mobileParty.IsMainParty || ( mobileParty.Party.MapFaction == Hero.MainHero.MapFaction && settings.ApplyWageTweakToFaction && !mobileParty.IsGarrison) )
                {
                    float num = settings.PartyWagePercent;
                    num = orig_result * num - orig_result;
                    //__result = MathF.Round(__result * num);
                    __result.Add(num, new TextObject("BT Party Wage Tweak"));
                }
                if (mobileParty.IsGarrison && mobileParty.Party.Owner == Hero.MainHero)
                {
                    float num2 = settings.GarrisonWagePercent;
                    num2 = orig_result * num2 - orig_result;
                    __result.Add(num2, new TextObject("BT Garrison Wage Tweak"));
                    // DebugHelpers.DebugMessage("Adjusted garrison " + mobileParty.Name + "by " + num2 + ". Value: " + __result);
                }
                //DebugHelpers.DebugMessage("Relevant data: mobileParty.Party.Owner:" + mobileParty.Party.Owner + "\nmobileParty.Party.MapFaction:" + mobileParty.Party.MapFaction + "\nmobileParty.Party.LeaderHero:" + mobileParty.Party.LeaderHero + "\nmobileParty.Party.Leader" + mobileParty.Party.Leader);
            }
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.PartyWageTweaksEnabled;
    }
}
