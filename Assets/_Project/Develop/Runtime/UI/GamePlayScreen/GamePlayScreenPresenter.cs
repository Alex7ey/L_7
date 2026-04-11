using Assets._Project.Develop.Runtime.UI.CommonView;
using Assets._Project.Develop.Runtime.UI.ShopItem;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.UI.GamePlayScreen
{
    public class GamePlayScreenPresenter : IPresenter
    {
        private GamePlayScreenView _gamePlayScreenView;
        private GamePlayPresentersFactory _gamePlayPresentersFactory;
        ProjectPresentersFactory _projectPresentersFactory;

        private List<IPresenter> _childPresenters = new();

        public GamePlayScreenPresenter(
            ProjectPresentersFactory projectPresentersFactory,
            GamePlayScreenView gamePlayScreenView,         
            GamePlayPresentersFactory gamePlayPresentersFactory)
        {
            _gamePlayScreenView = gamePlayScreenView;
            _gamePlayPresentersFactory = gamePlayPresentersFactory;
            _projectPresentersFactory = projectPresentersFactory;
        }

        public void Dispose()
        {
            foreach (var presenter in _childPresenters)
                presenter.Dispose();

            _childPresenters.Clear();
        }

        public void Initialize()
        {
            CreateShopMineView();

            CreateWallet();
            CounterMine();

            foreach (var presenter in _childPresenters)
                presenter.Initialize();
        }

        private void CreateShopMineView()
        {
            ShopItemPresenter shopItemPresenter = _gamePlayPresentersFactory.CreateShopMinePresenter(_gamePlayScreenView.transform);

            _childPresenters.Add(shopItemPresenter);
        }

        private void CreateWallet()
        {
            WalletPresenter walletPresenter = _projectPresentersFactory.CreateWalletPresenter(_gamePlayScreenView.WalletView);

            _childPresenters.Add(walletPresenter);
        }

        private void CounterMine()
        {
            ItemQuantityPresenter itemQuantityPresenter = _projectPresentersFactory.CreateMineQuantityPresenter(_gamePlayScreenView.WalletView);

            _childPresenters.Add(itemQuantityPresenter);
        }
    }
}
