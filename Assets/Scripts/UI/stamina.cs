using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stamina : MonoBehaviour
{
    [Header("Personaje")]
    public main_Mechanics personajeScript;

    [Header("Ajustes Stamina")]
    public int staMaxima = 100;
    public int staActual;
    public float velocidadRecarga = 5f;
    public float velocidadUso = 5f;
    public Slider barraStamina;

    void Start()
    {
        barraStamina.maxValue = staMaxima;
        barraStamina.value = staActual;
    }

    void Update()
    {
        if(personajeScript.enPausa)
        {
            return;
        }
        staActual = personajeScript.stamina;
        barraStamina.value = Mathf.MoveTowards(
            barraStamina.value,
            staActual,
            velocidadRecarga * Time.unscaledDeltaTime
        );
    } 
}
