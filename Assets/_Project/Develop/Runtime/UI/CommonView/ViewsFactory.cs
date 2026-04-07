using Assets._Project.Develop.Runtime.Utilities.AssetsLoader;
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets._Project.Develop.Runtime.UI
{
    public class ViewsFactory
    {
        private ResourcesAssetsLoader _resourceAssetsLoader;

        private Dictionary<string, string> _viewIDToResourcesPath = new()
        {
            {ViewIDs.CurrencyView, "UI/Wallet/CurrencyView" },
            {ViewIDs.StatisticsItemView,"UI/Score/ScoreView" },
            {ViewIDs.MainMenuScreen, "UI/MainMenu/MainMenuScreenView"},
            {ViewIDs.GamePlayScreen, "UI/GamePlay/GamePlayScreenView" },
            {ViewIDs.ResetStatisticsPopupView, "UI/MainMenu/PopupResetStatistics" }
        };

        public ViewsFactory(ResourcesAssetsLoader resourceAssetsLoader)
        {
            _resourceAssetsLoader = resourceAssetsLoader;
        }

        public TView Create<TView>(string viewID, Transform parent = null) where TView : MonoBehaviour, IView
        {
            if (_viewIDToResourcesPath.TryGetValue(viewID, out string resourcePath) == false)
                throw new ArgumentException($"You didn't set resource path for {typeof(TView)}, searched id: {viewID}");

            GameObject prefab = _resourceAssetsLoader.Load<GameObject>(resourcePath);
            GameObject instance = Object.Instantiate(prefab, parent);

            if (instance.TryGetComponent(out TView view) == false)
                throw new InvalidOperationException($"Not found {typeof(TView)} component on view instance");

            return view;
        }

        public void Release<TView>(TView view) where TView : MonoBehaviour, IView
        {
            Object.Destroy(view.gameObject);
        }
    }

}
