using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class Menu_logic : MonoBehaviour
{
    [Header("Principales")]
    public GameObject camara;
    public GameObject[] positions;
    public float velocidad = 5f;
    public int menu_actual;
    public int sub_menu;
    public GameObject Canvas;

    [Header("Menú Inicial")]
    public TextMeshProUGUI textoPulsa;
    public GameObject logoJuego;

    [Header("Menú Pricipal")]
    public GameObject menuInicial;
    public GameObject botones;

    [Header("Menú Opciones")]
    public GameObject opciones;

    [Header("Menú Versión Completa")]
    public GameObject menuVS;

    [Header("Confirmar salida")]
    public GameObject seguroPanel;


    [Header("Otros")]
    public CanvasGroup panelNegro;
    private float timer;

    private Vector3 posActual;
    private Quaternion rotActual;

    void Start()
    {
        menu_actual = 0;
        sub_menu = 0;
        timer = 0;

        panelNegro.alpha = 1f;
        panelNegro.blocksRaycasts = true;
        panelNegro.interactable = true;

        Camera CAMERA = camara.GetComponent<Camera>();
        CAMERA.enabled = true;

        Canvas.SetActive(true);

        camara.transform.position = positions[0].transform.position;
        camara.transform.rotation = positions[0].transform.rotation;
    }

    void Update()
    {
        CambiarPosicion(menu_actual);
        switch (menu_actual) 
        {
            case 0:
            Menu0();
            break;
            case 1:
            Menu1();
            break;
            case 2:
            Menu2();
            break;
            case 3:
            Menu3();
            break;
            case -1:
            Menu_1();
            break;
            default:
            break;
        }
        RestoMenus();
    }

    Vector3 P(float x, float y)
    {
        return new Vector3(x * Screen.width / 3840f, y * Screen.height / 2160f, 0);
    }

    public void CambioEscena(int index)
    {
        menu_actual = index;
    }

    public void Jugar()
    {
        SceneManager.LoadScene("Nivel");
    }

    public void AbrirPagina(string web)
    {
        Application.OpenURL(web);
    }

    // Llámalo desde los botones del menú
    public void CambiarPosicion(int index)
    {
        if(index >= positions.Length || index < 0)
        {
            posActual = positions[0].transform.position;
            rotActual = positions[0].transform.rotation;
        }
        else
        {
            posActual = positions[index].transform.position;
            rotActual = positions[index].transform.rotation;
        }

        // Posición
        camara.transform.position = Vector3.Lerp(
            camara.transform.position,
            posActual,
            velocidad * Time.deltaTime);

        // Rotación
        camara.transform.rotation = Quaternion.Slerp(
            camara.transform.rotation,
            rotActual,
            velocidad * Time.deltaTime);
    }

    public void Menu0()
    {
        textoPulsa.transform.position = Vector3.Lerp(textoPulsa.transform.position, P(1920, 380), 10 * Time.deltaTime);
        logoJuego.transform.position = Vector3.Lerp(logoJuego.transform.position, P(1920, 1330), 20 * Time.deltaTime);

        if(Input.anyKeyDown)
        {
            menu_actual = 1;
        }

    }

    public void Menu_1()
    {
       seguroPanel.transform.position = Vector3.Lerp(seguroPanel.transform.position, P(1920, 1080), 10 * Time.deltaTime);
    }

    public void Menu1()
    {
        menuInicial.transform.position = Vector3.Lerp(menuInicial.transform.position, P(3117, 1080), 10 * Time.deltaTime);
        botones.transform.position = Vector3.Lerp(botones.transform.position, P(1920, 1080), 10 * Time.deltaTime);
    }

    public void Menu2()
    {
        opciones.transform.position = Vector3.Lerp(opciones.transform.position, P(1920, 250), 10 * Time.deltaTime);
    }

    public void Menu3()
    {
        menuVS.transform.position = Vector3.Lerp(menuVS.transform.position, P(1920, 250), 10 * Time.deltaTime);
    }

    public void RestoMenus()
    {
        if(menu_actual != 0)
        {
            textoPulsa.transform.position = Vector3.Lerp(textoPulsa.transform.position, P(1920, -300), 10 * Time.deltaTime);
            logoJuego.transform.position = Vector3.Lerp(logoJuego.transform.position, P(1920, 3330), 20 * Time.deltaTime);
        }

        if(menu_actual != 1)
        {
            menuInicial.transform.position = Vector3.Lerp(menuInicial.transform.position, P(5117, 1080), 10 * Time.deltaTime);
            botones.transform.position = Vector3.Lerp(botones.transform.position, P(3920, 1080), 10 * Time.deltaTime);
        }

        if(menu_actual != 2)
        {
            opciones.transform.position = Vector3.Lerp(opciones.transform.position, P(1920, -3250), 10 * Time.deltaTime);
        }

        if(menu_actual != 3)
        {
            menuVS.transform.position = Vector3.Lerp(menuVS.transform.position, P(1920, -3250), 10 * Time.deltaTime);
        }
        if(menu_actual != -1)
        {
       seguroPanel.transform.position = Vector3.Lerp(seguroPanel.transform.position, P(1920, -1080), 10 * Time.deltaTime);

        }
        if (menu_actual != -2)
        {
            panelNegro.alpha = Mathf.MoveTowards(
                panelNegro.alpha,
                0f,
                2f * Time.deltaTime
            );

            if (panelNegro.alpha == 0f)
            {
                panelNegro.blocksRaycasts = false;
                panelNegro.interactable = false;
            }
        }
        else
        {
            panelNegro.blocksRaycasts = true;
            panelNegro.interactable = true;

            panelNegro.alpha = Mathf.MoveTowards(
                panelNegro.alpha,
                1f,
                2f * Time.deltaTime
            );

            timer += Time.deltaTime;

            if (timer >= 2f)
            {
                Application.Quit();

                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #endif
            }
        }
    }
}
