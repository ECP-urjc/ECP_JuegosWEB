using System.Linq;
using UnityEngine;

public class bala : MonoBehaviour
{
    [Header("Personaje")]
    public main_Mechanics personajeScript;

    [Header("Configuración")]
    public float velocidad = 20f;
    public int daño = 10;
    public float radioDeteccion = 0.08f;

    private bool impacto = false;

    void Update()
    {
        if (impacto) return;

        float distancia = velocidad * Time.deltaTime;
        Vector3 direccion = transform.forward;

        RaycastHit[] hits = Physics.SphereCastAll(
            transform.position,
            radioDeteccion,
            direccion,
            distancia,
            ~0,
            QueryTriggerInteraction.Ignore
        );

        // Ordenar de más cerca a más lejos
        System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

        foreach (RaycastHit hit in hits)
        {
            if (EsIgnorable(hit.collider)) continue;

            ProcesarImpacto(hit.collider);
            return;
        }

        transform.position += direccion * distancia;
    }

    bool EsIgnorable(Collider other)
    {
        return other.CompareTag("Traspasable") ||
               other.CompareTag("Player") ||
               other.CompareTag("Trigger");
    }

    void ProcesarImpacto(Collider other)
    {
        impacto = true;

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
            if (ZombieScript.vida <= 0)
            {
                ZombieScript.Morir(0);
            }
        }

        Destroy(gameObject);
    }
}