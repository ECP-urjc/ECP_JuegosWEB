using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu_logic : MonoBehaviour
{
    [Header("Principales")]
    public GameObject camara;
    public GameObject[] positions;
    public float velocidad = 5f;
    public int menu_actual;
    public int sub_menu;

    [Header("Menú Inicial")]
    public TextMeshProUGUI textoPulsa;
    public GameObject logoJuego;

    [Header("Menú Pricipal")]
    public GameObject menuInicial;
    public GameObject botones;

    [Header("Menú Opciones")]


    [Header("Menú Versión Completa")]


    [Header("Confirmar salida")]
    public GameObject seguroPanel;


    [Header("Otros")]
    public CanvasGroup panelNegro;
    private float timer;

    private Vector3 posActual;
    private Quaternion rotActual;

    void Start()
    {
        timer = 0;
        panelNegro.alpha = 1f;
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
        textoPulsa.transform.position = Vector3.Lerp(textoPulsa.transform.position, new Vector3(1920, 380, 0), 10 * Time.deltaTime);
        logoJuego.transform.position = Vector3.Lerp(logoJuego.transform.position, new Vector3(1920, 1430, 0), 20 * Time.deltaTime);

        if(Input.anyKeyDown)
        {
            menu_actual = 1;
        }

    }

    public void Menu_1()
    {
       seguroPanel.transform.position = Vector3.Lerp(seguroPanel.transform.position, new Vector3(1920, 1080, 0), 10 * Time.deltaTime);

       if(Input.GetKeyDown(KeyCode.Escape))
        {
            menu_actual = -2;
        }
    }

    public void Menu1()
    {
        menuInicial.transform.position = Vector3.Lerp(menuInicial.transform.position, new Vector3(3117, 1080, 0), 10 * Time.deltaTime);
        botones.transform.position = Vector3.Lerp(botones.transform.position, new Vector3(1920, 1080, 0), 10 * Time.deltaTime);
    }

    public void Menu2()
    {

    }

    public void Menu3()
    {

    }

    public void RestoMenus()
    {
        if(menu_actual != 0)
        {
            textoPulsa.transform.position = Vector3.Lerp(textoPulsa.transform.position, new Vector3(1920, -300, 0), 10 * Time.deltaTime);
            logoJuego.transform.position = Vector3.Lerp(logoJuego.transform.position, new Vector3(1920, 3430, 0), 20 * Time.deltaTime);
        }

        if(menu_actual != 1)
        {
            menuInicial.transform.position = Vector3.Lerp(menuInicial.transform.position, new Vector3(5117, 1080, 0), 10 * Time.deltaTime);
            botones.transform.position = Vector3.Lerp(botones.transform.position, new Vector3(3920, 1080, 0), 10 * Time.deltaTime);
        }

        if(menu_actual != 2)
        {

        }

        if(menu_actual != 3)
        {
            
        }
        if(menu_actual != -1)
        {
       seguroPanel.transform.position = Vector3.Lerp(seguroPanel.transform.position, new Vector3(1920, -1080, 0), 10 * Time.deltaTime);

        }
        if (menu_actual != -2)
        {
            // Se desvanece
            panelNegro.alpha = Mathf.Lerp(panelNegro.alpha, 0f, 2 * Time.deltaTime);
            if (panelNegro.alpha < 0.01f)
            {
                panelNegro.alpha = 0f;
                panelNegro.blocksRaycasts = false;
            }
        }
        else
        {
            // Aparece
            panelNegro.blocksRaycasts = true;
            panelNegro.alpha = Mathf.Lerp(panelNegro.alpha, 1f, 2 * Time.deltaTime);
            if(timer >= 2)
            {
                Application.Quit();
            }
            timer += Time.deltaTime;
        }
    }
}
