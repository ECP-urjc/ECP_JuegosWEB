using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class cronometro : MonoBehaviour
{
    [Header("Personaje")]
    public main_Mechanics PersonajeScript;

    [Header("Cronometros")]
    public TextMeshProUGUI Cronometro;
    public bool invertir;
    public int tiempoInicial;

    public float tiempo = 0f;

    void Start()
    {
        if (invertir)
        {
            tiempo = tiempoInicial;
        }
    }

    void Update()
    {
        if(PersonajeScript.enPausa)
        {
            return;
        }

        if(invertir)
        {
            tiempo -= Time.unscaledDeltaTime;
        }
        else
        {
            tiempo += Time.unscaledDeltaTime;
        }
        

        int horas = Mathf.FloorToInt(tiempo / 3600f);
        int minutos = Mathf.FloorToInt((tiempo % 3600f) / 60f);
        int segundos = Mathf.FloorToInt(tiempo % 60f);
        int centesimas = Mathf.FloorToInt((tiempo * 100f) % 100f);

        if (horas > 0)
        {
            // HH:MM:SS.CC
            Cronometro.text = string.Format(
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
            Cronometro.text = string.Format(
                "{0:00}:{1:00}.{2:00}",
                minutos,
                segundos,
                centesimas
            );
        }

        if(invertir == true && tiempo <= 0)
        {
            tiempo = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
