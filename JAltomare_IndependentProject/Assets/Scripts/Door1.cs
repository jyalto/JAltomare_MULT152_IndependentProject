using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1 : MonoBehaviour
{
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.nestFilled == true)
        {
            transform.position = new Vector3(31.5f, 46.36f, 66.81f);
        }
    }
}
