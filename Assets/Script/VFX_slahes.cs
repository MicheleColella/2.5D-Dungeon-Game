using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_slahes : MonoBehaviour
{
    public GameObject Timemanager;
    public TimeManager timeManager;

    [Header("VFX_Gameobjects")]
    public GameObject slashes;
    public GameObject slash1;
    public GameObject slash2; 
    public GameObject slash3;
    public GameObject slash4;

    bool canvfx1;

    public GameObject Sprites;
    public float Direction;
    public float Rotation;

    void Start()
    {
        Timemanager = GameObject.Find("TimeManager");
        timeManager = Timemanager.GetComponent<TimeManager>();

        canvfx1 = true;

        slash1.SetActive(false);
        slash2.SetActive(false);
        slash3.SetActive(false);
        slash4.SetActive(false);
    }


    void Update()
    {
        Direction = Sprites.transform.localScale.x;
        Rotation = Sprites.transform.rotation.eulerAngles.z;
        slashes.transform.rotation = Quaternion.Euler(Sprites.transform.rotation.eulerAngles.x, Sprites.transform.rotation.eulerAngles.y, Rotation);
        slashes.transform.localScale = new Vector3(Direction, slashes.transform.localScale.y, slashes.transform.localScale.z);

        if (Input.GetKeyDown(KeyCode.K))
        {
            VFX_Slash2();
        }
    }

    public void VFX_Slash1()
    {
        if (canvfx1)
        {
            StartCoroutine(VFX_Slashcool1());
        }
    }
    public void VFX_Slash2()
    {
        StartCoroutine(VFX_Slashcool2());
    }

    public void VFX_Slash3()
    {
        StartCoroutine(VFX_Slashcool3());
    }

    public void VFX_Slash4()
    {
        StartCoroutine(VFX_Slashcool4());
    }

    IEnumerator VFX_Slashcool1()
    {
        slash1.SetActive(true);
        canvfx1 = false;
        yield return new WaitForSeconds(0.5f);
        canvfx1 = true;
        slash1.SetActive(false);
    }

    IEnumerator VFX_Slashcool2()
    {
        slash2.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        slash2.SetActive(false);
    }

    IEnumerator VFX_Slashcool3()
    {
        slash3.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        slash3.SetActive(false);
    }

    IEnumerator VFX_Slashcool4()
    {
        slash4.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        slash4.SetActive(false);
    }
}
