using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class bala : MonoBehaviour
{
    [Header("Configuración")]
    public TextMeshProUGUI puntaje;

    [Header("Configuración")]
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
            int puntajeInt;
            int.TryParse(puntaje.text, out puntajeInt);
            puntajeInt++;
            puntaje.text = puntajeInt.ToString("D5");
            Destroy(gameObject);
        }
        // La bala desaparece al tocar cuaqlquier objeto q no sea traspasable
        else if (!other.CompareTag("Traspasable"))
        {
            Destroy(gameObject);
        }
    }
}
