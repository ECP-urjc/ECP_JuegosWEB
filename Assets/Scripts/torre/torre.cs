using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torre : MonoBehaviour
{
    [Header("Personaje")]
    public main_Mechanics personajeScript;
    [Header("Configuración")]
    public string texto;
    public int puntaje;
    [Header("Internas")]
    private bool enZona = false;

    public void Start()
    {
        enZona = false;
    }

    public void Update()
    {
        if(enZona && Input.GetKeyDown(KeyCode.E))
        {
            personajeScript.texto.text = "Cura esparcida";
            personajeScript.puntaje += puntaje;
            enZona = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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
