using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo2Script : MonoBehaviour
{

    public Animator animator;
    public AudioSource audioSource;


    private void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Movimiento playerMovement = other.GetComponent<Movimiento>();
            if (playerMovement != null && playerMovement.IsFalling())
            {
                //Die();
            }
            else
            {
                var damageable = other.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.AddDamage(1);
        
                }
            }
        }
    }

    }

  

