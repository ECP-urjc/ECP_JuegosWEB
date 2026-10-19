
using UnityEngine;

public class bala : MonoBehaviour
{
    [Header("Personaje")]
    public main_Mechanics personajeScript;

    [Header("Configuración")]
    public float velocidad = 20f;
    public int daño = 10;
    public float radioDeteccion = 0.08f;

    private Vector3 posicionAnterior;
    private bool impacto = false;

    void Start()
    {
        posicionAnterior = transform.position;
    }

    void Update()
    {
        if (impacto) return;

        float distancia = velocidad * Time.deltaTime;
        Vector3 direccion = transform.forward;

        // Comprobar todo el recorrido de la bala
        if (Physics.SphereCast(
            posicionAnterior,
            radioDeteccion,
            direccion,
            out RaycastHit hit,
            distancia))
        {
            ProcesarImpacto(hit.collider);
            return;
        }

        transform.position += direccion * distancia;
        posicionAnterior = transform.position;
    }

    void ProcesarImpacto(Collider other)
    {
        if (other.CompareTag("Traspasable") ||
            other.CompareTag("Player") ||
            other.CompareTag("Trigger"))
        {
            return;
        }

        // Buscar el zombie aunque el collider esté en un hijo
        Transform zombie = other.transform;

        while (zombie != null && !zombie.CompareTag("Zombie"))
        {
            zombie = zombie.parent;
        }

        if (zombie != null)
        {
            Zombie_Mechanics ZombieScript = zombie.GetComponent<Zombie_Mechanics>();
            ZombieScript.vida -= daño;
            if(ZombieScript.vida <= 0){
                ZombieScript.Morir(0);
            }
            Destroy(gameObject);
            return;
        }

        // Desaparecer al tocar un objeto sólido no atravesable
        impacto = true;
        Destroy(gameObject);
    }
}
