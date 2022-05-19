using BetterSongList.Interfaces;
using BetterSongList.SortModels;
using BetterSongList.Util;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace ExtraBSLSorters.Sorters
{
    public class PlayCountSorter : ITransformerPlugin, ISorterPrimitive, ISorterWithLegend
    {
        public string name => "Play Count";

        public bool visible => PluginConfig.Instance.EnablePlayCountSorter;

        public bool isReady => _playerDataModel != null;

        private PlayerDataModel _playerDataModel;
        private Dictionary<string, int> _playCounts;

        public Task Prepare(CancellationToken cancelToken) => Prepare();
        public Task Prepare()
        {
            var objects = Resources.FindObjectsOfTypeAll<PlayerDataModel>();
            if (objects != null)
                _playerDataModel = objects.FirstOrDefault();

            if (_playerDataModel == null)
                return Task.CompletedTask;

            return Task.CompletedTask;
        }

        public void UpdatePlayCounts()
        {
            if (_playerDataModel == null)
                Prepare();
            
            _playCounts = new Dictionary<string, int>();
            foreach (var statsData in _playerDataModel.playerData.levelsStatsData)
            {
                if (_playCounts.ContainsKey(statsData.levelID))
                {
                    _playCounts[statsData.levelID] += statsData.playCount;
                }
                else
                {
                    _playCounts.Add(statsData.levelID, statsData.playCount);
                }
            }
        }

        public int GetPlayCount(IPreviewBeatmapLevel level)
        {
            if (_playCounts == null || _playerDataModel == null)
                UpdatePlayCounts();

            if (level == null)
                return -1;
            
            return _playCounts.ContainsKey(level.levelID) ? _playCounts[level.levelID] : 0;
        }

        public IEnumerable<KeyValuePair<string, int>> BuildLegend(IPreviewBeatmapLevel[] levels) =>
            SongListLegendBuilder.BuildFor(levels, (level) =>
        {
            int count = GetPlayCount(level);
            if (count == 0)
                return "Never";
            return count.ToString();
        });

        public float? GetValueFor(IPreviewBeatmapLevel level)
        {
            return GetPlayCount(level);
        }

        public void ContextSwitch(SelectLevelCategoryViewController.LevelCategory levelCategory,
            IAnnotatedBeatmapLevelCollection playlist) => UpdatePlayCounts();
    }
}
