//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TornadoSpawner : MonoBehaviour
//{
//    public GameObject tornadoPrefab; // Reference to the baby bird prefab
//    public Vector3 tornadoStart = new Vector3(84f, 41f, 50f);
//    public Vector3 tornadoEnd = new Vector3(80f, 41f, 50f);

//    void Start()
//    {
//        InvokeRepeating("SpawnTornado", 3.0f, 1.5f);
//    }
//    void SpawnTornado()
//    {
//        Instantiate(tornadoPrefab, tornadoStart, tornadoPrefab.transform.rotation);

//        if (transform.position == tornadoEnd)
//        {
//            Destroy(gameObject);
//        }
//    }
//    void StopSpawning()
//    {
//        CancelInvoke("SpawnTornado");
//    }
//}
