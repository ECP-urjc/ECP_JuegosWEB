using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Flotar : MonoBehaviour
{
    [Header("Personaje")]
    public main_Mechanics personajeScript;

    [Header("Elegir 'bala', 'vida', 'arma',...")]
    public string type;
    public int cantidad = 10;
    public TextMeshProUGUI contador;
    public int puntajePorAtaqueCerca;

    [Header("Flotar Movimiento")]
    public float altura = 0.3f;
    public float velocidad = 10f;
    private float posicionInicialY;

    void Start()
    {
        posicionInicialY = transform.position.y;
    }

    void Update()
    {
        float nuevaY = posicionInicialY + Mathf.Sin(Time.time * velocidad) * altura;

        transform.position = new Vector3(
            transform.position.x,
            nuevaY,
            transform.position.z
        );
    }

    public void SumarBalas()
    {
        int numero = int.Parse(contador.text.Replace("/ ", ""));
        numero += cantidad;
        contador.text = "/ " + numero.ToString();
        gameObject.SetActive(false);
    }

    public void SumarVida()
    {
        personajeScript.vida += cantidad;
    }

    public void DarArma()
    {
        personajeScript.usos = cantidad;
        personajeScript.tieneArma = true;
        personajeScript.puntajePorAtaqueCerca = puntajePorAtaqueCerca;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (type == "bala")
            {
                SumarBalas();
            }
            else if (type == "vida")
            {
                SumarVida();
            }
            else if (type == "arma")
            {
                DarArma();
            }

            gameObject.SetActive(false);
            Invoke("Reaparecer", 10f);
        }
    }

    private void Reaparecer()
    {
        gameObject.SetActive(true);
    }
}
