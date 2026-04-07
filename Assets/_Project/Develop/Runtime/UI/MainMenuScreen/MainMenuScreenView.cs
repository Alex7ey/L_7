using Assets._Project.Develop.Runtime.UI.CommonView;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Develop.Runtime.UI
{
    public class MainMenuScreenView: MonoBehaviour, IView
    {
        [field: SerializeField] public IconTextListView WalletView { get; private set; }
        [field: SerializeField] public IconTextListView StatisticsView { get; private set; }

        [SerializeField] private Button _restartStatisticsButton;

        public event Action RestartStatisticsButtonClicked;
      
        private void OnEnable() => _restartStatisticsButton.onClick.AddListener(OnOpenPopupRestartStatistics);

        private void OnDisable() => _restartStatisticsButton.onClick.RemoveListener(OnOpenPopupRestartStatistics);

        private void OnOpenPopupRestartStatistics() => RestartStatisticsButtonClicked?.Invoke();
    }
}
