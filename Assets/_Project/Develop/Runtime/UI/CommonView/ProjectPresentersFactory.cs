using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Configs.Shop;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.Inventory;
using Assets._Project.Develop.Runtime.Meta.Features.LevelsProgression;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.CommonView;
using Assets._Project.Develop.Runtime.UI.LevelsMenuPopup;
using Assets._Project.Develop.Runtime.UI.ShopItem;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using TMPro;

namespace Assets._Project.Develop.Runtime.UI
{
    public class ProjectPresentersFactory
    {
        private readonly DIContainer _container;

        public ProjectPresentersFactory(DIContainer container) => _container = container;

        public WalletPresenter CreateWalletPresenter(IconTextListView iconTextListView)
        {
            return new WalletPresenter(iconTextListView, _container.Resolve<ViewsFactory>(), _container.Resolve<WalletService>(), this);
        }

        public CurrencyPresenter CreateCurrencyPresenter(IReadOnlyVariable<int> currency, CurrencyTypes currencyType, IconTextView view)
        {
            return new CurrencyPresenter(currency, currencyType, _container.Resolve<ConfigsProviderService>().GetConfig<CurrencyIconsConfig>(), view);
        }

        public TextFieldPresenter CreateTextFieldPresenter(IReadOnlyVariable<string> text, TextMeshProUGUI view) => new TextFieldPresenter(text, view);

        public LevelTilePresenter CreateLevelTilePresenter(LevelTileView view, int levelNumber)
        {
            return new LevelTilePresenter(
                _container.Resolve<LevelsProgressionService>(),
                _container.Resolve<SceneSwitcherService>(),
                _container.Resolve<ICoroutinesPerformer>(),
                levelNumber,
                view);
        }

        public LevelsMenuPopupPresenter CreateLevelsMenuPopupPresenter(LevelsMenuPopupView view)
        {
            return new LevelsMenuPopupPresenter(
                _container.Resolve<ICoroutinesPerformer>(),
                _container.Resolve<ConfigsProviderService>(),
                this,
                _container.Resolve<ViewsFactory>(),
                view);
        }

        public ItemQuantityPresenter CreateMineQuantityPresenter(IconTextListView iconTextListView)
        {
            return new ItemQuantityPresenter(
                _container.Resolve<ConfigsProviderService>().GetConfig<ShopMineConfig>(),
                _container.Resolve<InventoryService>(),
                _container.Resolve<ViewsFactory>(),
                iconTextListView
                );
        }
    }
}
