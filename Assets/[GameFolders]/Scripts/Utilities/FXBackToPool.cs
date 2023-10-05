using UnityEngine;
public class FXBackToPool : MonoBehaviour
{
    private void OnDisable()
    {
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}