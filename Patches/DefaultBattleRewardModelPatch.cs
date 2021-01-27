using HarmonyLib;
using Helpers;
using System;
using System.Windows.Forms;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;
using TaleWorlds.Core;
using TaleWorlds.Localization;

// Explained Number Changed in 1.5.7 - All patches adjusted to new format.

namespace BannerlordTweaks.Patches
{
    
    [HarmonyPatch(typeof(DefaultBattleRewardModel), "CalculateRenownGain")]
    [HarmonyPriority(Priority.Last)]
    public class DefaultBattleRewardModelPatch
    {
        
        //static bool Prefix(PartyBase party, float renownValueOfBattle, float contributionShare, StatExplainer explanation, ref float __result)
        /*
        static bool Prefix(PartyBase party, float renownValueOfBattle, float contributionShare, ref ExplainedNumber __result)
        {
            try
            {
                var battleRenownMultiplier = 1f;

                if ( (BannerlordTweaksSettings.Instance is { } settings && party.LeaderHero != null) && (settings.BattleRewardApplyToAI || party.LeaderHero == Hero.MainHero) )
                {
                    battleRenownMultiplier = settings.BattleRenownMultiplier;
                }

                //var stat = new ExplainedNumber((renownValueOfBattle * contributionShare) * battleRenownMultiplier, explanation);

                if (party.IsMobile)
                    if(party.MobileParty.HasPerk(DefaultPerks.Charm.ShowYourScars, false))
                    {
                        PerkHelper.AddPerkBonusForParty(DefaultPerks.Charm.ShowYourScars, party.MobileParty, true, ref __result);
                        // DebugHelpers.DebugMessage($"DefaultBattleRewardModelRenownPatch. Show Your Scars Bonus\nHero is:"+party.LeaderHero.Name+"\nrenownValueOfBattle is: " + renownValueOfBattle + "\nbattleRenownMultiplier is:" + battleRenownMultiplier + "\nexplanation:" + explanation + "\n");
                    }
                    if (party.MobileParty.HasPerk(DefaultPerks.Throwing.LongReach, true))
                    {
                        PerkHelper.AddPerkBonusForParty(DefaultPerks.Throwing.LongReach, party.MobileParty, false, ref __result);
                    }
                    PerkObject famousCommander = DefaultPerks.Leadership.FamousCommander;
                    MobileParty mobileParty = party.MobileParty;
                //PerkHelper.AddPerkBonusForCharacter(famousCommander, (mobileParty != null) ? mobileParty.Leader : null, true, ref result);
                PerkHelper.AddPerkBonusForCharacter(famousCommander, mobileParty?.Leader, true, ref __result);

                __result.AddFactor(battleRenownMultiplier, new TaleWorlds.Localization.TextObject("BT Battle Renown Tweak Multiplier"));

                //__result = result.ResultNumber;
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during DefaultBattleRewardModelRenownPatch. Reverting to original behaviour...\n\nException:\n{ex.Message}\n\n{ex.InnerException?.Message}\n\n{ex.InnerException?.InnerException?.Message}");
                return true;
            }
        }
        */


        static void Postfix(PartyBase party, float renownValueOfBattle, float contributionShare, ref ExplainedNumber result)
        {
            //float num = __result.ResultNumber;
            //float battleRenownMultiplier = 1f;
            _ = (renownValueOfBattle, contributionShare);

            if ((BannerlordTweaksSettings.Instance is { } settings && party.LeaderHero != null) && (settings.BattleRewardApplyToAI || party.LeaderHero == Hero.MainHero))
            {
                float battleRenownMultiplier = settings.BattleRenownMultiplier;
                result.AddFactor(battleRenownMultiplier, new TaleWorlds.Localization.TextObject("BT Battle Renown Tweak Multiplier"));
            }
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.BattleRewardTweaksEnabled;
    }


    [HarmonyPatch(typeof(DefaultBattleRewardModel), "CalculateInfluenceGain")]
    public class DefaultBattleRewardModelInfluencePatch
    {

        /*
        //static bool Prefix(PartyBase party, float influenceValueOfBattle, float contributionShare, ref ExplainedNumber result, ref float __result)
        static bool Prefix(PartyBase party, float influenceValueOfBattle, float contributionShare, ref ExplainedNumber __result)
        {
            try
            {
                var battleInfluenceMultiplier = 1f;

                //if (BannerlordTweaksSettings.Instance.BattleRewardApplyToAI || party.LeaderHero != null && party.LeaderHero == Hero.MainHero)
                if ((BannerlordTweaksSettings.Instance is { } settings && party.LeaderHero != null) && (settings.BattleRewardApplyToAI || party.LeaderHero == Hero.MainHero))
                {
                    battleInfluenceMultiplier = settings.BattleInfluenceMultiplier;
                }

                //var stat = new ExplainedNumber(party.MapFaction.IsKingdomFaction ? (influenceValueOfBattle * contributionShare * battleInfluenceMultiplier) : 0f, explanation, null);
                __result.Add(party.MapFaction.IsKingdomFaction ? (influenceValueOfBattle * contributionShare * battleInfluenceMultiplier) : 0f, new TextObject("BT Inflience Gain Tweak"), null);

                //__result = result.ResultNumber;
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during DefaultBattleRewardModelInfluencePatch. Reverting to original behavior... \n\nException:\n{ex.Message}\n\n{ex.InnerException?.Message}\n\n{ex.InnerException?.Message}");
                return true;
            }
        }
        */

        static void Postfix(PartyBase party, float influenceValueOfBattle, float contributionShare, ref ExplainedNumber result)
        {
            if ((BannerlordTweaksSettings.Instance is { } settings && party.LeaderHero != null) && (settings.BattleRewardApplyToAI || party.LeaderHero == Hero.MainHero))
            {
                float battleInfluenceMultiplier = settings.BattleInfluenceMultiplier;
                result.Add(party.MapFaction.IsKingdomFaction ? (influenceValueOfBattle * contributionShare * battleInfluenceMultiplier) : 0f, new TextObject("BT Inflience Gain Tweak"), null);
            }
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.BattleRewardTweaksEnabled;

    }
}
