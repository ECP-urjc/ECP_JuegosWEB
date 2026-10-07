using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Flotar : MonoBehaviour
{
    public string type;
    public TextMeshProUGUI contador;
    public Slider barraVida;
    public float altura = 0.3f;
    public float velocidad = 10f;
    public int cantidad = 10;

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
    }

    public void SumarVida()
    {
        barraVida.value += cantidad;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(type == "bala"){SumarBalas();}
            else if (type == "vida"){SumarVida();}
            gameObject.SetActive(false);
            Invoke("Reaparecer", 10f);
        }
    }

    private void Reaparecer()
    {
        gameObject.SetActive(true);
    }
}
