using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EathPillarActive : MonoBehaviour
{
    public GameManager gameManager;
    public ParticleSystem iceSystem;

    // Start is called before the first frame update
    void Start()
    {
        iceSystem = GetComponentInChildren<ParticleSystem>();
    }
    private void Update()
    {
        if (gameManager.gameOver == true)
        {
            iceSystem.Stop();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.collectedAllEarth == true)
        {
            iceSystem.Play();
            gameManager.earthPillarActive = true;
        }
    }
}
