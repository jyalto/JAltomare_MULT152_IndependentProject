using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music1 : MonoBehaviour
{
    private AudioSource asMusic1Vol;
    // Start is called before the first frame update
    void Start()
    {
        asMusic1Vol = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            asMusic1Vol.Play();
        }

    }
}
