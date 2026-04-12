using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Configs.Shop;
using Assets._Project.Develop.Runtime.Meta.Features.Inventory;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.ShopItem
{
    public class ShopItemPresenter : IPresenter, IDisposable
    {
        private readonly int _price;
        private readonly Sprite _itemIcon;
        private readonly Sprite _currencyIcon;
        private readonly CurrencyTypes _currencyType;

        private readonly ShopItemView View;

        private readonly WalletService _walletService;
        private readonly InventoryService _inventoryService;
        private readonly PlayerDataProvider _playerDataProvider;
        private readonly ICoroutinesPerformer _coroutinesPerformer;

        IDisposable _dispose;

        public ShopItemPresenter(
            ShopItemView view,
            ShopItemConfig config,
            CurrencyIconsConfig currencyIconsConfig,
            WalletService walletService,
            InventoryService inventoryService,
            PlayerDataProvider playerDataProvider,
            ICoroutinesPerformer coroutinesPerformer)
        {
            _walletService = walletService;
            _inventoryService = inventoryService;
            _playerDataProvider = playerDataProvider;
            _coroutinesPerformer = coroutinesPerformer;

            View = view;
            _itemIcon = config.Sprite;
            _currencyIcon = currencyIconsConfig.GetSpriteFor(config.CurrencyType);
            _price = config.Price;
            _currencyType = config.CurrencyType;

            _dispose = _walletService.GetCurrency(_currencyType).Subscribe(BallanceChange);
        }

        public void Initialize()
        {
            View.ShopButtonClicked += Shop;

            View.SetCurrencyImage(_currencyIcon);
            View.SetItemImage(_itemIcon);
            View.SetText($"Купить за " + _price);

            BallanceChange(0, _price);
        }

        public void Shop()
        {
            if (_walletService.Enough(_currencyType, _price))
            {
                _walletService.Spend(_currencyType, _price);
                _inventoryService.Add("Mine", 1);

                _coroutinesPerformer.StartPerform(_playerDataProvider.SaveAsync());
            }
        }

        private void BallanceChange(int oldValue, int newValue)
            => View.ShopButton.interactable = _walletService.Enough(_currencyType, _price);

        public void Dispose()
        {
            View.ShopButtonClicked -= Shop;
            _dispose.Dispose();
        }
    }
}
