using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GateController : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;
    const float GATE_OPEN_TIME=0.5f;
    private void OnEnable()
    {
        Spawner.OnWaveEnd.AddListener(GateOpen);
    }
    private void OnDisable()
    {
        Spawner.OnWaveEnd.RemoveListener(GateOpen);

    }
    private void GateOpen()
    {
        leftDoor.transform.DORotate(new Vector3(0f, 90f, 0f), GATE_OPEN_TIME);
        rightDoor.transform.DORotate(new Vector3(0f, -90f, 0f), GATE_OPEN_TIME);
    }
}
