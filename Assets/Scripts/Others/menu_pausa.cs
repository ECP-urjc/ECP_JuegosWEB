using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_pausa : MonoBehaviour
{
    [Header("Personaje")]
    public main_Mechanics PersonajeScript;

    [Header("otros")]
    public GameObject menuPausa;
    private bool pausado = false;
    void Start()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CambiarPausa();
        }
    }

    public void CambiarPausa()
    {
        pausado = !pausado;

        menuPausa.SetActive(pausado);

        if (pausado)
        {
            PersonajeScript.enPausa = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            PersonajeScript.enPausa = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void ReiniciarEscena()
    {
        SceneManager.LoadScene("SandBox");
    }

    public void SalirJuego()
    {
        SceneManager.LoadScene("menu(s)");
    }
}
