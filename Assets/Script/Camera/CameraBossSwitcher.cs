using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBossSwitcher : MonoBehaviour
{
    private Animator animator;
    public bool PlayerCam;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (PlayerCam)
        {
            animator.Play("BossCam");
        }
        else
        {
            animator.Play("PlayerCam");
        }
        PlayerCam = !PlayerCam;
    }

    public void SwitchState()
    {
        if (PlayerCam)
        {
            animator.Play("BossCam");
        }
        else
        {
            animator.Play("PlayerCam");
        }
        PlayerCam = !PlayerCam;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SwitchState();
        }
    }
}
