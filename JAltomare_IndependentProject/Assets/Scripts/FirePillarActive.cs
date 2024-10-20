using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePillarActive : MonoBehaviour
{
    GameController gc;
    public ParticleSystem fireSystem;

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        fireSystem = GetComponentInChildren<ParticleSystem>();
    }
    private void Update()
    {
        if (gc.gameOver == true)
        {
            fireSystem.Stop();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gc.collectedAllFire == true)
        {
            fireSystem.Play();
            gc.firePillarActive = true;
        }

    }
}
