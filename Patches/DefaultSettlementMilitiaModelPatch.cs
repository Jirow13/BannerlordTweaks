using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Localization;
using System.Windows.Forms;
using System;

// Convert TweakedSettlementFoodModel to patch due to 1.5.7 changes.

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultSettlementMilitiaModel), "CalculateMilitiaChange")]

    public class DefaultSettlementMilitiaModelPatch
    {
        //static void Postfix(Settlement settlement, ref ExplainedNumber result, ref int __result)
        static void Postfix(Settlement settlement, ref ExplainedNumber __result)
        {
            if (BannerlordTweaksSettings.Instance is { } settings && settings.SettlementMilitiaBonusEnabled && settlement is not null)
            {
                if (settlement.IsCastle)
                    __result.Add(settings.CastleMilitiaBonus, new TextObject("Recruitment drive"));
                if (settlement.IsTown)
                    __result.Add(settings.TownMilitiaBonus, new TextObject("Citizen militia"));
            }
            return;
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.SettlementMilitiaBonusEnabled;
    }

    /*
    public class DefaultSettlementMilitiaModelPatch : DefaultSettlementMilitiaModel
    {
        public override float CalculateMilitiaChange(Settlement settlement, StatExplainer? explanation = null)
        {
            if (settlement == null) throw new ArgumentNullException(nameof(settlement));
            float baseVal = base.CalculateMilitiaChange(settlement, explanation);
            ExplainedNumber en = new ExplainedNumber(0f, explanation);
            en.Add(baseVal);
            try
            {
                if (BannerlordTweaksSettings.Instance is { } settings && settings.SettlementMilitiaBonusEnabled)
                {
                    if (settlement.IsCastle)
                        en.Add(settings.CastleMilitiaBonus, new TextObject("Recruitment drive"));
                    if (settlement.IsTown)
                        en.Add(settings.TownMilitiaBonus, new TextObject("Citizen militia"));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred in TweakedSettlementMilitiaModule:\n\n{ex.ToStringFull()}");
            }
            return en.ResultNumber;
        }

    
        public override void CalculateMilitiaSpawnRate(Settlement settlement, out float meleeTroopRate, out float rangedTroopRate, out float meleeEliteTroopRate, out float rangedEliteTroopRate)
        {
            base.CalculateMilitiaSpawnRate(settlement, out meleeTroopRate, out rangedTroopRate, out float _meleeEliteTroopRate, out float _rangedEliteTroopRate);

            if (BannerlordTweaksSettings.Instance is { } settings && settings.SettlementMilitiaEliteSpawnRateBonusEnabled)
            {
                _meleeEliteTroopRate += settings.SettlementEliteMeleeSpawnRateBonus;
                _rangedEliteTroopRate += settings.SettlementEliteRangedSpawnRateBonus;
            }
            meleeEliteTroopRate = _meleeEliteTroopRate;
            rangedEliteTroopRate = _rangedEliteTroopRate;
        }
    }
    */
}
