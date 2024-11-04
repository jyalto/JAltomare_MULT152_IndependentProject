using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2 : MonoBehaviour
{
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameOver == true)
        {
            transform.position = new Vector3(31.5f, 58.9f, 128.11f);
        }
    }
}
