using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pproximaTorreText : MonoBehaviour
{
    [Header("Personaje")]
    public main_Mechanics personajeScript;

    [Header("Texto")]
    public TextMeshProUGUI texto;

    // Update is called once per frame
    void Update()
    {
        texto.text = "Próxima torre: " + personajeScript.siguienteObjetivo;
    }
}
