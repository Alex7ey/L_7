using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.CommonView;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;

namespace Assets._Project.Develop.Runtime.UI
{
    public class CurrencyPresenter : IDisposable
    {
        private readonly IReadOnlyVariable<int> _currency;
        private readonly CurrencyTypes _currencyType;
        private readonly CurrencyIconsConfig _currencyIconsConfig;

        public readonly IconTextView View;

        private IDisposable _disposable;

        public CurrencyPresenter(
           IReadOnlyVariable<int> currency,
           CurrencyTypes currencyType,
           CurrencyIconsConfig currencyIconsConfig,
           IconTextView view)
        {
            _currency = currency;
            _currencyType = currencyType;
            _currencyIconsConfig = currencyIconsConfig;
            View = view;
        }

        public void Initialize()
        {
            UpdateValue(_currency.Value);
            View.SetIcon(_currencyIconsConfig.GetSpriteFor(_currencyType));

            _disposable = _currency.Subscribe(OnCurrencyChange);
        }

        public void Dispose() => _disposable.Dispose();

        private void OnCurrencyChange(int arg1, int newValue) => UpdateValue(newValue);

        private void UpdateValue(int value) => View.SetText(value.ToString());
    }
}
