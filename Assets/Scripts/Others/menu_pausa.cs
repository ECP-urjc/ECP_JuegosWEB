using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_pausa : MonoBehaviour
{
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
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void ReiniciarEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SalirJuego()
    {
        Application.Quit();
    }
}
