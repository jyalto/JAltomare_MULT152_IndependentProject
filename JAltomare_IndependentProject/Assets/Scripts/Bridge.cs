using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    GameController gc;
    public GameObject bridge1; // Reference to the baby bird prefab

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gc.nestFilled == true)
        {
            transform.position = new Vector3(-22.16f, 41.35f, -59.43f);
        }
    }
}
