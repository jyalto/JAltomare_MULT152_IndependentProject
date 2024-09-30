using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 10000f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Transform target = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 direction = target.position - transform.position;
        rb.AddForce(direction * speed * Time.deltaTime);
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Destroy(gameObject);
    //}
}
