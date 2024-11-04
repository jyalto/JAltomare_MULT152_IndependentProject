using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MeetIceElder : MonoBehaviour
{
    public GameObject iceElder;
    public Animator iceAnim;
    // Start is called before the first frame update
    void Start()
    {
        iceElder = GameObject.Find("IceElder");
        iceAnim = iceElder.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.canShootIce = true;
            iceAnim.SetTrigger("playerMet");
        }
    }


}
