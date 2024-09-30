using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private int lives = 3;
    public int enemyLives
    {
        get { return lives; }

        private set
        {
            lives = value;

            if (lives <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy down.");
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
