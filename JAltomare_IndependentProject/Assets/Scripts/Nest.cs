using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour
{ 
    GameController gc;
    public GameObject birdGroup;


    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gc.collectedAll == true)
        {
        Instantiate(birdGroup, birdGroup.transform.position, birdGroup.transform.rotation);
        gc.nestFilled = true;
        }
    }
}   
