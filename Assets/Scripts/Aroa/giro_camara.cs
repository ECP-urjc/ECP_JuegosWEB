using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giro_camara : MonoBehaviour
{
    [Header("Referencias")]
    public Transform personaje;

    [Header("Sensibilidad")]
    public float sensibilidad = 3f;

    [Header("Limites horizontales")]
    public float limiteIzquierda = -90f;
    public float limiteDerecha = 90f;

    [Header("Limites verticales")]
    public float limiteAbajo = -45f;
    public float limiteArriba = 45f;

    private float rotacionHorizontal;
    private float rotacionVertical;

    private void Start()
    {
        if (personaje == null)
            return;

        rotacionHorizontal = 0f;
        rotacionVertical = 0f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (personaje == null)
            return;

        float movimientoX = Input.GetAxis("Mouse X") * sensibilidad;
        float movimientoY = Input.GetAxis("Mouse Y") * sensibilidad;

        rotacionHorizontal += movimientoX;
        rotacionVertical -= movimientoY;

        rotacionHorizontal = Mathf.Clamp(
            rotacionHorizontal,
            limiteIzquierda,
            limiteDerecha
        );

        rotacionVertical = Mathf.Clamp(
            rotacionVertical,
            limiteAbajo,
            limiteArriba
        );

        // El giro horizontal parte de la rotacion actual del personaje.
        Quaternion rotacionBase = personaje.rotation;

        Quaternion rotacionHorizontalFinal =
            rotacionBase * Quaternion.Euler(0f, rotacionHorizontal, 0f);

        // El giro vertical se aplica sobre el eje local del objeto.
        transform.rotation =
            rotacionHorizontalFinal * Quaternion.Euler(rotacionVertical, 0f, 0f);
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
