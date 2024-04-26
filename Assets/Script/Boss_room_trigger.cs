using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_room_trigger : MonoBehaviour
{
    public bool roomCheck;
    int cont = 0;
    public GameObject closedRoom;
    public GameObject door;

    public int waitTime;
    private void Update()
    {
        if (!roomCheck)
        {
            if(closedRoom != null)
            {
                //closedRoom.SetActive(true);
                StartCoroutine(activeClose());
            }
        }
        else
        {
            if (closedRoom != null)
            {
                //closedRoom.SetActive(false);
                StartCoroutine(deactiveClose());
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interior_Map") && cont == 0)
        {
            cont += 1;
            roomCheck = true;
            //Debug.Log("Touched a rail");
        }
    }

    IEnumerator activeClose()
    {
        yield return new WaitForSeconds(waitTime);
        closedRoom.SetActive(true);
    }
    IEnumerator activeDoor()
    {
        yield return new WaitForSeconds(waitTime);
        door.SetActive(true);
    }
    IEnumerator deactiveClose()
    {
        yield return new WaitForSeconds(waitTime);
        closedRoom.SetActive(false);
    }
    
    IEnumerator DeactiveDoor()
    {
        yield return new WaitForSeconds(waitTime);
        door.SetActive(false);
    }

}
