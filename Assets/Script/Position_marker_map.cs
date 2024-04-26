using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position_marker_map : MonoBehaviour
{
    public GameObject position_map;
    public bool triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log(true);
            triggered = true;
            position_map.SetActive(triggered);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(false);
        triggered = false;
        position_map.SetActive(triggered);
    }
}
