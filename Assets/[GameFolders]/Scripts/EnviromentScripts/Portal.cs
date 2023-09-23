using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Portal : MonoBehaviour,IInteractable
{
    public static StringEvent OnUsePortal = new StringEvent();
    public string portalNextLevelName;
    public bool canBeInteract { get; private set; }
    private void OnEnable()
    {
        Spawner.OnWaveEnd.AddListener(PortalOpen);
    }
    private void OnDisable()
    {
        Spawner.OnWaveEnd.RemoveListener(PortalOpen);
    }
    private void PortalOpen()
    {
        canBeInteract = true;
    }
    public void Interact()
    {
        if (!canBeInteract)
            return;
        OnUsePortal.Invoke(portalNextLevelName);
    }
}
