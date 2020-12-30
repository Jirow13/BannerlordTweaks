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
        static void Postfix(MobileParty party, StatExplainer explanation, ref int __result)
        {
            if (party.LeaderHero != null && party.LeaderHero == Hero.MainHero && BannerlordTweaksSettings.Instance is { } settings)
            {
                int num;
                if (BannerlordTweaksSettings.Instance.LeadershipPartySizeBonusEnabled)
                {
                    num = (int)Math.Ceiling(party.LeaderHero.GetSkillValue(DefaultSkills.Leadership) * settings.LeadershipPartySizeBonus);
                    __result += num;
                    explanation?.AddLine("BT Leadership bonus", num);
                }

                if (BannerlordTweaksSettings.Instance.StewardPartySizeBonusEnabled)
                {
                    num = (int)Math.Ceiling(party.LeaderHero.GetSkillValue(DefaultSkills.Steward) * settings.StewardPartySizeBonus);
                    __result += num;
                    explanation?.AddLine("BT Steward bonus", num);
                }
            }
        }

    static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.PartySizeTweakEnabled;
    }
    
    [HarmonyPatch(typeof(DefaultPartySizeLimitModel), "CalculateMobilePartyPrisonerSizeLimitInternal")]
    public class DefaultPrisonerSizeLimitModelPatch
    {
        static void Postfix(PartyBase party, StatExplainer explanation, ref int __result)
        {
            if (party.LeaderHero != null && party.LeaderHero == Hero.MainHero)
            {
                if (BannerlordTweaksSettings.Instance is { } settings && settings.PrisonerSizeTweakEnabled)
                {
                    double percent = Math.Abs((double)(settings.PrisonerSizeTweakPercent) / 100);
                    double num = (int)Math.Ceiling(__result * percent);
                    __result += (int)Math.Round(num);
                    explanation?.AddLine("BT Prisoner Limit Bonus", (float)num);
                }
            }
        }

    static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.PrisonerSizeTweakEnabled;
    }
}
