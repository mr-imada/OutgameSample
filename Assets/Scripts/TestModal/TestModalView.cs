using UnityEngine;
using UnityEngine.UI;
using ScreenSystem.Modal;
using UniRx;
using System;
using TMPro;

public class TestModalView : ModalViewBase
{
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private TextMeshProUGUI _messageText;

    public IObservable<Unit> OnNext => _nextButton.OnClickAsObservable();
    public IObservable<Unit> OnClose => _closeButton.OnClickAsObservable();

    public void SetView(TestModalModel model)
    {
        _messageText.SetText(model.TestMessage);
    }
}
