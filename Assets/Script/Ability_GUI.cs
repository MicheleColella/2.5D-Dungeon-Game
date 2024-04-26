using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability_GUI : MonoBehaviour
{
    [Header("Ability 1")]
    public Image abilityImage1;
    public float cooldown1 = 1;
    bool isCooldown = false;
    bool CanAbility1 = true;
    bool recharge1;
    bool canotability1;

    [Header("Ability 2")]
    public Image abilityImage2;
    public float cooldown2 = 5;
    bool isCooldown2 = false;
    bool CanAbility2 = true;
    bool recharge2;
    bool canotability2;

    [Header("Ability 3")]
    public Image abilityImage3;
    public float cooldown3 = 5;
    bool isCooldown3 = false;
    bool CanAbility3 = true;
    bool recharge3;
    bool canotability3;

    void Start()
    {
        StartCoroutine(startcooldown());
        StartCoroutine(startcooldown1());
        abilityImage1.fillAmount = 1;
        abilityImage2.fillAmount = 1;
        abilityImage3.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Ability1();
        Ability2();
        Ability3();
        //Debug.Log("canotability1 = " + canotability1);
        //Debug.Log("canotability2 = " + canotability2);
        //Debug.Log("canotability3 = " + canotability3);
    }

    private void Ability1()
    {
        float y = Input.GetAxis("Fire3");
        float x = Input.GetAxis("Fire4");

        if(!canotability3 && !canotability2)
        {
            if ((Input.GetKeyDown(KeyCode.Alpha1) || y == 1) && !CanAbility1)
            {
                canotability1 = true;
                isCooldown = true;
                CanAbility1 = true;
                abilityImage1.fillAmount = 1;
            }
        }

        if (isCooldown)
        {
            abilityImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;

            if(abilityImage1.fillAmount <= 0)
            {
                abilityImage1.fillAmount = 0;
                CanAbility1 = true;
                recharge1 = true;
                canotability1 = false;
            }
        }

        if (recharge1)
        {
            abilityImage1.fillAmount += 1 / 0.934f * Time.deltaTime;

            if (abilityImage1.fillAmount >= 1)
            {
                isCooldown = false;
                abilityImage1.fillAmount = 1;
                CanAbility1 = false;
                recharge1 = false;
            }
        }
    }

    private void Ability2()
    {
        float y = Input.GetAxis("Fire3");
        float x = Input.GetAxis("Fire4");

        if (!canotability3 && !canotability1)
        {
            if ((Input.GetKeyDown(KeyCode.Alpha2) || y == -1) && !CanAbility2)
            {
                canotability2 = true;
                isCooldown2 = true;
                CanAbility2 = true;
                abilityImage2.fillAmount = 1;
            }
        }

        if (isCooldown2)
        {
            abilityImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;

            if (abilityImage2.fillAmount <= 0)
            {
                abilityImage2.fillAmount = 0;
                CanAbility2 = true;
                recharge2 = true;
                canotability2 = false;
            }
        }

        if (recharge2)
        {
            abilityImage2.fillAmount += 2f / 7.5f * Time.deltaTime;

            if (abilityImage2.fillAmount >= 1)
            {
                isCooldown2 = false;
                abilityImage2.fillAmount = 1;
                CanAbility2 = false;
                recharge2 = false;
            }
        }
    }

    private void Ability3()
    {
        float y = Input.GetAxis("Fire3");
        float x = Input.GetAxis("Fire4");

        if (!canotability2 && !canotability1)
        {
            if ((Input.GetKeyDown(KeyCode.Alpha3) || x == -1) && !CanAbility3)
            {
                canotability3 = true;
                isCooldown3 = true;
                CanAbility3 = true;
                abilityImage3.fillAmount = 1;
            }
        }

        if (isCooldown3)
        {
            abilityImage3.fillAmount -= 1 / cooldown3 * Time.deltaTime;

            if (abilityImage3.fillAmount <= 0)
            {
                abilityImage3.fillAmount = 0;
                CanAbility3 = true;
                recharge3 = true;
                canotability3 = false;
            }
        }

        if (recharge3)
        {
            abilityImage3.fillAmount += 2f / 7.5f * Time.deltaTime;

            if (abilityImage3.fillAmount >= 1)
            {
                isCooldown3 = false;
                abilityImage3.fillAmount = 1;
                CanAbility3 = false;
                recharge3 = false;
            }
        }
    }

    IEnumerator startcooldown1()
    {
        yield return new WaitForSeconds(15);
        CanAbility1 = false;
    }
    IEnumerator startcooldown()
    {
        yield return new WaitForSeconds(15);
        CanAbility2 = false;
        CanAbility3 = false;
    }
}
