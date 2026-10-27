using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class torre : MonoBehaviour
{
    [Header("Personaje")]
    public main_Mechanics personajeScript;

    [Header("Configuración")]
    public string nombre;
    public cronometro Cronometro;
    public string texto;
    public int puntaje;

    [Header("Internas")]
    private bool enZona = false;
    public string[] nombres = new string[3];

    public void Start()
    {
        enZona = false;
    }

    public void Update()
    {
        if(enZona && Input.GetKeyDown(KeyCode.E))
        {
            personajeScript.texto.text = "";
            personajeScript.siguienteObjetivo = SorteoTorre();
            personajeScript.puntaje += puntaje;
            Cronometro.tiempo += 120;
            enZona = false;
        }
    }

    public string SorteoTorre()
    {
        string aux = nombres[Random.Range(0, nombres.Length)];
        return aux;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && nombre == personajeScript.siguienteObjetivo)
        {
            personajeScript.texto.text = texto;
            enZona = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enZona = false;
            personajeScript.texto.text = "";
        }
    }
}
