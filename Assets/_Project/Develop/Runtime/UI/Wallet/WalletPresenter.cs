using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.CommonView;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
namespace Assets._Project.Develop.Runtime.UI
{
    public class WalletPresenter : IPresenter
    {
        private readonly IconTextListView _walletView;
        private readonly ViewsFactory _viewFactory;
        private readonly WalletService _walletService;
        private readonly ProjectPresentersFactory _projectPresentersFactory;

        private readonly List<CurrencyPresenter> _currencyPresenters = new();

        public WalletPresenter(
            IconTextListView view,
            ViewsFactory viewFactory,
            WalletService walletService,
            ProjectPresentersFactory projectPresentersFactory)
        {
            _walletView = view;
            _viewFactory = viewFactory;
            _walletService = walletService;
            _projectPresentersFactory = projectPresentersFactory;
        }

        public void Initialize()
        {
            foreach (var currencyType in _walletService.AvailableCurrencies)
            {
                IconTextView currencyView = _viewFactory.Create<IconTextView>(ViewIDs.CurrencyView);
                       
                _walletView.Add(currencyView);

                CurrencyPresenter currencyPresenter = _projectPresentersFactory.CreateCurrencyPresenter(_walletService.GetCurrency(currencyType), currencyType, currencyView);

                currencyPresenter.Initialize();
                _currencyPresenters.Add(currencyPresenter);
            }
        }

        public void Dispose()
        {
            foreach (var presenter in _currencyPresenters)
            {
                _walletView.Remove(presenter.View);
                _viewFactory.Release(presenter.View);
                presenter.Dispose();
            }
        }
    }
}
