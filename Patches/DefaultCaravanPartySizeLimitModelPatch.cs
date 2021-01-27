using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Party;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultPartySizeLimitModel), "CalculateMobilePartyMemberSizeLimit")]
    //[HarmonyPatch(typeof(DefaultPartySizeLimitModel), "GetPartyMemberSizeLimit")]
    public class DefaultCaravanPartySizeLimitModelPatch
    {
        //static void Postfix(MobileParty party, StatExplainer explanation, ref int __result)
        static void Postfix(MobileParty party, ref ExplainedNumber __result)
        {
            if (party.IsCaravan && party.Party?.Owner != null && party.Party.Owner == Hero.MainHero && BannerlordTweaksSettings.Instance is { } settings)
            {
                float num = settings.PlayerCaravanPartySize;
                float num2 = __result.ResultNumber;
                float num3 = num - num2;
                __result.Add((int)Math.Ceiling(num3), null);
            }
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.PlayerCaravanPartySizeTweakEnabled;
    }
}
