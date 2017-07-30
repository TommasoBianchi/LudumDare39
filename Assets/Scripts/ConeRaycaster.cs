using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeRaycaster : MonoBehaviour
{

    public LayerMask castLayers;
    [Range(0.1f, 5)]
    public float range;
    [Range(0, 180)]
    public float angle;
    [Range(3, 8)]
    public int numberOfLines;

    void Update()
    {
        Raycast(Vector2.right);
    }

    public GameObject Raycast(Vector2 dir)
    {
        dir = Quaternion.Euler(0, 0, -angle / 2f) * dir;

        for (int i = 0; i < numberOfLines; i++)
        {
            RaycastHit2D result = Physics2D.Raycast(transform.position, dir, range, castLayers);
            Debug.DrawLine(transform.position, transform.position + new Vector3(dir.x, dir.y, 0) * range, Color.red);
            if (result != null && result.transform != null)
            {
                return result.transform.gameObject;
            }
            dir = Quaternion.Euler(0, 0, angle / (numberOfLines - 1)) * dir;
        }

        return null;
    }
}
