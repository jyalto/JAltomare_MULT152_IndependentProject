using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private int lives = 3;

    public GameObject corePrefab;
    public int enemyLives
    {
        get { return lives; }

        private set
        {
            lives = value;

            if (lives <= 0)
            {
                // Debug.Log("Enemy down.");
                Instantiate(corePrefab, this.transform.position, this.transform.rotation);
                Destroy(this.gameObject);
            }
        }
    }
    private void Start()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Projectile(Clone)")
        {
            enemyLives -= 1;
            Debug.Log("Critical Hit!");
        }
    }
}
