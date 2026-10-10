using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class bala : MonoBehaviour
{
    [Header("Personaje")]
    public main_Mechanics personajeScript;

    [Header("Configuración")]
    public float velocidad = 20f;
    public int puntaje;

    void FixedUpdate()
    {
        transform.position += transform.forward * velocidad * Time.fixedDeltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        // Si toca un zombie, lo mata
        if (other.CompareTag("Zombie"))
        {
            Destroy(other.gameObject);
            personajeScript.puntaje += puntaje;
            Destroy(gameObject);
        }
        // La bala desaparece al tocar cuaqlquier objeto q no sea traspasable, el jugador o un trigger random
        else if (!other.CompareTag("Traspasable") && !other.CompareTag("Player") && !other.CompareTag("Trigger"))
        {
            Destroy(gameObject);
        }
    }
}
