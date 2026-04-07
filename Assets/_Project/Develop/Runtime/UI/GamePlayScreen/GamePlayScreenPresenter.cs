using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.UI.GamePlayScreen
{
    public class GamePlayScreenPresenter : IPresenter
    {
        private ProjectPresentersFactory _projectPresentersFactory;
        private GamePlayScreenView _gamePlayScreenView;

        private List<IPresenter> _childPresenters = new();

        public GamePlayScreenPresenter(
             ProjectPresentersFactory projectPresentersFactory,
            GamePlayScreenView gamePlayScreenView)
        {
            _projectPresentersFactory = projectPresentersFactory;
            _gamePlayScreenView = gamePlayScreenView;
        }

        public void Dispose()
        {
            foreach (var presenter in _childPresenters)
                presenter.Dispose();

            _childPresenters.Clear();
        }

        public void Initialize()
        {
            foreach (var presenter in _childPresenters)
                presenter.Initialize();
        }
    }
}
