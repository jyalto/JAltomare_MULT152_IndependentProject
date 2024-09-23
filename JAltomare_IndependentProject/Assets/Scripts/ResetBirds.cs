using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBirds : MonoBehaviour
{
    GameController gc;

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gc.collectible = 0;
            gc.deleteBirds = true;
            gc.needToSpawnBirds = true;
        }
    }
}
