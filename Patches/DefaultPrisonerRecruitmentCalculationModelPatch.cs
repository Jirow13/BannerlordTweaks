using System;
using HarmonyLib;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Library;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultPrisonerRecruitmentCalculationModel), "GetConformityChangePerHour")]
    public class DefaultPrisonerRecruitmentCalculationModelPatch
    {
        static void Postfix(PartyBase party, CharacterObject troopToBoost, ref int __result)
         {
            if (BannerlordTweaksSettings.Instance is { } settings && settings.PrisonerConformityTweaksEnabled && party.LeaderHero is not null)
            {
                float num = 0;
                if (party.LeaderHero == Hero.MainHero ||
                  (party.Owner is not null && party.Owner.Clan == Hero.MainHero.Clan && settings.PrisonerConformityTweaksApplyToClan) ||
                  (settings.PrisonerConformityTweaksApplyToAi))
                {
                    num = __result * (1 + settings.PrisonerConformityTweakBonus);
                    party.MobileParty.EffectiveQuartermaster.AddSkillXp(DefaultSkills.Charm, (num/10));
                    __result = MBMath.Round(num);
                }
            }

            // Add Tier-Specific Boosts?
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.PrisonerConformityTweaksEnabled;
    }    
}
