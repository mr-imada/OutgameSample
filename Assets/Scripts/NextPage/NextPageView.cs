using System;
using UnityEngine;
using ScreenSystem.Page;
using UnityEngine.UI;
using UniRx;
using TMPro;

public class NextPageView : PageViewBase
{
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private Button _returnButton;

    public IObservable<Unit> OnClickReturn => _returnButton.OnClickAsObservable();

    public void SetView(NextPageModel model)
    {
        _messageText.SetText(model.NextMessage);
    }
}