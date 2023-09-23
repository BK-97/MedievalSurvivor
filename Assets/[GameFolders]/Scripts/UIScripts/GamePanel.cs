using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        HidePanel();

    }
    private void OnEnable()
    {
        LevelManager.Instance.OnLevelStart.AddListener(ShowPanel);
        GameManager.Instance.OnStageFail.AddListener(HidePanel);
    }
    private void OnDisable()
    {
        LevelManager.Instance.OnLevelStart.RemoveListener(ShowPanel);
        GameManager.Instance.OnStageFail.RemoveListener(HidePanel);
    }
    void ShowPanel()
    {
        canvasGroup.alpha = 1;
    }
    void HidePanel()
    {
        canvasGroup.alpha = 0;
    }
}
