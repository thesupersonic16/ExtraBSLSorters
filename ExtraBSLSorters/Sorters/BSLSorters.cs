using BetterSongList;
using BetterSongList.Interfaces;
using BetterSongList.SortModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraBSLSorters.Sorters
{
    public static class BSLSorters
    {
        internal static void Init()
        {
            if (SortMethods.RegisterPrimitiveSorter(new PlayCountSorter()))
                Plugin.Log.Info("Registered PlayCountSorter!");
            else
                Plugin.Log.Warn("Failed to register PlayCountSorter!");
            if (SortMethods.RegisterCustomSorter(new SongAuthorSorter()))
                Plugin.Log.Info("Registered SongAuthorSorter!");
            else
                Plugin.Log.Warn("Failed to register SongAuthorSorter!");
        }
    }
}
