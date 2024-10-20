using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject birdPrefab; // Reference to the baby bird prefab
    public int numberOfBirds = 5; // Number of birds to spawn

    public Vector3[] positionArray1 = new[] { new Vector3(29.0f, 50.5f, 16.8f), new Vector3(5.1f, 52.0f, 32.2f), new Vector3(7.2f, 52.0f, 58.3f), new Vector3(41.7f, 52.5f, 60.8f), new Vector3(47.1f, 52.5f, 29.7f)};
    public Vector3[] positionArray2 = new[] { new Vector3(29.0f, 50.5f, 16.8f), new Vector3(18.7f, 52.0f, 28.1f), new Vector3(11.9f, 52.0f, 50.7f), new Vector3(29.5f, 52.5f, 65.5f), new Vector3(46.9f, 52.5f, 43.9f) };
    public Vector3[] positionArray3 = new[] { new Vector3(29.0f, 50.5f, 16.8f), new Vector3(12.7f, 52.0f, 21.5f), new Vector3(15.3f, 52.0f, 39.9f), new Vector3(22.1f, 52.5f, 64.9f), new Vector3(36.2f, 52.4f, 42.3f) };
    public Vector3[] positionArray4 = new[] { new Vector3(29.0f, 50.5f, 16.8f), new Vector3(5.6f, 52.0f, 42.1f), new Vector3(15.4f, 53.17f, 62.1f), new Vector3(33f, 52.5f, 56.1f), new Vector3(32.2f, 51.0f, 33.4f) };
    
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
        // choose which array of locations to spawn
        int randNum = Random.Range(0, 4);

        if (randNum == 0) 
        {
            for (int i = 0; i < positionArray1.Length; i++)
            {
                Vector3 birdPosition = positionArray1[i];
                Instantiate(birdPrefab, birdPosition, birdPrefab.transform.rotation);
            }
        }
        else if (randNum == 1) 
        {
            for (int i = 0; i < positionArray2.Length; i++)
            {
                Vector3 birdPosition = positionArray2[i];
                Instantiate(birdPrefab, birdPosition, birdPrefab.transform.rotation);
            }
        }
        else if (randNum == 2) 
        {
            for (int i = 0; i < positionArray3.Length; i++)
            {
                Vector3 birdPosition = positionArray2[i];
                Instantiate(birdPrefab, birdPosition, birdPrefab.transform.rotation);
            }
        }
        else
        {
            for (int i = 0; i < positionArray4.Length; i++)
            {
                Vector3 birdPosition = positionArray4[i];
                Instantiate(birdPrefab, birdPosition, birdPrefab.transform.rotation);
            }
        }
    }
}
