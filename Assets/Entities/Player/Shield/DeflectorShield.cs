using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectorShield : MonoBehaviour {

    [HideInInspector]
    public float shieldDuration;
    [HideInInspector]
    public float shieldHealth;

    public AudioClip shieldOnlineSound, shieldOfflineSound;

    PlayerController playerController;
    Animator animator;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        AudioSource.PlayClipAtPoint(shieldOnlineSound, transform.position, 1.0f);
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Projectile beam = collider.GetComponent<Projectile>();
        if (beam)
        {
            shieldHealth -= beam.GetDamage();
            beam.Hit();
            Debug.Log("Shield health remaining: " + shieldHealth);
            animator.Play("Shield Hit");
            if (shieldHealth < 0) { ShieldOff(); }
        }
    }

    private void Update()
    {
        shieldDuration -= Time.deltaTime;
        if (shieldDuration < 0)
        {
            ShieldOff();
        }
    }

    private void ShieldOff ()
    {
        AudioSource.PlayClipAtPoint(shieldOfflineSound, transform.position, 1.0f);
        playerController.shieldOnline = false;
        Destroy(gameObject);
    }
}

