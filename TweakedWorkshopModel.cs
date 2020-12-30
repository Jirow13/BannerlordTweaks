using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace BannerlordTweaks
{
    public class TweakedWorkshopModel : DefaultWorkshopModel
    {
        public override int GetMaxWorkshopCountForPlayer()
        {
            if (BannerlordTweaksSettings.Instance is { } settings && settings.MaxWorkshopCountTweakEnabled)
                return settings.BaseWorkshopCount + Clan.PlayerClan.Tier * settings.BonusWorkshopsPerClanTier;
            else
                return base.GetMaxWorkshopCountForPlayer();
        }

        public override int GetBuyingCostForPlayer(Workshop workshop)
        {
            if (workshop == null) throw new ArgumentNullException(nameof(workshop));
            if (BannerlordTweaksSettings.Instance is { } settings && settings.WorkshopBuyingCostTweakEnabled)
                return workshop.WorkshopType.EquipmentCost + (int)workshop.Settlement.Prosperity / 2 + settings.WorkshopBaseCost;
            else
                return base.GetBuyingCostForPlayer(workshop);
        }
    }
}
