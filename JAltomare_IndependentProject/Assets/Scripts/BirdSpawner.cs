using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject birdPrefab; // Reference to the baby bird prefab
    public int numberOfBirds = 5; // Number of birds to spawn

    public Vector3[] positionArray = new[] { new Vector3(53f, 41f, 65f), new Vector3(91f, 41f, 42f), new Vector3(32f, 41f, 46f), new Vector3(76f, 41f, 5f), new Vector3(45f, 41f, 0f) };

    GameController gc;

    void Start()
    {
        SpawnBirds();
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    void Update()
    {
        if (gc.needToSpawnBirds == true)
        {
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
