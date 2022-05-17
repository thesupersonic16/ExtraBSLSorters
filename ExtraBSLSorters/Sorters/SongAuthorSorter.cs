using BetterSongList.Interfaces;
using BetterSongList.SortModels;
using BetterSongList.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExtraBSLSorters.Sorters
{
    public class SongAuthorSorter : ITransformerPlugin, ISorterCustom, ISorterWithLegend, IComparer<IPreviewBeatmapLevel>
    {
        public string name => "Artist Name";

        public bool visible => PluginConfig.Instance.EnableSongAuthorSorter;

        public bool isReady => true;

        public Task Prepare(CancellationToken cancelToken) => Task.CompletedTask;

        public IEnumerable<KeyValuePair<string, int>> BuildLegend(IPreviewBeatmapLevel[] levels) =>
            SongListLegendBuilder.BuildFor(levels, (level) =>
            {
                return level.songAuthorName.Length > 0 ? level.songAuthorName.Substring(0, 1) : null;
            });

        public void DoSort(ref IEnumerable<IPreviewBeatmapLevel> levels, bool ascending)
        {
            levels = ascending ? levels.OrderBy(x => x, this) : levels.OrderByDescending(x => x, this);
        }

        public int Compare(IPreviewBeatmapLevel x, IPreviewBeatmapLevel y) => string.Compare(x.songAuthorName, y.songAuthorName);

        public void ContextSwitch(SelectLevelCategoryViewController.LevelCategory levelCategory, IAnnotatedBeatmapLevelCollection playlist) { }
    }
}
