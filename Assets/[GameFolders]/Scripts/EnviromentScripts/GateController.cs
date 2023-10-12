using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GateController : MonoBehaviour
{
    #region Params
    public GameObject leftDoor;
    public GameObject rightDoor;

    const float GATE_OPEN_TIME = 0.5f;
    #endregion
    #region Methods
    private void OnEnable()
    {
        Spawner.OnAllEnemiesEnd.AddListener(GateOpen);
    }
    private void OnDisable()
    {
        Spawner.OnAllEnemiesEnd.RemoveListener(GateOpen);

    }
    private void GateOpen()
    {
        leftDoor.transform.DORotate(new Vector3(0f, 90f, 0f), GATE_OPEN_TIME);
        rightDoor.transform.DORotate(new Vector3(0f, -90f, 0f), GATE_OPEN_TIME);
    }
    #endregion
}
