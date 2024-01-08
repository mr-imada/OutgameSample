using System;
using UnityEngine;
using ScreenSystem.Page;
using UnityEngine.UI;
using UniRx;
using TMPro;

public class FirstPageView : PageViewBase
{
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private TextMeshProUGUI _modalCountText;
    [SerializeField] private Button _nextPageButton;
    [SerializeField] private Button _nextModalButton;

    public IObservable<Unit> OnClickPage => _nextPageButton.OnClickAsObservable();
    public IObservable<Unit> OnClickModal => _nextModalButton.OnClickAsObservable();

    public void SetView(FirstPageModel model)
    {
        _messageText.SetText(model.FirstPageMessage);
        UpdateModalCount(0);
    }

    public void UpdateModalCount(int count)
    {
        _modalCountText.SetText($"Modal Count: {count}");
    }
}