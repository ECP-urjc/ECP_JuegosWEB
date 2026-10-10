using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Zombie_spawner_prueba : MonoBehaviour
{
    [Header("Configuración")]
    public GameObject prefabZombie;
    public float tiempoEntreZombies = 3f;
    public int maxZombies = 10;

    [Header("Zona de aparición")]
    public UnityEngine.Vector3 tamanoMapa = new UnityEngine.Vector3(50f, 0f, 50f);

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
        float x = Random.Range(-tamanoMapa.x / 2f, tamanoMapa.x / 2f);
        float z = Random.Range(-tamanoMapa.z / 2f, tamanoMapa.z / 2f);

        UnityEngine.Vector3 posicion = transform.position + new UnityEngine.Vector3(x, 1f, z);

        GameObject zombie = Instantiate(
            prefabZombie,
            posicion,
            UnityEngine.Quaternion.identity
        );

        zombiesActuales++;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, tamanoMapa);
    }
}
