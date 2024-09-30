using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsRaised : MonoBehaviour
{
    private GameController gc;

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gc.firePillarActive == true && gc.earthPillarActive == true)
        {
            transform.position = new Vector3(59.2f, 103.8f, 184.7f);
        }
    }
}
