using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MeetFireElder : MonoBehaviour
{
    public GameObject fireElder;
    public Animator fireAnim;
    // Start is called before the first frame update
    void Start()
    {
        fireElder = GameObject.Find("FireElder");
        fireAnim = fireElder.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.canShootFire = true;
            fireAnim.SetTrigger("playerMet");
        }
    }


}
