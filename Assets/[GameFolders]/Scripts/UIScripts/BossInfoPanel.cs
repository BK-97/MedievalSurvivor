using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class BossInfoPanel : PanelBase
{
    public Slider HealthBar;
    public TextMeshProUGUI bossName;
    private EnemyHealthController _cachedHealthController;
    private void OnEnable()
    {
        HidePanel();
        GameManager.Instance.OnStageFail.AddListener(HidePanel);
        Spawner.OnBossSpawned.AddListener(SetBar);
    }
    private void OnDisable()
    {
        GameManager.Instance.OnStageFail.RemoveListener(HidePanel);
        Spawner.OnBossSpawned.RemoveListener(SetBar);
    }

    private void SetBar(GameObject bossObject)
    {
        _cachedHealthController = bossObject.GetComponent<EnemyHealthController>();
        HealthBar.maxValue = bossObject.GetComponent<EnemyStateController>().characterData.Health;
        HealthBar.value = HealthBar.maxValue;
        bossName.text = bossObject.GetComponentInChildren<SkinnedMeshRenderer>().gameObject.name.ToUpper();
        ShowPanel();
    }
    private void Update()
    {
        if(_cachedHealthController!=null)
            ChangeHealth();
    }
    private void ChangeHealth()
    {
        if (_cachedHealthController.GetCurrentHealth() == 0)
            HidePanel();
        HealthBar.value = _cachedHealthController.GetCurrentHealth();
    }
}
