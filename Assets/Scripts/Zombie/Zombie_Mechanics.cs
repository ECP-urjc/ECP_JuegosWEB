using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class Zombie_Mechanics : MonoBehaviour
{
    [Header("Personaje")]
    public main_Mechanics personajeScript;
    
    [Header("Modelo")]
    public Animator ZombieAnimator;

    [Header("Atributos")]
    public int daño = 10;
    public int vida = 10;
    public int stamina = 10;
    public float tiempoEntreAtaques = 2f;
    public int puntaje = 10;
    private float timer = 0;
    private Coroutine corrutinaAtaque;
    private bool killed = false;
    private Vector3 deadPos;

    void Update()
    {
        if (killed)
        {
            timer += Time.deltaTime;
            transform.position = deadPos;

            if (timer >= 10f)
            {
                Destroy(gameObject);
            }
        }
    }
    public void Morir(int aux)
    {
        switch(aux){
            case 0:
                personajeScript.puntaje += puntaje * (personajeScript.numCombo + 1);
                personajeScript.numCombo++;
                personajeScript.razonCombo = "Disparo";
            break;
            case 1:
                personajeScript.puntaje += Mathf.RoundToInt(puntaje / 5f) * (personajeScript.numCombo + 1);
            break;
        }
        personajeScript.stamina += stamina;
        ZombieAnimator.SetTrigger("die");
        deadPos = transform.position;
        GetComponent<Collider>().enabled = false;
        gameObject.tag = "Untagged";
        killed = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ZombieAnimator.SetBool("atack", true);
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
            ZombieAnimator.SetBool("atack", false);
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
