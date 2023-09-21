using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotateController : MonoBehaviour
{
    public float rotationSpeed = 3;

    private void Update()
    {
        if (!LevelManager.Instance.IsLevelStarted)
            return;
        Vector3 direction = InputManager.Instance.GetMouseWorldPos() - transform.position;
        direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
