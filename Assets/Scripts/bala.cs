using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bala : MonoBehaviour
{
    public float velocidad = 20f;

    void FixedUpdate()
    {
        transform.position += transform.forward * velocidad * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        // Si toca un zombie, lo mata
        if (other.CompareTag("Zombie"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        // La bala desaparece al tocar cuaqlquier objeto q no sea traspasable
        else if (!other.CompareTag("Traspasable"))
        {
            Destroy(gameObject);
        }
    }
}
