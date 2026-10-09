using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class respawn_sandbox : MonoBehaviour
{
    [Header("Personaje")]
    public main_Mechanics personajeScript;
    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -10.0f || personajeScript.vida <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
