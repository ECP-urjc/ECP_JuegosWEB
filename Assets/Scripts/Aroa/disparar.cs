using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class disparar : MonoBehaviour
{
    [Header("Personaje")]
    public main_Mechanics personajeScript;

    [Header("Configuración Disparo")]
    public GameObject prefabBala;
    public Transform puntoDisparo;
    public float velocidadBala = 20f;

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.I) || Input.GetMouseButtonDown(0)) && personajeScript.balasCargadas > 0)
        {
            personajeScript.balasCargadas--;
            DispararBala();
        }
    }

    void DispararBala()
    {
        GameObject Bala = Instantiate(
            prefabBala,
            puntoDisparo.position,
            puntoDisparo.rotation
        );

        bala scriptBala = Bala.GetComponent<bala>();

        if (scriptBala != null)
        {
            scriptBala.velocidad = velocidadBala;
        }
    }
}
