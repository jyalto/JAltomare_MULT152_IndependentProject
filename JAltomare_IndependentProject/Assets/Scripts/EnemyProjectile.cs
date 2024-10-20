using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Transform target = GameObject.FindGameObjectWithTag("Player").transform;
        //Vector3 direction = target.position - transform.position;
        //rb.AddForce(direction * speed * Time.deltaTime);
    }
    private void Update()
    {

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
                PlayerController.OnTakeDamage(10);
        Destroy(gameObject);
    }
}

