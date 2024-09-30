using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 1000f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Transform target = GameObject.FindGameObjectWithTag("Player").transform;
        //Vector3 direction = target.position - transform.position;
        //rb.AddForce(direction * speed * Time.deltaTime);
    }
    private void Update()
    {
        //rb = GetComponent<Rigidbody>();
        Transform target = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 direction = target.position - transform.position;
        rb.AddForce(direction * speed * Time.deltaTime);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
                PlayerMovement.OnTakeDamage(10);

        Destroy(gameObject);
    }
}

