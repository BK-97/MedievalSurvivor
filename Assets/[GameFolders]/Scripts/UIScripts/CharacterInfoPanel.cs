using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoPanel : PanelBase
{
    private void OnEnable()
    {
        HidePanel();

        LevelManager.Instance.OnLevelStart.AddListener(ShowPanel);
        SceneController.Instance.OnSceneUnLoaded.AddListener(HidePanel);
        GameManager.Instance.OnStageFail.AddListener(HidePanel);
    }
    private void OnDisable()
    {
        LevelManager.Instance.OnLevelStart.RemoveListener(ShowPanel);
        SceneController.Instance.OnSceneUnLoaded.AddListener(HidePanel);
        GameManager.Instance.OnStageFail.RemoveListener(HidePanel);
    }
}
