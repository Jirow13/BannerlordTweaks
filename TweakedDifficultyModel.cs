using TaleWorlds.CampaignSystem.SandBox.GameComponents;

namespace BannerlordTweaks
{
    public class TweakedDifficultyModel : DefaultDifficultyModel
    {
        public override float GetDamageToFriendsMultiplier()
        {
            return BannerlordTweaksSettings.Instance is { } settings && settings.DamageToFriendsTweakEnabled ? settings.DamageToFriendsMultiplier : base.GetDamageToFriendsMultiplier();
        }

        public override float GetDamageToPlayerMultiplier()
        {
            return BannerlordTweaksSettings.Instance is { } settings && settings.DamageToPlayerTweakEnabled ? settings.DamageToPlayerMultiplier : base.GetDamageToPlayerMultiplier();
        }

        public override float GetPlayerTroopsReceivedDamageMultiplier()
        {
            return BannerlordTweaksSettings.Instance is { } settings && settings.DamageToTroopsTweakEnabled ? settings.DamageToTroopsMultiplier : base.GetPlayerTroopsReceivedDamageMultiplier();
        }

        public override float GetCombatAIDifficultyMultiplier()
        {
            return BannerlordTweaksSettings.Instance is { } settings && settings.CombatAIDifficultyTweakEnabled ? settings.CombatAIDifficultyMultiplier : base.GetCombatAIDifficultyMultiplier();
        }

        public override float GetPlayerMapMovementSpeedBonusMultiplier()
        {
            return BannerlordTweaksSettings.Instance is { } settings && settings.PlayerMapMovementSpeedBonusTweakEnabled ? settings.PlayerMapMovementSpeedBonusMultiplier : base.GetPlayerMapMovementSpeedBonusMultiplier();
        }
    }
}
