using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBirds : MonoBehaviour
{
    GameController gc;
    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        if (gc.deleteBirds == true)
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("BabyBird");
            foreach (GameObject obj in objectsWithTag) 
            {
                Destroy(obj);
            }
            gc.deleteBirds = false;
            gc.needToSpawnBirds = true;
        }


    }
    private void OnTriggerEnter(Collider other)
    {

    }
}
