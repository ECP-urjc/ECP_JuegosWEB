using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class main_Mechanics : MonoBehaviour
{
    [Header("Cámara")]
    public GameObject[] camaraPositions;
    public GameObject mira;

    [Header("Atributos")]
    public int vida;
    public int stamina;
    public int puntaje;

    [Header("Armas")]
    public bool tieneArma;
    public int usos;
    public int puntajePorAtaqueCerca;
    public Collider zonaAtaque;

    [Header("Movimiento")]
    public float maxSpeed = 70f;
    public float acceleration = 20f;
    public float deceleration = 50f;
    public float marchaatrasMax = 10f;
    public float rotationSpeed = 90f;
    public float fuerzaSalto = 12f;
    public float gravedad = 30f;
    private float velocidadVertical = 0f;

    [Header("Internas")]
    public TextMeshProUGUI texto;
    private float currentSpeed = 0f;
    private bool estaEnSuelo;
    private bool quiereSaltar = false;
    private bool quiereAtacar = false;

    private Rigidbody rb;

    void Start()
    {
        texto.text = "";
        rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotationX |
        RigidbodyConstraints.FreezeRotationZ;

        rb.useGravity = true;
    }

    void FixedUpdate()
    {
        Movement();
        Jump();
        Ataque();
    }

    void Update()
    {
        ZoomCamera();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            quiereSaltar = true;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            quiereAtacar = true;
        }

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

        if (movimiento == -1 && estaEnSuelo)
        {
            // Frenar doble de rápido
            currentSpeed = Mathf.MoveTowards(
                currentSpeed,
                -marchaatrasMax,
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

        rb.velocity = new Vector3(
            transform.forward.x * currentSpeed,
            rb.velocity.y,
            transform.forward.z * currentSpeed
        );

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
            if (quiereSaltar && estaEnSuelo)
        {
            velocidadVertical = fuerzaSalto;
            estaEnSuelo = false;
        }

        quiereSaltar = false;

        // Gravedad
        if (!estaEnSuelo)
        {
            velocidadVertical -= gravedad * Time.fixedDeltaTime;
        }
        else
        {
            if (velocidadVertical < 0)
            {
                velocidadVertical = 0;
            }
        }

        Vector3 velocidad = rb.velocity;
        velocidad.y = velocidadVertical;
        rb.velocity = velocidad;
    }

    public void ZoomCamera()
    {
        GameObject camara = camaraPositions[0];
        GameObject ZoomOut = camaraPositions[1];
        GameObject ZoomIn = camaraPositions[2];

        bool hacerZoom = false;

        // Detectar si está apuntando
        if (Input.GetKey(KeyCode.P) || Input.GetMouseButton(1))
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
            mira.SetActive(true);
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
            mira.SetActive(false);
        }
    }

    public void Ataque()
    {
        if (!quiereAtacar) {return;}

        quiereAtacar = false;

        if (!tieneArma) {return;}

        Collider[] enemigos = Physics.OverlapBox(
            zonaAtaque.bounds.center,
            zonaAtaque.bounds.extents,
            zonaAtaque.transform.rotation
        );

        foreach (Collider enemigo in enemigos)
        {
            if (enemigo.CompareTag("Zombie"))
            {
                Destroy(enemigo.gameObject);
                puntaje += puntajePorAtaqueCerca;
            }
        }

        usos--;
        if(usos <= 0){
            tieneArma = false;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            estaEnSuelo = true;
        }
    }

    void OnCollisionEnter(Collision collision)
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