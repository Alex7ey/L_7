using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Configs.Shop;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.Inventory;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.ShopItem;
using Assets._Project.Develop.Runtime.Utilities.Cleanup;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.GamePlayScreen
{
    public class GamePlayPresentersFactory
    {
        private readonly DIContainer _container;

        public GamePlayPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public ShopItemPresenter CreateShopMinePresenter(Transform parent)
        {
            return new ShopItemPresenter(
                _container.Resolve<ViewsFactory>().Create<ShopItemView>(ViewIDs.ShopItemView, parent),
                _container.Resolve<ConfigsProviderService>().GetConfig<ShopMineConfig>(),
                _container.Resolve<ConfigsProviderService>().GetConfig<CurrencyIconsConfig>(),
                _container.Resolve<WalletService>(),
                _container.Resolve<InventoryService>(),
                _container.Resolve<PlayerDataProvider>(),
                _container.Resolve<ICoroutinesPerformer>()
                );
        }
    }
}
