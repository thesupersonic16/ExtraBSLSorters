using BetterSongList;
using BetterSongList.PlayCountSort.Sorters;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;

namespace BetterSongList.PlayCountSort
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }

        [Init]
        public void Init(IPALogger logger)
        {
            Instance = this;
            Log = logger;
            Log.Info("BetterSongList.PlayCountSort initialized.");
        }

        [OnStart]
        public void OnApplicationStart()
        {
            SortMethods.RegisterPrimitiveSorter(new PlayCountSorter());
        }
    }
}
