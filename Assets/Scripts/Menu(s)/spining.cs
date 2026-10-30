using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spining : MonoBehaviour
{
    public float velocidad = 90f;

    public Vector3 eje = Vector3.up;

    void Update()
    {
        transform.Rotate(eje * velocidad * Time.deltaTime, Space.Self);
    }
}
