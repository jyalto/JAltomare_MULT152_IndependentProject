using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsRaised : MonoBehaviour
{
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.firePillarActive == true && gameManager.earthPillarActive == true)
        {
            transform.position = new Vector3(26.85f, 54.56f, 118.58f);
        }
    }
}
