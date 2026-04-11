using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.CommonView
{
    public class ElementsListView<TElement> : MonoBehaviour, IView where TElement : MonoBehaviour, IView
    {
        [SerializeField] private Transform _parent;

        private List<TElement> _elements = new();

        public IReadOnlyList<TElement> Elements => _elements;

        public void Add(TElement element)
        {
            element.transform.SetParent(_parent);
            _elements.Add(element);
        }

        public void Remove(TElement element)
        {
            if (_elements.Contains(element) == false)
                throw new ArgumentException($"{nameof(element)} not in the list of items");

            element.transform.SetParent(null);
            _elements.Remove(element);
        }
    }
}
