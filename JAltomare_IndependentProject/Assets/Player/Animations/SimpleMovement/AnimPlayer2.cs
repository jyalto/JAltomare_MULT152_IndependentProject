using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlayer2 : MonoBehaviour
{
    Animator animator;
    private GameController gc;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        // FORWARD
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("runForward", true);
        }
        if (!Input.GetKey(KeyCode.W))
        {
            animator.SetBool("runForward", false);
        }

        // LEFT
        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("runLeft", true);
        }
        if (!Input.GetKey(KeyCode.A))
        {
            animator.SetBool("runLeft", false);
        }

        // RIGHT
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("runRight", true);
        }
        if (!Input.GetKey(KeyCode.D))
        {
            animator.SetBool("runRight", false);
        }

        // BACK
        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("runBackward", true);
        }
        if (!Input.GetKey(KeyCode.S))
        {
            animator.SetBool("runBackward", false);
        }
    }
}
