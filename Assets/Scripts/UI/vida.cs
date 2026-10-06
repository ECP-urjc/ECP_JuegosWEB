using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class vida : MonoBehaviour
{
    public int vidaMaxima = 100;
    public int vidaActual;

    public Slider barraVida;

    void Start()
    {
        vidaActual = vidaMaxima;

        barraVida.maxValue = vidaMaxima;
        barraVida.value = vidaActual;
    }

    public void RecibirDaño(int daño)
    {
        vidaActual -= daño;

        if (vidaActual < 0)
            vidaActual = 0;

        barraVida.value = vidaActual;

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        Debug.Log("El jugador ha muerto");
    }
}
