using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Core;

namespace BannerlordTweaks
{
    public class TweakedSiegeEventModel : DefaultSiegeEventModel
    {
        public override float GetConstructionProgressPerHour(SiegeEngineType type, SiegeEvent siegeEvent, ISiegeEventSide side, StatExplainer? explanation = null)
        {
            if (BannerlordTweaksSettings.Instance is { } settings && settings.SiegeConstructionProgressPerDayMultiplierEnabled)
                return base.GetConstructionProgressPerHour(type, siegeEvent, side, explanation) * settings.SiegeConstructionProgressPerDayMultiplier;
            else
                return base.GetConstructionProgressPerHour(type, siegeEvent, side, explanation);
        }

        public override float GetColleteralDamageCasualties(SiegeEngineType siegeEngineType, MobileParty party)
        {
            if (BannerlordTweaksSettings.Instance is { } settings && settings.SiegeCasualtiesTweakEnabled)
                return settings.SiegeCollateralDamageCasualties;
            else
                return base.GetColleteralDamageCasualties(siegeEngineType, party);
        }

        public override float GetDestructionCasualties(SiegeEngineType destroyedSiegeEngine)
        {
            if (BannerlordTweaksSettings.Instance is { } settings && settings.SiegeCasualtiesTweakEnabled)
                return settings.SiegeDestructionCasualties;
            else
                return base.GetDestructionCasualties(destroyedSiegeEngine);
        }
    }
}
