using BetterSongList.Interfaces;
using BetterSongList.SortModels;
using BetterSongList.Util;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace BetterSongList.PlayCountSort.Sorters
{
    public class PlayCountSorter : ITransformerPlugin, ISorterPrimitive, ISorterWithLegend
    {
        public string name => "Play Count";

        public bool visible => true;

        public bool isReady => _playerDataModel != null;

        private PlayerDataModel _playerDataModel;

        public Task Prepare(CancellationToken cancelToken) => Prepare();
        public Task Prepare()
        {
            var objects = Resources.FindObjectsOfTypeAll<PlayerDataModel>();
            if (objects != null)
                _playerDataModel = objects.FirstOrDefault();
            return Task.CompletedTask;
        }

        public int GetPlayCount(IPreviewBeatmapLevel level)
        {
            if (_playerDataModel == null)
                Prepare();

            if (level == null)
                return -1;
            
            int count = 0;
            foreach (var statsData in _playerDataModel.playerData.levelsStatsData.Where(t => t.levelID == level.levelID))
                count += statsData.playCount;
            return count;
        }

        public IEnumerable<KeyValuePair<string, int>> BuildLegend(IPreviewBeatmapLevel[] levels) =>
            SongListLegendBuilder.BuildFor(levels, (level) =>
        {
            return $"{GetPlayCount(level)}";
        });

        public float? GetValueFor(IPreviewBeatmapLevel level)
        {
            return GetPlayCount(level);
        }

        public void ContextSwitch(SelectLevelCategoryViewController.LevelCategory levelCategory, IAnnotatedBeatmapLevelCollection playlist) { }
    }
}
