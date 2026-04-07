using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using TMPro;

namespace Assets._Project.Develop.Runtime.UI.CommonView
{
    public class TextFieldPresenter : IPresenter
    {
        private readonly IReadOnlyVariable<string> _text;

        public readonly TextMeshProUGUI View;

        private IDisposable _disposable;

        public TextFieldPresenter(IReadOnlyVariable<string> text, TextMeshProUGUI view)
        {
            _text = text;
            View = view;
        }

        public void Initialize()
        {
            UpdateValue(_text.Value);

            _disposable = _text.Subscribe(OnValueChange);
        }

        public void Dispose() => _disposable.Dispose();

        private void OnValueChange(string arg1, string newValue) => UpdateValue(newValue);

        private void UpdateValue(string value) => View.text = value.ToString();
    }
}
