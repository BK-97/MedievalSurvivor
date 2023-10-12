using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Portal : MonoBehaviour,IInteractable
{
    #region Events
    public static UnityEvent OnUsePortal = new UnityEvent();
    #endregion
    #region Params
    public string portalNextLevelName;
    public bool canBeInteract { get; private set; }
    #endregion
    #region Methods
    private void OnEnable()
    {
        Spawner.OnAllEnemiesEnd.AddListener(PortalOpen);
    }
    private void OnDisable()
    {
        Spawner.OnAllEnemiesEnd.RemoveListener(PortalOpen);
    }
    private void PortalOpen()
    {
        canBeInteract = true;
    }
    public void Interact()
    {
        if (!canBeInteract)
            return;
        OnUsePortal.Invoke();
    }
    #endregion
}
