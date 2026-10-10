using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class puntos : MonoBehaviour
{
    [Header("Personaje")]
    public main_Mechanics personajeScript;

    [Header("Configuracion")]
    public TextMeshProUGUI puntaje;
    public int cifras;
    public float tiempoEspera;

    private int puntajeActual = 0;
    private Coroutine corrutinaPuntaje;

    void Update()
    {
        if (puntajeActual == personajeScript.puntaje)
        {
            if (corrutinaPuntaje != null)
            {
                StopCoroutine(corrutinaPuntaje);
            }
        }else
        {
            corrutinaPuntaje = StartCoroutine(AnimarPuntaje(personajeScript.puntaje));
        }
    }

    IEnumerator AnimarPuntaje(int objetivo)
    {
        while (puntajeActual < personajeScript.puntaje)
        {
            puntajeActual++;

            puntaje.text = puntajeActual.ToString("D5");

            yield return new WaitForSeconds(tiempoEspera);
        }
    }
}
