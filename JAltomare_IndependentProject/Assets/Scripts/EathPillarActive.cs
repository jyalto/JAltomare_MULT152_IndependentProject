using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EathPillarActive : MonoBehaviour
{
    GameController gc;
    public ParticleSystem iceSystem;

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        iceSystem = GetComponentInChildren<ParticleSystem>();
    }
    private void Update()
    {
        if (gc.gameOver == true)
        {
            iceSystem.Stop();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gc.collectedAllEarth == true)
        {
            iceSystem.Play();
            gc.earthPillarActive = true;
        }
    }
}
