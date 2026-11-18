using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AroaAnimations : MonoBehaviour
{
    public main_Mechanics personajeScript;
    public Animator animator;
    public Rigidbody rb;

    void Update()
    {
        animator.SetFloat("velocity", rb.velocity.magnitude);
        animator.SetBool("enSuelo", personajeScript.estaEnSuelo);
        animator.SetBool("vivo", personajeScript.vivo);
    }
}
