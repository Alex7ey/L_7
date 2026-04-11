using Assets._Project.Develop.Runtime.UI.Popups;
using Assets._Project.Develop.Runtime.UI.UIRoot;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.MainMenuScreen
{
    public class MainMenuPopupService : PopupService
    {
        private readonly MainMenuUIRoot _uiRoot;

        public MainMenuPopupService(ViewsFactory viewsFactory, 
            ProjectPresentersFactory presentersFactory, 
            MainMenuUIRoot uiRoot) : base(viewsFactory, presentersFactory)
        {
            _uiRoot = uiRoot;
        }

        protected override Transform PopupLayer => _uiRoot.PopupsLayer;
    }
}
