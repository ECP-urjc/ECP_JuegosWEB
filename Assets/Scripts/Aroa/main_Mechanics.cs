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
    public int numCombo;
    public int curasEsparcidas;
    public string siguienteObjetivo;

    [Header("Atributos internos")]
    public int perdidaStaminaPorSalto;
    public string razonCombo;
    public int puntosPorSaltoEnPared;
    public int puntosPorSaltoEnRampa;

    [Header("Torres Iniciales")]
    public string[] torres = new string[3];

    [Header("Balas")]
    public int balasCargadas;
    public int balasTotales;
    public int maxBalas;

    [Header("Armas Cerca")]
    public bool tieneArma;
    public int usos;
    public int daño;
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
    public bool enPausa = false;
    private bool finalRampa;

    [Header("Salto de Pared")]
    public float fuerzaSaltoPared = 15f;
    private bool tocandoPared = false;
    private Vector3 normalPared;
    private Vector3 velocidadPared = Vector3.zero;
    public float velocidadAlineacion = 360f;
    public float suavizadoAlineacion = 8f;
    private bool alineando = false;
    private float anguloObjetivoY;

    [Header("Velocidad Tiempo")]
    public float velocidadNormal = 1f;
    public float velocidadMinima = 0.2f;
    public float velocidadLerp = 2f;
    public float segundosPorPunto = 0.1f;

    [Header("Internas")]
    public TextMeshProUGUI texto;
    private float currentSpeed = 0f;
    private bool estaEnSuelo;
    private bool quiereSaltar = false;
    private bool quiereAtacar = false;
    private bool restandoStamina = false;
    private float tiempoTexto;

    private Rigidbody rb;

    void Start()
    {
        texto.text = "";
        finalRampa = false;
        rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotationX |
        RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        rb.useGravity = true;

        siguienteObjetivo = EleccionPrimeraTorre();
    }

    public string EleccionPrimeraTorre()
    {
        return torres[Random.Range(0, torres.Length)];
    }

    void FixedUpdate()
    {
        Movement();
        Jump();
        Ataque();
    }

    void Update()
    {
        Relentizar();
        if(!enPausa)
        {
            JumpDetector();
            ZoomCamera();
        }
        ResetearTexto();
    }

    public void Movement()
    {
        rb.angularVelocity = Vector3.zero;

        float movimiento = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            movimiento = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movimiento = -1f;
        }

        if (movimiento == -1)
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

        Vector3 velHorizontal = transform.forward * currentSpeed;

        if (velocidadPared != Vector3.zero)
        {
            velocidadPared = Vector3.MoveTowards(
                velocidadPared,
                Vector3.zero,
                deceleration * Time.fixedDeltaTime
            );
        }

        rb.velocity = new Vector3(
            velHorizontal.x + velocidadPared.x,
            rb.velocity.y,
            velHorizontal.z + velocidadPared.z
        );

        float rotacion = 0f;
        if (Input.GetKey(KeyCode.A)) rotacion = -1f;
        else if (Input.GetKey(KeyCode.D)) rotacion = 1f;

        if (rotacion != 0)
        {
            // Si el jugador gira manualmente, se cancela la alineación
            alineando = false;
            transform.Rotate(0f, rotacion * rotationSpeed * Time.fixedDeltaTime, 0f);
        }
        else if (alineando)
        {
            // Suavizado exponencial: avanza una fracción de lo que falta cada frame
            float nuevoY = Mathf.LerpAngle(
                transform.eulerAngles.y,
                anguloObjetivoY,
                1f - Mathf.Exp(-suavizadoAlineacion * Time.fixedDeltaTime)
            );
            transform.rotation = Quaternion.Euler(0f, nuevoY, 0f);

            if (Mathf.Abs(Mathf.DeltaAngle(nuevoY, anguloObjetivoY)) < 0.2f)
            {
                transform.rotation = Quaternion.Euler(0f, anguloObjetivoY, 0f);
                alineando = false;
            }
        }
    }

    public void JumpDetector()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            quiereSaltar = true;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            quiereAtacar = true;
        }
    }

    public void Jump()
    {
        if (quiereSaltar && !estaEnSuelo && !tocandoPared)
        {
            quiereSaltar = false;
        }

        if (quiereSaltar && estaEnSuelo)
        {
            velocidadVertical = fuerzaSalto;
            if (finalRampa)
            {
                if (stamina >= perdidaStaminaPorSalto)
                {
                    velocidadVertical = fuerzaSalto * 2.5f;
                    currentSpeed = maxSpeed;
                    puntaje += puntosPorSaltoEnRampa * (numCombo + 1);
                    numCombo++;
                    razonCombo = "Rampa";

                    stamina -= perdidaStaminaPorSalto;
                }
                else
                {
                    texto.text = "Stamina insuficiente";
                }
            }

            estaEnSuelo = false;
            quiereSaltar = false;
        }
        else if (quiereSaltar && tocandoPared)
        {
            if (stamina >= perdidaStaminaPorSalto)
            {
                velocidadVertical = fuerzaSalto * 1.5f;

                // Velocidad hacia la normal de la pared, se da una sola vez
                velocidadPared = normalPared * fuerzaSaltoPared;

                tocandoPared = false;
                quiereSaltar = false;

                stamina -= perdidaStaminaPorSalto;

                puntaje += puntosPorSaltoEnPared * (numCombo + 1);
                numCombo++;
                razonCombo = "Pared";
            }
            else
            {
                texto.text = "Stamina insuficiente";
            }
        } 

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

        rb.velocity = new Vector3(
            rb.velocity.x,
            velocidadVertical,
            rb.velocity.z
        );
    }

    void IniciarAlineacion()
    {
        // Dirección hacia la pared (la normal apunta de la pared hacia ti)
        Vector3 haciaPared = -normalPared;

        // Ángulo de la pared respecto a tu frente (-180 a 180)
        float anguloPared = Vector3.SignedAngle(transform.forward, haciaPared, Vector3.up);

        // Lado más cercano: 0 = frente, 90 = derecha, 180 = detrás, -90 = izquierda
        float ladoMasCercano = Mathf.Round(anguloPared / 90f) * 90f;

        // Giro necesario para que la pared quede exactamente en ese lado
        float giroNecesario = anguloPared - ladoMasCercano;

        anguloObjetivoY = transform.eulerAngles.y + giroNecesario;
        alineando = true;
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
                15f * Time.unscaledDeltaTime
            );
            camara.transform.rotation = Quaternion.Lerp(
                camara.transform.rotation,
                ZoomIn.transform.rotation,
                15f * Time.unscaledDeltaTime
            );
            mira.SetActive(true);
        }

        // Volver la camara a la posición original
        else{
            camara.transform.position = Vector3.Lerp(
                camara.transform.position,
                ZoomOut.transform.position,
                15f * Time.unscaledDeltaTime
            );
            camara.transform.rotation = Quaternion.Lerp(
                camara.transform.rotation,
                ZoomOut.transform.rotation,
                15f * Time.unscaledDeltaTime
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

        bool mata = false;

        foreach (Collider enemigo in enemigos)
        {
            if (enemigo.CompareTag("Zombie"))
            {
                Zombie_Mechanics ZombieScript = enemigo.GetComponent<Zombie_Mechanics>();
                ZombieScript.vida -= daño;
                if(ZombieScript.vida <= 0){
                    ZombieScript.Morir(1);
                    mata = true;
                }
            }
        }

        if(mata)
        {
            numCombo++;
            razonCombo = "Bate";
        }

        usos--;
        if(usos <= 0){
            tieneArma = false;
        }
    }

    public void Relentizar()
    {
        float velocidadObjetivo;

        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            velocidadObjetivo = velocidadMinima;
            if(!restandoStamina){
                StartCoroutine(RestarStamina());
            }
        }
        else
        {
            velocidadObjetivo = velocidadNormal;
        }

        if(!enPausa)
        {
            Time.timeScale = Mathf.Lerp(
                Time.timeScale,
                velocidadObjetivo,
                velocidadLerp * Time.unscaledDeltaTime
            );
        }
        else
        {
            Time.timeScale = 0;
        }

        Time.fixedDeltaTime = 0.02f * Mathf.Max(Time.timeScale, 0.2f);
    }

    IEnumerator RestarStamina()
    {
        restandoStamina = true;

        while (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            stamina--;
            yield return new WaitForSecondsRealtime(segundosPorPunto);
        }

        restandoStamina = false;
    }

    public void ResetearTexto()
    {
        if(tiempoTexto >= 3)
        {
            texto.text = "";
        }

        if (texto.text != "")
        {
            tiempoTexto += Time.unscaledDeltaTime;
        }else
        {
            tiempoTexto = 0;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            foreach (ContactPoint contacto in collision.contacts)
            {
                // Si la normal apunta hacia arriba, el suelo está debajo
                if (contacto.normal.y > 0.7f)
                {
                    estaEnSuelo = true;
                    return;
                }
            }
            // Colisiona con "Suelo" pero no por debajo (pared, costado)
            estaEnSuelo = false;
        }

        if (collision.gameObject.CompareTag("Pared"))
        {
            ProcesarPared(collision);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            foreach (ContactPoint contacto in collision.contacts)
            {
                if (contacto.normal.y > 0.7f)
                {
                    estaEnSuelo = true;
                    return;
                }
            }
        }

        if (collision.gameObject.CompareTag("Pared"))
        {
            ProcesarPared(collision);
            if (tocandoPared)
            {
                IniciarAlineacion();
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            estaEnSuelo = false;
        }

        if (collision.gameObject.CompareTag("Pared"))
        {
            tocandoPared = false;
        }
    }

    void ProcesarPared(Collision collision)
    {
        Vector3 suma = Vector3.zero;
        int contactosLaterales = 0;

        foreach (ContactPoint contacto in collision.contacts)
        {
            if (Mathf.Abs(contacto.normal.y) < 0.3f)
            {
                suma += new Vector3(contacto.normal.x, 0f, contacto.normal.z);
                contactosLaterales++;
            }
        }

        if (contactosLaterales == 0) return;

        Vector3 normal = suma.normalized;

        // Asegurar que la normal apunta de la pared hacia el personaje
        Vector3 haciaPersonaje = transform.position - collision.GetContact(0).point;
        haciaPersonaje.y = 0f;
        if (Vector3.Dot(normal, haciaPersonaje) < 0f)
        {
            normal = -normal;
        }

        normalPared = normal;
        tocandoPared = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("finalRampa"))
        {
            finalRampa = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("finalRampa"))
        {
            finalRampa = false;
        }
    }
}