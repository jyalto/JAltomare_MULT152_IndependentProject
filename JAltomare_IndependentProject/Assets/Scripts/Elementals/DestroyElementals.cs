using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyElementals : MonoBehaviour
{
    private void Update()
    {
        if (GameManager.Instance.deleteElementals == true)
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Elemental");
            foreach (GameObject obj in objectsWithTag)
            {
                Destroy(obj);
            }
            GameManager.Instance.deleteElementals = false;
            GameManager.Instance.needtoSpawnElementals = true;
        }


    }
    private void OnTriggerEnter(Collider other)
    {

    }
}
