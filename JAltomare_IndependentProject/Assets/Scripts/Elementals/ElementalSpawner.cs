using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalSpawner : MonoBehaviour
{
    public GameObject fireElementalPrefab;
    public GameObject iceElementalPrefab;
    public int numberOfElementals = 4;

    public Vector3[] positionArrayFire = new[] { new Vector3(-28.7f, 57.65f, 204.8f), 
                                             new Vector3(-19.42f, 57.65f, 204.8f), 
                                             new Vector3(-9f, 57.65f, 204.8f), 
                                             new Vector3(-40.0f, 57.65f, 204.8f)};

    public Vector3[] positionArrayIce = new[] { new Vector3(55.46f, 57.65f, 204.8f),
                                             new Vector3(65.46f, 57.65f, 204.8f),
                                             new Vector3(75f, 57.65f, 204.8f),
                                             new Vector3(85f, 57.65f, 204.8f)};

    // Start is called before the first frame update
    void Start()
    {
        SpawnFireElementals();
        SpawnIceElementals();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.needtoSpawnElementals == true)
        {
            SpawnFireElementals();
            SpawnIceElementals();
            GameManager.Instance.needtoSpawnElementals = false;
        }
    }

    void SpawnFireElementals()
    {
        for (int i = 0; i < positionArrayFire.Length; i++)
        {
            Vector3 firePosition = positionArrayFire[i];
            Instantiate(fireElementalPrefab, firePosition, fireElementalPrefab.transform.rotation);
        }
    }
    void SpawnIceElementals()
    {
        for (int i = 0; i < positionArrayIce.Length; i++)
        {
            Vector3 icePosition = positionArrayIce[i];
            Instantiate(iceElementalPrefab, icePosition, iceElementalPrefab.transform.rotation);
        }
    }
}
