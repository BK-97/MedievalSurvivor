using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class FeedbackPanel : PanelBase
{
    public static StringEvent OnFeedbackOpen = new StringEvent();
    public static UnityEvent OnFeedbackClose = new UnityEvent();
    public TextMeshProUGUI feedbackText;

    private void OnEnable()
    {
        HidePanel();

        GameManager.Instance.OnStageFail.AddListener(HidePanel);
        OnFeedbackOpen.AddListener(GiveFeedback);
        OnFeedbackClose.AddListener(HidePanel);
        SceneController.Instance.OnSceneUnLoaded.AddListener(HidePanel);
    }
    private void OnDisable()
    {
        GameManager.Instance.OnStageFail.RemoveListener(HidePanel);
        OnFeedbackOpen.RemoveListener(GiveFeedback);
        OnFeedbackClose.RemoveListener(HidePanel);
        SceneController.Instance.OnSceneUnLoaded.RemoveListener(HidePanel);
    }
    private void GiveFeedback(string feedbackString)
    {
        ShowPanel();
        string upperFeedback = feedbackString.ToUpper();
        feedbackText.text = upperFeedback;
    }
}
