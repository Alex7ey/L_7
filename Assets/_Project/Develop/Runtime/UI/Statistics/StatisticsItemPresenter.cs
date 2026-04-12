using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Meta.Features.Statistics;
using Assets._Project.Develop.Runtime.UI.CommonView;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;

namespace Assets._Project.Develop.Runtime.UI
{
    public class StatisticsItemPresenter : IPresenter
    {
        private readonly IReadOnlyVariable<int> _statisticsItems;
        private readonly StatisticsItemTypes _statisticsItemTypes;
        private readonly StatisticsIconsConfig _statisticsIconsConfig;

        public readonly IconTextView View;

        private IDisposable _disposable;

        public StatisticsItemPresenter(IReadOnlyVariable<int> statisticsItems, StatisticsItemTypes statisticsItemTypes, StatisticsIconsConfig statisticsIconsConfig, IconTextView view)
        {
            _statisticsItems = statisticsItems;
            _statisticsItemTypes = statisticsItemTypes;
            _statisticsIconsConfig = statisticsIconsConfig;
            View = view;
        }

        public void Initialize()
        {
            UpdateValue(_statisticsItems.Value);
            View.SetIcon(_statisticsIconsConfig.GetSpriteFor(_statisticsItemTypes));

            _disposable = _statisticsItems.Subscribe(OnCurrencyChange);
        }

        public void Dispose() => _disposable.Dispose();

        private void OnCurrencyChange(int arg1, int newValue) => UpdateValue(newValue);

        private void UpdateValue(int value) => View.SetText(value.ToString());
    }
}
