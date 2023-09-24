using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookToCamera : MonoBehaviour
{
    #region Parameters
    private Camera mainCamera;
    bool look = false;
    #endregion
    #region Properties

    #endregion
    #region MonoBehaviour Methods
    private void OnEnable()
    {
        look = true;
    }
    private void OnDisable()
    {
        look = false;
    }
    private void Start()
    {
        mainCamera = Camera.main;
    }
    #endregion
    #region My Methods
    private void Update()
    {
        if (mainCamera == null)
            return;
        if (look)
        {
            transform.LookAt(mainCamera.transform);
        }
    }
    #endregion
}
