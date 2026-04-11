using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Develop.Runtime.UI.ShopItem
{
    public class ShopItemView : MonoBehaviour, IView
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private Image _currencyImage;

        [SerializeField] private TMP_Text _priceText;

        [SerializeField] public Button ShopButton;

        public event Action ShopButtonClicked;

        public void SetText(string text) => _priceText.text = text;

        public void SetItemImage(Sprite icon) => _itemImage.sprite = icon;

        public void SetCurrencyImage(Sprite icon) => _currencyImage.sprite = icon;

        private void OnShopButtonClick() => ShopButtonClicked?.Invoke();

        private void OnEnable() => ShopButton.onClick.AddListener(OnShopButtonClick);

        private void OnDisable() => ShopButton.onClick.RemoveListener(OnShopButtonClick);
    }
}
