using Assets._Project.Develop.Runtime.Configs.Shop;
using Assets._Project.Develop.Runtime.Meta.Features.Inventory;
using Assets._Project.Develop.Runtime.UI.CommonView;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;

namespace Assets._Project.Develop.Runtime.UI.ShopItem
{
    public class ItemQuantityPresenter : IPresenter
    {
        private readonly ShopItemConfig _shopItemConfig;
        private readonly InventoryService _inventoryService;
        private readonly ViewsFactory _viewsFactory;

        public readonly IconTextView View;

        private IDisposable _disposable;
        private ReactiveVariable<int> _count;


        public ItemQuantityPresenter(ShopItemConfig shopItemConfig, InventoryService inventoryService, ViewsFactory viewsFactory, IconTextListView iconTextListView)
        {
            _shopItemConfig = shopItemConfig;
            _inventoryService = inventoryService;
            _viewsFactory = viewsFactory;

            View = _viewsFactory.Create<IconTextView>(ViewIDs.IconTextView, iconTextListView.transform);
        }

        public void Initialize()
        {
            if (_inventoryService.HasItem(_shopItemConfig.Name) == false)
                return;

            _count = _inventoryService.Inventory[_shopItemConfig.Name];

            View.SetIcon(_shopItemConfig.Sprite);
            View.SetText(_count.Value.ToString() + "x");

            _disposable = _count.Subscribe((int oldValue, int newValue) => View.SetText(newValue.ToString() + "x"));
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
