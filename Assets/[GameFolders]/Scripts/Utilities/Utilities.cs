using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    public static bool IsNullOrDestroyed(object obj, out System.Type type)
    {
        if (obj == null)
        {
            type = null;
            return true;
        }

        System.Type objType = obj.GetType();

        type = objType;

        return ReferenceEquals(objType, type);
    }
    public static Vector3 GetMouseWorldPosition(LayerMask hitLayer)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, hitLayer))
        {
            return hit.point;
        }

        return Vector3.zero;
    }
}
