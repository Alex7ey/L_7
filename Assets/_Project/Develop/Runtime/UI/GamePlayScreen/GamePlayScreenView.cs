using Assets._Project.Develop.Runtime.UI.CommonView;
using System;
using TMPro;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.GamePlayScreen
{
    public class GamePlayScreenView : MonoBehaviour, IView
    {
        [field: SerializeField] public IconTextListView WalletView { get; private set; }
    }
}
