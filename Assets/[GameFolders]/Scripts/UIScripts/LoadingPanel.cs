using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class LoadingPanel : PanelBase
{
    public VideoPlayer videoPlayer;
    private void OnEnable()
    {
        HidePanel();
        SceneController.Instance.OnSceneTransitionStart.AddListener(ShowPanel);
        SceneController.Instance.OnSceneLoaded.AddListener(HidePanel);
    }
    private void OnDisable()
    {
        SceneController.Instance.OnSceneTransitionStart.RemoveListener(ShowPanel);
        SceneController.Instance.OnSceneLoaded.RemoveListener(HidePanel);
    }
    public override void ShowPanel()
    {
        base.ShowPanel();
        videoPlayer.Play();
    }
    public override void HidePanel()
    {
        base.HidePanel();
        videoPlayer.Pause();
    }
}
