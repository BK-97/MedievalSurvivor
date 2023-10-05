using UnityEngine;
public class FXBackToPool : MonoBehaviour
{
    private void OnDisable()
    {
        //MultiGameObjectPool.Instance.ReturnObject(gameObject);
    }
}