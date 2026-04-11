using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Shop
{
    public abstract class ShopItemConfig : ScriptableObject
    {
        [field: SerializeField] public int Price { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public CurrencyTypes CurrencyType { get; private set; }
    }
}
