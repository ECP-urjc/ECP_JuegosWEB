using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vida : MonoBehaviour
{
    [Header("Personaje")]
    public main_Mechanics personajeScript;

    [Header("Ajustes Vida")]
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
        if(personajeScript.enPausa)
        {
            return;
        }
        vidaActual = personajeScript.vida;
        barraVida.value = Mathf.MoveTowards(
            barraVida.value,
            vidaActual,
            velocidadCuracion * Time.unscaledDeltaTime
        );

        if(personajeScript.vida <= 0)
        {
            personajeScript.vida = 0;
        }
        else if(personajeScript.vida >= vidaMaxima)
        {
            personajeScript.vida = vidaMaxima;
        }
    }

    public void RecibirDaño(int daño)
    {
        if(personajeScript.enPausa)
        {
            return;
        }
        vidaActual -= daño;

        if (vidaActual < 0)
            vidaActual = 0;

        barraVida.value = vidaActual;
        Morir();
    }

    void Morir()
    {
        Debug.Log("El jugador ha muerto");
    }
}
