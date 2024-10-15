using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Bird Collector
    public int collectible = 0;
    public bool collectedAll = false;
    public bool nestFilled = false;

    // If contact with tornado, reset Birds
    public bool needToSpawnBirds = false;
    public bool deleteBirds = false;

    // Gem Collectors
    public int fireCore = 0;
    public bool collectedAllFire = false;
    public bool firePillarActive = false;
    public int earthCore = 0;
    public bool collectedAllEarth = false;
    public bool earthPillarActive = false;

    // Meet the Gamayun
    public bool meetGamayun = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Update Bird Counter
        if (collectible >= 5)
        {
            collectedAll = true;
        }
        else
        {
            collectedAll = false;
        }

        // Update Fire Gem Counter
        if (fireCore >= 4)
        {
            collectedAllFire = true;
        }
        else
        {
            collectedAllFire = false;
        }

        // Update Earth Gem Counter
        if (earthCore >= 4)
        {
            collectedAllEarth = true;
        }
        else
        {
            collectedAllEarth = false;
        }
    }
}
