using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EathPillarActive : MonoBehaviour
{
    GameController gc;
    public GameObject activeEarthTower;

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gc.collectedAllEarth == true)
        {
            Destroy(gameObject);

            Instantiate(activeEarthTower, activeEarthTower.transform.position, activeEarthTower.transform.rotation);

            gc.earthPillarActive = true;
        }
    }
}
