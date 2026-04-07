using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using TMPro;
using Assets._Project.Develop.Runtime.UI.CommonView;
using Assets._Project.Develop.Runtime.UI.MainMenuScreen;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Meta.Features.Statistics;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProvider;

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
    }
}
