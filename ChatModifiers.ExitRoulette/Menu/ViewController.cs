using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using ChatModifiers.API;
using Zenject;

namespace ExitRoulette.Menu
{
    [ViewDefinition("ExitRoulette.Menu.view.bsml")]
    internal class ViewController : BSMLAutomaticViewController, IInitializable
    {
        [UIValue("secondsSettingValue")]
        public int secondsSettingValue
        {
            get
            {
                if (ExitRouletteModifier.customModifier != null)
                {
                    // My disdain for this line is profound
                    return int.Parse(ExitRouletteModifier.customModifier.ModifierSettings.AdditionalSettings["SecondsActive"].ToString());
                }
                return 5;
            }
            set
            {
                if (ExitRouletteModifier.customModifier != null)
                {
                    ExitRouletteModifier.customModifier.ModifierSettings.AdditionalSettings["SecondsActive"] = (object)value;
                    ExitRouletteModifier.customModifier.SaveSettings();
                }
            }
        }

        [UIValue("chanceSettingValue")]
        public int chanceSettingValue
        {
            get
            {
                if (ExitRouletteModifier.customModifier != null)
                {
                    // My disdain for this line is profound
                    return int.Parse(ExitRouletteModifier.customModifier.ModifierSettings.AdditionalSettings["Chance"].ToString());
                }
                return 5;
            }
            set
            {
                if (ExitRouletteModifier.customModifier != null)
                {
                    ExitRouletteModifier.customModifier.ModifierSettings.AdditionalSettings["Chance"] = (object)value;
                    ExitRouletteModifier.customModifier.SaveSettings();
                }
            }
        }

        public void Initialize()
        {
            ExitRouletteModifier.customModifier.SettingsViewController = this;
            ExitRouletteModifier.customModifier.Register();
        }
    }
}
