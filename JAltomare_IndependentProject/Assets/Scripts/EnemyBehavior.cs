using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject enemyProjectile;
    private Transform target;
    public Transform shootPoint;
    public float shootRange = 10f;
    public float turnSpeed = 10f;
    public float projectileSpeed = 800f;
    public float fireRate = 2f;
    public int lives = 3;

    public GameObject corePrefab;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (lives <= 0)
        {
            Debug.Log("Enemy down.");
            Instantiate(corePrefab, this.transform.position + new Vector3 (0f, -3f, 0f), this.transform.rotation);
            Destroy(this.gameObject);
        }

        fireRate -= Time.deltaTime;

        Vector3 direction = target.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (distanceToPlayer <= shootRange && fireRate <= 0)
        {
            Shoot();
            fireRate = 2f;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Projectile(Clone)")
        {
            lives -= 1;
        }
    }
    void Shoot()
    {
        GameObject newEnemyProjectile = Instantiate(enemyProjectile, shootPoint.position, shootPoint.rotation);
        Rigidbody ProjectileRB = newEnemyProjectile.GetComponent<Rigidbody>();
        Transform target = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 direction = target.position - transform.position;
        ProjectileRB.AddForce(direction * projectileSpeed * Time.deltaTime, ForceMode.Impulse);
    }

}
