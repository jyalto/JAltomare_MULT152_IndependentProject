using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float OnscreenDelay = 2f;
    private AudioSource asFireProj;
    public AudioClip fireImpact;
    void Start()
    {
        Destroy(this.gameObject, OnscreenDelay);
        asFireProj = GetComponent<AudioSource>();
    }
    private void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !GameManager.Instance.playerDead)
        {
            PlayerController.OnTakeDamage(20);
            asFireProj.PlayOneShot(fireImpact);
            Destroy(gameObject);
        }
    }
}

