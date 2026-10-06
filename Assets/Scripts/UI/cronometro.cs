using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class cronometro : MonoBehaviour
{
    public TextMeshProUGUI textoCronometro;

    private float tiempo = 0f;

    void Update()
    {
        tiempo += Time.deltaTime;

        int horas = Mathf.FloorToInt(tiempo / 3600f);
        int minutos = Mathf.FloorToInt((tiempo % 3600f) / 60f);
        int segundos = Mathf.FloorToInt(tiempo % 60f);
        int centesimas = Mathf.FloorToInt((tiempo * 100f) % 100f);

        if (horas > 0)
        {
            // HH:MM:SS.CC
            textoCronometro.text = string.Format(
                "{0:00}:{1:00}:{2:00}.{3:00}",
                horas,
                minutos,
                segundos,
                centesimas
            );
        }
        else
        {
            // MM:SS.CC
            textoCronometro.text = string.Format(
                "{0:00}:{1:00}.{2:00}",
                minutos,
                segundos,
                centesimas
            );
        }
    }
}
