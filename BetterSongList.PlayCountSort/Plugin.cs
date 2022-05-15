using BetterSongList.PlayCountSort.Sorters;
using IPA;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
        }

        [OnStart]
        public void OnStart()
        {
            if (SortMethods.RegisterPrimitiveSorter(new PlayCountSorter()))
                Log.Info("Registered PlayCountSorter!");
            else
                Log.Warn("Failed to register PlayCountSorter!");
        }

        // Just to make BSIPA happy, I would deregister if a function for it was provided
        [OnExit]
        public void OnExit()
        {
        }
    }
}
