using System;
using UnityEngine;
using ScreenSystem.Page;
using UnityEngine.UI;
using UniRx;
using TMPro;

public class TestPageView : PageViewBase
{
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private TextMeshProUGUI _modalCountText;
    [SerializeField] private Button _nextPageButton;
    [SerializeField] private Button _nextModalButton;

    public IObservable<Unit> OnClickPage => _nextPageButton.OnClickAsObservable();
    public IObservable<Unit> OnClickModal => _nextModalButton.OnClickAsObservable();

    public void SetView(TestPageModel model)
    {
        _messageText.SetText(model.TestMessage);
        UpdateModalCount(0);
    }

    public void UpdateModalCount(int count)
    {
        _modalCountText.SetText($"Modal Count: {count}");
    }
}