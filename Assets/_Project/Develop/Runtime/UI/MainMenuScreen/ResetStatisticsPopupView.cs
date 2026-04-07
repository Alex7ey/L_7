using Assets._Project.Develop.Runtime.UI.CommonView;
using Assets._Project.Develop.Runtime.UI.Popups;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Develop.Runtime.UI.MainMenuScreen
{
    public class ResetStatisticsPopupView : PopupViewBase, IView
    {
        [SerializeField] public TextMeshProUGUI _text;
        [SerializeField] public Button Button;

        [SerializeField] private IconTextView _iconTextView;

        public void SetIconTextView(int cost, Sprite icon)
        {
            _iconTextView.SetIcon(icon);
            _iconTextView.SetText(cost.ToString());
        }
    }
}
