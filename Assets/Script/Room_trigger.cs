using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_trigger : MonoBehaviour
{
    public bool triggered;

    [Header("Other Trigger")]
    public GameObject[] Room_Triggers;

    [Header("Spawner")]
    public GameObject[] Spawner;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            if (other.gameObject.tag == "Player")
            {
                //Debug.Log("HIT");
                
                for(int i = 0; i < Room_Triggers.Length; i++)
                {
                    //Debug.Log(i);
                    Room_Triggers[i].GetComponent<Room_trigger>().triggered = true;
                }
                
                triggered = true;
            }
        }
    }
}
