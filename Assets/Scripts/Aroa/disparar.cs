using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class disparar : MonoBehaviour
{
    [Header("Configuración Disparo")]
    public TextMeshProUGUI numBalas;
    public GameObject prefabBala;
    public Transform puntoDisparo;
    public float velocidadBala = 20f;

    void Update()
    {
        int numBalasInt;
        int.TryParse(numBalas.text, out numBalasInt);

        if ((Input.GetKeyDown(KeyCode.I) || Input.GetMouseButtonDown(0)) && numBalasInt > 0)
        {
            DispararBala();

            numBalasInt--;
            numBalas.text = numBalasInt.ToString();
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
