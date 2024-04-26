using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delay_sword_damage : MonoBehaviour
{
    public Weapon Weapons;
    bool damage = false;

    public void attiva_danno()
    {
        damage = true;
        //Debug.Log("Attivo");
    }

    private void Update()
    {
        if(damage)
        {
            Weapons.Attack();
            damage = false;
        }
    }
}
