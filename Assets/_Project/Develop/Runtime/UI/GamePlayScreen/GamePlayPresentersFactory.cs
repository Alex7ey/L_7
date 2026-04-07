using Assets._Project.Develop.Runtime.Gameplay.Features;
using Assets._Project.Develop.Runtime.Infrastructure.DI;

namespace Assets._Project.Develop.Runtime.UI.GamePlayScreen
{
    public class GamePlayPresentersFactory
    {
        private DIContainer _container;

        public GamePlayPresentersFactory(DIContainer container)
        {
            _container = container;
        }     
    }
}
