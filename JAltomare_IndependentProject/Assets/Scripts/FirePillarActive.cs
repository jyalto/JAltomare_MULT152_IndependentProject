using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePillarActive : MonoBehaviour
{
    GameController gc;
    public GameObject activeFireTower; 

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gc.collectedAllFire == true)
        {
            Destroy(gameObject);

            Instantiate(activeFireTower, activeFireTower.transform.position, activeFireTower.transform.rotation);

            gc.firePillarActive = true;
        }
    }
}
