using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePillarActive : MonoBehaviour
{
    public GameManager gameManager;
    public ParticleSystem fireSystem;

    // Start is called before the first frame update
    void Start()
    {

        fireSystem = GetComponentInChildren<ParticleSystem>();
    }
    private void Update()
    {
        if (gameManager.gameOver == true)
        {
            fireSystem.Stop();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.collectedAllFire == true)
        {
            fireSystem.Play();
            gameManager.firePillarActive = true;
        }

    }
}
