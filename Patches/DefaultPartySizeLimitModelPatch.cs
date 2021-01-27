using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Party;
using TaleWorlds.Core;
using TaleWorlds.Localization;


namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultPartySizeLimitModel), "CalculateMobilePartyMemberSizeLimit")]
    public class DefaultPartySizeLimitModelPatch
    {
        //static void Postfix(MobileParty party, StatExplainer explanation, ref int __result)
        static void Postfix(MobileParty party, ref ExplainedNumber __result)
        {
            if (party is not null && party.LeaderHero is not null && party.LeaderHero == Hero.MainHero && BannerlordTweaksSettings.Instance is { } settings)
            {
                int num;
                if (settings.LeadershipPartySizeBonusEnabled)
                {
                    num = (int)Math.Ceiling(party.LeaderHero.GetSkillValue(DefaultSkills.Leadership) * settings.LeadershipPartySizeBonus);
                    __result.Add((float)num, new TextObject("BT Leadership bonus"));
                    //__result += num;
                }

                if (settings.StewardPartySizeBonusEnabled)
                {
                    num = (int)Math.Ceiling(party.LeaderHero.GetSkillValue(DefaultSkills.Steward) * settings.StewardPartySizeBonus);
                    __result.Add((float)num, new TextObject("BT Steward bonus"));
                    //__result += num;
                }
            }
        }

    static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.PartySizeTweakEnabled;
    }


    //[HarmonyPatch(typeof(DefaultPartySizeLimitModel), "CalculateMobilePartyPrisonerSizeLimitInternal")]
    [HarmonyPatch(typeof(DefaultPartySizeLimitModel), "GetPartyPrisonerSizeLimit")]
    public class DefaultPrisonerSizeLimitModelPatch
    {
        //static void Postfix(PartyBase party, StatExplainer explanation, ref int __result)
        private static void Postfix(PartyBase party, ref ExplainedNumber __result)
        {
            if (party.LeaderHero != null && party.LeaderHero == Hero.MainHero)
            {
                if (BannerlordTweaksSettings.Instance is { } settings && settings.PrisonerSizeTweakEnabled)
                {
                    double percent = Math.Abs((double)(settings.PrisonerSizeTweakPercent) / 100);
                    double num = (int)Math.Ceiling(__result.ResultNumber * percent);
                    __result.Add((float)num, new TextObject("BT Prisoner Limit Bonus"));
                    //__result += (int)Math.Round(num);
                }
            }
        }

    static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.PrisonerSizeTweakEnabled;
    }
}
