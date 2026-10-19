using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie_Mechanics : MonoBehaviour
{
    [Header("Personaje")]
    public main_Mechanics personajeScript;

    [Header("Vida")]
    public int daño = 10;
    public int vida = 10;
    public int stamina = 10;
    public float tiempoEntreAtaques = 2f;
    public int puntaje = 10;

    private Coroutine corrutinaAtaque;

    public void Morir(int aux)
    {
        switch(aux){
            case 0:
                personajeScript.puntaje += puntaje;
            break;
            case 1:
                personajeScript.puntaje += Mathf.RoundToInt(puntaje / 5f);
            break;
        }
        personajeScript.stamina += stamina;
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (corrutinaAtaque == null)
            {
                corrutinaAtaque = StartCoroutine(AtaqueContinuo());
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (corrutinaAtaque != null)
            {
                StopCoroutine(corrutinaAtaque);
                corrutinaAtaque = null;
            }
        }
    }

    private IEnumerator AtaqueContinuo()
    {
        while (true)
        {
            personajeScript.vida -= daño;

            if (personajeScript.vida < 0)
            {
                personajeScript.vida = 0;
            }

            yield return new WaitForSeconds(tiempoEntreAtaques);
        }
    }
}
