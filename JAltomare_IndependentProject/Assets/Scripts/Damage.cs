using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Damage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            PlayerController.OnTakeDamage(15);
    }
}
