using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_pausa : MonoBehaviour
{
    [Header("Personaje")]
    public main_Mechanics PersonajeScript;

    [Header("otros")]
    public AudioSource audioSource;
    public GameObject menuPausa;
    private bool pausado = false;
    void Start()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PersonajeScript.vivo)
        {
            CambiarPausa();
        }

        if(PersonajeScript.vivo == false)
        {
            menuPausa.SetActive(false);
            Time.timeScale = 1f;
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
            audioSource.Pause();
        }
        else
        {
            PersonajeScript.enPausa = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            audioSource.UnPause();
        }
    }

    public void ReiniciarEscena()
    {
        PersonajeScript.enPausa = false;
        SceneManager.LoadScene("Nivel");
    }

    public void SalirJuego()
    {
        PersonajeScript.enPausa = false;
        SceneManager.LoadScene("menu(s)");
    }
}
