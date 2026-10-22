using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class logica_combos : MonoBehaviour
{
    [Header("Personaje")]
    public main_Mechanics personajeScript;

    [Header("Texto")]
    public TextMeshProUGUI comboText;
    private RectTransform comboTextRect;

    [Header("Configuración")]
    public float tiempo = 3f;
    public float velocidad = 10f;
    public float velocidadDesvanecido = 10f;
    
    public Vector2[] pos; 
    public Vector3[] rot;
    public Vector3[] scale;
    
    private float time;
    private int actualCombo;

    void Start()
    {
        comboTextRect = comboText.GetComponent<RectTransform>();

        pos[1] = comboTextRect.anchoredPosition;
        rot[1] = comboText.transform.localEulerAngles;
        scale[1] = comboText.transform.localScale;

        comboText.text = "";
        actualCombo = 0;
    }

    void Update()
    {
        Timer();
        UpdateCombo();
        RestartCombo();
    }

    public void Timer()
    {
        if (personajeScript.numCombo > 0)
        {
            time -= Time.unscaledDeltaTime;
        }
    }

    public void UpdateCombo()
    {
        if (personajeScript.numCombo == 0) return;

        if (actualCombo != personajeScript.numCombo)
        {
            comboTextRect.anchoredPosition = pos[0];
            comboText.transform.localRotation = Quaternion.Euler(rot[0]);
            comboText.transform.localScale = scale[0];

            comboText.color = new Color(comboText.color.r, comboText.color.g, comboText.color.b, 1f);

            time = tiempo;
            actualCombo = personajeScript.numCombo;

            comboText.text = "x" + personajeScript.numCombo + ": " + personajeScript.razonCombo;
        }

        comboTextRect.anchoredPosition = Vector2.Lerp(comboTextRect.anchoredPosition, pos[1], velocidad * Time.unscaledDeltaTime);
        comboText.transform.localRotation = Quaternion.Lerp(comboText.transform.localRotation, Quaternion.Euler(rot[1]), velocidad * Time.unscaledDeltaTime);
        comboText.transform.localScale = Vector3.Lerp(comboText.transform.localScale, scale[1], velocidad * Time.unscaledDeltaTime);
    }

    public void RestartCombo()
    {

        if (time <= 0.5)
        {
            // Reducimos el canal Alfa (opacidad) hacia 0 suavemente
            float nuevoAlfa = Mathf.MoveTowards(comboText.color.a, 0f, velocidadDesvanecido * Time.unscaledDeltaTime);
            comboText.color = new Color(comboText.color.r, comboText.color.g, comboText.color.b, nuevoAlfa);
        }

        if (time <= 0 && personajeScript.numCombo > 0)
        {
            personajeScript.numCombo = 0;
            actualCombo = 0;
        }
    }
}
