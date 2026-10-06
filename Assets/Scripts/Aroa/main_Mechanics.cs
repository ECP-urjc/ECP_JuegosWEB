using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main_Mechanics : MonoBehaviour
{
    public GameObject[] camaraPositions;
    public float maxSpeed = 70f;
    public float acceleration = 20f;
    public float deceleration = 50f;
    public float rotationSpeed = 90f;
    public float currentSpeed = 0f;
    public float alturaSalto = 2.5f;
    public float gravedadSalto = -25f;
    private bool estaEnSuelo;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotationX |
        RigidbodyConstraints.FreezeRotationZ;

        rb.useGravity = true;
    }

    void FixedUpdate()
    {
        Movement();
        Jump();
    }

    void Update()
    {
        ZoomCamera();
    }

    public void Movement()
    {
        rb.angularVelocity = Vector3.zero;
        
        // =========================
        // W / S
        // =========================

        float movimiento = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            movimiento = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movimiento = -1f;
        }

        // =========================
        // ACELERAR / FRENAR
        // =========================

        if (movimiento == -1)
        {
            // Frenar doble de rápido
            currentSpeed = Mathf.MoveTowards(
                currentSpeed,
                0f,
                deceleration * 1.8f * Time.fixedDeltaTime
            );
        }
        else if (movimiento == 1)
        {
            // Acelerar progresivamente
            currentSpeed += acceleration * movimiento * Time.fixedDeltaTime;

            // Limitar velocidad
            currentSpeed = Mathf.Clamp(
                currentSpeed,
                -maxSpeed,
                maxSpeed
            );
        }
        else
        {
            // Frenar poco a poco
            currentSpeed = Mathf.MoveTowards(
                currentSpeed,
                0f,
                deceleration * Time.fixedDeltaTime
            );
        }

        // =========================
        // MOVER PERSONAJE
        // =========================

        rb.velocity = transform.forward * currentSpeed;

        // =========================
        // A / D
        // =========================

        float rotacion = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            rotacion = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotacion = 1f;
        }

        // =========================
        // ROTAR PERSONAJE
        // =========================

        if (rotacion != 0)
        {
            float giro = rotacion * rotationSpeed * Time.fixedDeltaTime;

            Quaternion nuevaRotacion = Quaternion.Euler(
                0f,
                giro,
                0f
            );

            rb.MoveRotation(rb.rotation * nuevaRotacion);
        }
    }

    public void Jump()
    {
         // SALTAR
        if (Input.GetKeyDown(KeyCode.Space) && estaEnSuelo)
        {
            float velocidadSalto = Mathf.Sqrt(
                alturaSalto * -2f * gravedadSalto
            );

            rb.velocity = new Vector3(
                rb.velocity.x,
                velocidadSalto,
                rb.velocity.z
            );

            estaEnSuelo = false;
        }

        // GRAVEDAD DEL SALTO
        if (!estaEnSuelo)
        {
            rb.AddForce(
                Vector3.up * gravedadSalto,
                ForceMode.Acceleration
            );
        }
    }

    public void ZoomCamera()
    {
        GameObject camara = camaraPositions[0];
        GameObject ZoomOut = camaraPositions[1];
        GameObject ZoomIn = camaraPositions[2];

        bool hacerZoom = false;

        // Detectar si está apuntando
        if (Input.GetKey(KeyCode.I))
        {
            hacerZoom = true;
        }

        //Hacer el zoom con la cámara
        if (hacerZoom) {
            camara.transform.position = Vector3.Lerp(
                camara.transform.position,
                ZoomIn.transform.position,
                15f * Time.deltaTime
            );
            camara.transform.rotation = Quaternion.Lerp(
                camara.transform.rotation,
                ZoomIn.transform.rotation,
                15f * Time.deltaTime
            );
        }

        // Volver la camara a la posición original
        else{
            camara.transform.position = Vector3.Lerp(
                camara.transform.position,
                ZoomOut.transform.position,
                15f * Time.deltaTime
            );
            camara.transform.rotation = Quaternion.Lerp(
                camara.transform.rotation,
                ZoomOut.transform.rotation,
                15f * Time.deltaTime
            );
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            estaEnSuelo = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            estaEnSuelo = false;
        }
    }
}