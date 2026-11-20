
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie_spawner_prueba : MonoBehaviour
{
    [Header("Configuracion")]
    public GameObject prefabZombie;
    public float tiempoEntreZombies = 3f;
    public int maxZombies = 10;

    [Header("Zona de aparicion")]
    public Vector3 tamanoMapa = new Vector3(50f, 0f, 50f);

    [Header("NavMesh")]
    public float radioBusqueda = 3f;
    public int intentosMaximos = 10;

    private float temporizador = 0f;
    private int zombiesActuales = 0;

    void Update()
    {
        temporizador += Time.deltaTime;

        if (temporizador >= tiempoEntreZombies &&
            zombiesActuales < maxZombies)
        {
            GenerarZombie();
            temporizador = 0f;
        }
    }

    void GenerarZombie()
    {
        for (int i = 0; i < intentosMaximos; i++)
        {
            // Posicion aleatoria dentro de la zona
            float x = Random.Range(-tamanoMapa.x / 2f, tamanoMapa.x / 2f);
            float z = Random.Range(-tamanoMapa.z / 2f, tamanoMapa.z / 2f);

            Vector3 posicion = transform.position + new Vector3(x, 0f, z);

            // Buscar una posicion valida en el NavMesh
            NavMeshHit hit;

            if (NavMesh.SamplePosition(
                posicion,
                out hit,
                radioBusqueda,
                NavMesh.AllAreas))
            {
                // Crear zombie en la posicion encontrada
                GameObject nuevoZombie = Instantiate(
                    prefabZombie,
                    hit.position,
                    Quaternion.identity
                );

                zombiesActuales++;
                return;
            }
        }

        Debug.LogWarning("No se ha encontrado una posicion valida para el zombie.");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            transform.position,
            tamanoMapa
        );
    }
}
