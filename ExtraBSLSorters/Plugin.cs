using BeatSaberMarkupLanguage.Settings;
using ExtraBSLSorters.Sorters;
using ExtraBSLSorters.UI.ViewControllers;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;

namespace ExtraBSLSorters
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }

        [Init]
        public void Init(IPALogger logger, Config config)
        {
            Instance = this;
            Log = logger;
            PluginConfig.Instance = config.Generated<PluginConfig>();
        }

        [OnStart]
        public void OnStart()
        {
            BSMLSettings.instance.AddSettingsMenu("ExtraBSLSorters", "ExtraBSLSorters.UI.Views.SettingsView.bsml", SettingsViewController.instance);
            BSLSorters.Init();
        }

        // Just to make BSIPA happy, I would deregister if a function for it was provided
        [OnExit]
        public void OnExit()
        {
        }
    }
}
