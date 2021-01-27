using HarmonyLib;
using System;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Core;
using TaleWorlds.Localization;

// Convert TweakedSettlementFoodModel to patch due to 1.5.7 changes.

namespace BannerlordTweaks.Patches
{    
    [HarmonyPatch(typeof(DefaultSettlementFoodModel), "CalculateTownFoodStocksChange")]
    public class DefaultSettlementFoodModelPatch
    {
        //static void Postfix(Town town, ref ExplainedNumber result, ref int __result)
        static void Postfix(Town town, ref ExplainedNumber __result)
        {
            if (BannerlordTweaksSettings.Instance is { } settings && settings.SettlementFoodBonusEnabled && town is not null)
            {
                if (town.IsCastle)
                    __result.Add(settings.CastleFoodBonus, new TextObject("Military rations"));
                else if (town.IsTown)
                    __result.Add(settings.TownFoodBonus, new TextObject("Citizen food drive"));

                if (settings.SettlementProsperityFoodMalusTweakEnabled && settings.SettlementProsperityFoodMalusDivisor != 50)
                {
                    float malus = town.Owner.Settlement.Prosperity / 50f;
                    __result.Add(malus, new TextObject("shouldn't be seen!"));
                    //explanation?.Lines.Remove(explanation.Lines.Last());

                    
                    TextObject prosperityTextObj = GameTexts.FindText("str_prosperity", null);
                    /*
                    var line = explanation?.Lines.Where((x) => !string.IsNullOrWhiteSpace(x.Name) && x.Name == prosperityTextObj.ToString()).FirstOrDefault();
                    if (line != null) explanation?.Lines.Remove(line);
                    */

                    malus = -town.Owner.Settlement.Prosperity / settings.SettlementProsperityFoodMalusDivisor;
                    __result.Add(malus, prosperityTextObj);
                }
            }
            return;
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.SettlementFoodBonusEnabled;
    }

    /*
    public class TweakedSettlementFoodModel : DefaultSettlementFoodModel
    {
        public override float CalculateTownFoodStocksChange(Town town, StatExplainer? explanation = null)
        {
            if (town == null) throw new ArgumentNullException(nameof(town));
            float baseVal = base.CalculateTownFoodStocksChange(town, explanation);
            if (BannerlordTweaksSettings.Instance is { } settings && settings.SettlementFoodBonusEnabled)
            {
                ExplainedNumber en = new ExplainedNumber(baseVal, explanation);
                explanation?.Lines.Remove(explanation.Lines.Last());

                if (town.IsCastle)
                    en.Add(settings.CastleFoodBonus, new TextObject("Military rations"));
                else if (town.IsTown)
                    en.Add(settings.TownFoodBonus, new TextObject("Citizen food drive"));

                if (settings.SettlementProsperityFoodMalusTweakEnabled && settings.SettlementProsperityFoodMalusDivisor != 50)
                {
                    float malus = town.Owner.Settlement.Prosperity / 50f;
                    en.Add(malus, new TextObject("shouldn't be seen!"));
                    explanation?.Lines.Remove(explanation.Lines.Last());

                    TextObject prosperityTextObj = GameTexts.FindText("str_prosperity", null);
                    var line = explanation?.Lines.Where((x) => !string.IsNullOrWhiteSpace(x.Name) && x.Name == prosperityTextObj.ToString()).FirstOrDefault();
                    if (line != null) explanation?.Lines.Remove(line);

                    malus = -town.Owner.Settlement.Prosperity / settings.SettlementProsperityFoodMalusDivisor;
                    en.Add(malus, prosperityTextObj);
                }

                return en.ResultNumber;
            }
            else
                return baseVal;
        }
    }
    */
}
