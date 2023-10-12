using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private IInteractable nearInteractable;
    bool canInteract = true;

    private void OnEnable()
    {
        InputManager.OnInteractInput.AddListener(InteractWithInteractable);
    }
    private void OnDisable()
    {
        InputManager.OnInteractInput.RemoveListener(InteractWithInteractable);
    }
    private void InteractWithInteractable()
    {
        if (!canInteract)
            return;
        if (nearInteractable == null)
            return;
        nearInteractable.Interact();
    }
    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null && interactable.canBeInteract)
        {
            nearInteractable = interactable;
            string feedbackString = "PRESS E FOR INTERACT";
            FeedbackPanel.OnFeedbackOpen.Invoke(feedbackString);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            nearInteractable = null;
            FeedbackPanel.OnFeedbackClose.Invoke();
        }
    }
}
