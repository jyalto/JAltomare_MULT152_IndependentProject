using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject birdPrefab; // Reference to the baby bird prefab
    public int numberOfBirds = 5; // Number of birds to spawn
    public Vector3[] positionArray = new[] { new Vector3(32f, 41f, 52f), new Vector3(42f, 41f, 62f), 
        new Vector3(66f, 41f, 80f), new Vector3(52f, 41f, 72f), new Vector3(80f, 41f, 55f) };

    GameController gc;

    void Start()
    {
        SpawnBirds();
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    void Update()
    {
        if (gc.deleteBirds == true)
        {
            Destroy(GameObject.FindGameObjectWithTag("BabyBird"));

        }
        if (gc.needToSpawnBirds == true)
        {
            gc.deleteBirds = false;
            SpawnBirds();
            gc.needToSpawnBirds = false;
        }
    }
    void SpawnBirds()
    {
        for (int i = 0; i < numberOfBirds; i++)
        {
            int positionArrayIndex = i; // Random.Range(0, positionArray.Length);
            Vector3 randPos = positionArray[positionArrayIndex];
            Instantiate(birdPrefab, randPos, birdPrefab.transform.rotation);
        }
    }
}
