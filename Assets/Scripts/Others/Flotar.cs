using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Flotar : MonoBehaviour
{
    public TextMeshProUGUI contador;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int numero = int.Parse(contador.text);
            numero += cantidad;
            contador.text = numero.ToString();
            gameObject.SetActive(false);
            Invoke("Reaparecer", 10f);
        }
    }

    private void Reaparecer()
    {
        gameObject.SetActive(true);
    }
}
