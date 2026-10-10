using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bate_aparicion : MonoBehaviour
{
    [Header("Personaje")]
    public main_Mechanics personajeScript;

    [Header("Bate")]
    public GameObject bate;

    // Start is called before the first frame update
    void Update()
    {
        if (personajeScript.tieneArma == true)
        {
            bate.SetActive(true);
        }
        else
        {
            bate.SetActive(false);
        }
    }
}
