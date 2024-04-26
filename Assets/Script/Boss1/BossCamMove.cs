using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossCamMove : MonoBehaviour
{
    public RoomTemplates roomTemplates;
    public GameObject TargetCam;
    public GameObject Boss1;
    public bool Attacked;
    public bool Followed;
    private CinemachineVirtualCamera vcam;
    private bool done;
    private bool done1;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        roomTemplates = GameObject.Find("Room Templates").GetComponent<RoomTemplates>();
    }

    private void Update()
    {
        if (roomTemplates.RoomspawnedBoss && !done1)
        {
            TargetCam = GameObject.Find("TargetCam");
            transform.position = TargetCam.transform.position;
            Attacked = true;
            done1 = true;
        }

        if (roomTemplates.spawnedBoss && !done)
        {
            Boss1 = GameObject.Find("Boss1(Clone)");
            if(Boss1 != null)
            {
                vcam.Follow = Boss1.transform;
                Followed = true;
                done = true;
            }
        }
    }
}
