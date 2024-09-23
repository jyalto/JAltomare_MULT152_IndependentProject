using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public int collectible = 0;
    public bool collectedAll = false;
    public bool nestFilled = false;
    public bool needToSpawnBirds = false;
    public bool deleteBirds = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (collectible == 5)
        {
            collectedAll = true;
        }
        else
        {
            collectedAll = false;
        }
    }
}
