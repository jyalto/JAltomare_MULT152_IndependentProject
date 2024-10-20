using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverVol : MonoBehaviour
{
    private GameController gc;
    private AudioSource Music1Vol;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        Music1Vol = GameObject.FindGameObjectWithTag("MusicVolume").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gc.gameOver = true;
            Music1Vol.Stop();
            player.gameObject.GetComponent<Animator>().enabled = false;
        }
    }
}
