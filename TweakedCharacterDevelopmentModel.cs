using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace BannerlordTweaks
{
    public class TweakedCharacterDevelopmentModel : DefaultCharacterDevelopmentModel
    {
        public override int LevelsPerAttributePoint => BannerlordTweaksSettings.Instance is { } settings && settings.AttributeFocusPointTweakEnabled
            ? settings.AttributePointRequiredLevel
            : base.LevelsPerAttributePoint;

        public override int FocusPointsPerLevel => BannerlordTweaksSettings.Instance is { } settings && settings.AttributeFocusPointTweakEnabled
            ? settings.FocusPointsPerLevel
            : base.FocusPointsPerLevel;
    }
}
