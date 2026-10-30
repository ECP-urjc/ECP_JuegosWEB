using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class aparcicion_y_desaparicion : MonoBehaviour
{
    public TextMeshProUGUI texto;
    public float velocidad = 2f;

    // Segundos de espera antes de que empiece la animación
    public float retraso = 3f;

    private float alfaObjetivo = 1f;
    private float temporizador = 0f;

    void Start()
    {
        if (texto == null) texto = GetComponent<TextMeshProUGUI>();

        // Empieza invisible
        Color c = texto.color;
        c.a = 0f;
        texto.color = c;
    }

    void Update()
    {
        // Espera antes de empezar
        if (temporizador < retraso)
        {
            temporizador += Time.deltaTime;
            return;
        }

        Color col = texto.color;
        col.a = Mathf.Lerp(col.a, alfaObjetivo, velocidad * Time.deltaTime);
        texto.color = col;

        // Cuando se acerca al objetivo, cambia al contrario
        if (Mathf.Abs(col.a - alfaObjetivo) < 0.05f)
        {
            alfaObjetivo = (alfaObjetivo == 1f) ? 0f : 1f;
        }
    }
}
