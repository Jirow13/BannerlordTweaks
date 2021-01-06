using HarmonyLib;
using TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper;

// Retired in 1.5.6 - Remote Companion Mgmt is part of the base game now.

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(SkillVM), "InitializeValues")]
    public class SkillVMPatch
    {
        static void Postfix(ref bool ____isInSamePartyAsPlayer)
        {
            ____isInSamePartyAsPlayer = true;
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.RemoteCompanionSkillManagementEnabled;
    }
}
