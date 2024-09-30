using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject enemyProjectile;
    private Transform target;
    public Transform shootPoint;
    public float shootRange = 10f;
    public float turnSpeed = 10f;
    public float fireRate = 1f;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        fireRate -= Time.deltaTime;

        Vector3 direction = target.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (distanceToPlayer <= shootRange && fireRate <= 0)
        {
            Shoot();
            fireRate = 1f;
        }
    }
    void Shoot()
    {
        Instantiate(enemyProjectile, shootPoint.position, shootPoint.rotation);
    }
}
