using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInfoPanel : PanelBase
{
    private void OnEnable()
    {
        HidePanel();
        GameManager.Instance.OnStageFail.AddListener(HidePanel);
    }
    private void OnDisable()
    {
        GameManager.Instance.OnStageFail.RemoveListener(HidePanel);
    }
}
