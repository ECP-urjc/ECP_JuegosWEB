using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class clonador_texto : MonoBehaviour
{
    public TextMeshProUGUI textoOriginal;
    public TextMeshProUGUI textoCopia;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textoCopia.text = textoOriginal.text;
    }
}
