using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NuevoObjetivoTexto : MonoBehaviour
{
    [Header("Personaje")]
    public main_Mechanics personajeScript;

    [Header("Otros")]
    public TextMeshProUGUI texto;

    [Header("Posiciones (anchoredPosition Y)")]
    public float yOculto = 150f;    // texto fuera de pantalla
    public float yVisible = -100f;  // texto visible

    private RectTransform rect;
    private float timer;
    private bool startTimer;
    private string actualObjetivo;

    void Start()
    {
        rect = texto.rectTransform;
        startTimer = false;
        timer = 0;
        actualObjetivo = personajeScript.siguienteObjetivo;
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, yOculto);
    }

    void Update()
    {
        if (personajeScript.siguienteObjetivo != actualObjetivo)
        {
            actualObjetivo = personajeScript.siguienteObjetivo;
            startTimer = true;
            timer = 0;
        }

        texto.text = actualObjetivo;

        Timer();
    }

    public void Timer()
    {
        float destinoY = yOculto;

        if (startTimer)
        {
            timer += Time.unscaledDeltaTime;

            if (timer < 5f)
            {
                destinoY = yVisible;
            }
            else
            {
                startTimer = false;
            }
        }

        Vector2 posicionDestino = new Vector2(
            rect.anchoredPosition.x,
            destinoY
        );

        rect.anchoredPosition = Vector2.Lerp(
            rect.anchoredPosition,
            posicionDestino,
            1f - Mathf.Exp(-10f * Time.unscaledDeltaTime)
        );
    }
}