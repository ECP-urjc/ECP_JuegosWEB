using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vida : MonoBehaviour
{
    public int vidaMaxima = 100;
    public int vidaActual;
    public float velocidadCuracion = 5f;
    public Slider barraVida;

    void Start()
    {
        barraVida.maxValue = vidaMaxima;
        barraVida.value = vidaActual;
    }

    void Update()
    {
        barraVida.value = Mathf.MoveTowards(
            barraVida.value,
            vidaActual,
            velocidadCuracion * Time.deltaTime
        );
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
