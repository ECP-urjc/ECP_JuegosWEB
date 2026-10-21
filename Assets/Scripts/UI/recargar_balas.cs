using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class recargar_balas : MonoBehaviour
{
    [Header("Personaje")]
    public main_Mechanics personajeScript;

    [Header("UI texts")]
    public TextMeshProUGUI numBalas;
    public TextMeshProUGUI totalBalas;

    [Header("Configuración")]
    public float tiempoEntreBalas = 0.1f;

    private bool recargando = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !recargando)
        {
            StartCoroutine(RecargarBalas());
        }

        totalBalas.text = "/ " + personajeScript.balasTotales;
        numBalas.text = "" + personajeScript.balasCargadas;
    }

    IEnumerator RecargarBalas()
    {
        recargando = true;

        // Pasar balas una a una
        while (personajeScript.balasCargadas < personajeScript.maxBalas && personajeScript.balasTotales > 0)
        {
            personajeScript.balasTotales--;
            personajeScript.balasCargadas++;

            // Esperar segundos antes de pasar la siguiente
            yield return new WaitForSecondsRealtime(tiempoEntreBalas);
        }

        recargando = false;
    }
}
