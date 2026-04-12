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
    }
}
