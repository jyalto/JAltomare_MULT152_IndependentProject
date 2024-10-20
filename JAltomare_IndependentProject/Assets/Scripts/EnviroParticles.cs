using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviroParticles : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem windSystem;
    private GameController gc;
    void Start()
    {
        windSystem.Play();
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gc.nestFilled == true)
        {
            windSystem.Stop();
        }
    }
}
