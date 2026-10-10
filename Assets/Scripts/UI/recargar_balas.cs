using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class recargar_balas : MonoBehaviour
{
    [Header("UI texts")]
    public TextMeshProUGUI numBalas;
    public TextMeshProUGUI totalBalas;

    [Header("Configuración")]
    public float tiempoEntreBalas = 0.1f;
    public int maxBalas = 10;

    private bool recargando = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !recargando)
        {
            StartCoroutine(RecargarBalas());
        }
    }

    IEnumerator RecargarBalas()
    {
        recargando = true;

        int total = int.Parse(totalBalas.text.Replace("/ ", ""));
        int balas = int.Parse(numBalas.text.Replace("/ ", ""));

        // Pasar balas una a una
        while (balas < maxBalas && total > 0)
        {
            total--;
            balas++;

            // Actualizar los textos
            totalBalas.text = "/ " + total;
            numBalas.text = ""+balas;

            // Esperar 0.1 segundos antes de pasar la siguiente
            yield return new WaitForSeconds(tiempoEntreBalas);
        }

        recargando = false;
    }
}
