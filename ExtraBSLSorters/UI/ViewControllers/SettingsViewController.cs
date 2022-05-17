using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using System;
using System.Collections.Generic;

namespace ExtraBSLSorters.UI.ViewControllers
{
    [HotReload(RelativePathToLayout = @"SettingsView.bsml")]
    [ViewDefinition("ExtraBSLSorters.UI.Views.SettingsView.bsml")]
    internal class SettingsViewController : NotifiableSingleton<SettingsViewController>
    {
        [UIValue("enablePlayCountSorter")]
        private bool EnablePlayCountSorter
        {
            get => PluginConfig.Instance.EnablePlayCountSorter;
            set
            {
                PluginConfig.Instance.EnablePlayCountSorter = value;
                NotifyPropertyChanged(nameof(EnablePlayCountSorter));
            }
        }

        [UIValue("enableSongAuthorSorter")]
        private bool EnableSongAuthorSorter
        {
            get => PluginConfig.Instance.EnableSongAuthorSorter;
            set
            {
                PluginConfig.Instance.EnableSongAuthorSorter = value;
                NotifyPropertyChanged(nameof(EnableSongAuthorSorter));
            }
        }
    }
}
