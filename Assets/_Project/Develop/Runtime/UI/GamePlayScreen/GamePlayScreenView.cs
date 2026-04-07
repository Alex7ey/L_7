using System;
using TMPro;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.GamePlayScreen
{
    public class GamePlayScreenView : MonoBehaviour, IView
    {
        [field: SerializeField] public TextMeshProUGUI ExpectedText { get; private set; }
        [field: SerializeField] public TextMeshProUGUI PlayerInputText { get; private set; }
        [field: SerializeField] public TextMeshProUGUI InformationText { get; private set; }
    }
}
