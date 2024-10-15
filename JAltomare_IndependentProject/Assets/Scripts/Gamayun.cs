using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamayun : MonoBehaviour
{
    private GameController gc;

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gc.meetGamayun = true;
        }
    }
}
