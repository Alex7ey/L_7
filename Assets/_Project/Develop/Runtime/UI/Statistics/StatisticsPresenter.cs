using Assets._Project.Develop.Runtime.Meta.Features.Statistics;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.CommonView;
using System.Collections.Generic;
using UnityEngine;


namespace Assets._Project.Develop.Runtime.UI
{
    public class StatisticsPresenter : IPresenter
    {
        private readonly IconTextListView _iconTextListView;
        private readonly ViewsFactory _viewFactory;
        private readonly PlayerStatisticsService _playerStatisticsService;
        private readonly ProjectPresentersFactory _projectPresentersFactory;

        private List<StatisticsItemPresenter> _statisticsItemPresenters = new();

        public StatisticsPresenter(IconTextListView iconsTextListView, ViewsFactory viewFactory, PlayerStatisticsService playerStatisticsService, ProjectPresentersFactory projectPresentersFactory)
        {
            _iconTextListView = iconsTextListView;
            _viewFactory = viewFactory;
            _playerStatisticsService = playerStatisticsService;
            _projectPresentersFactory = projectPresentersFactory;
        }

        public void Dispose()
        {
            foreach (var presenter in _statisticsItemPresenters)
            {
                _iconTextListView.Remove(presenter.View);
                _viewFactory.Release(presenter.View);
                presenter.Dispose();
            }
        }

        public void Initialize()
        {
            foreach (var statisticsItem in _playerStatisticsService.AvailableStatisticsItems)
            {
                IconTextView statisticsItemView = _viewFactory.Create<IconTextView>(ViewIDs.IconTextView, _iconTextListView.transform);

                _iconTextListView.Add(statisticsItemView);

                StatisticsItemPresenter statisticsItemPresenter =
                _projectPresentersFactory.CreateStatisticsItemPresenter(_playerStatisticsService.GetStats(statisticsItem), statisticsItem, statisticsItemView);

                statisticsItemPresenter.Initialize();
                _statisticsItemPresenters.Add(statisticsItemPresenter);
            }
        }
    }
}
