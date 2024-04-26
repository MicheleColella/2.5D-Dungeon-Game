using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_room_central : MonoBehaviour
{
    public GameObject closedRoom;
    public GameObject door;
    public Boss_room_trigger boss_Room_Trigger;


    void Update()
    {
        gameObject.SetActive(boss_Room_Trigger.roomCheck);
    }
}
