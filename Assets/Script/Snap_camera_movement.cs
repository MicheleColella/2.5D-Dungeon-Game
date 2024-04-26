using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap_camera_movement : MonoBehaviour
{
    public Transform player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
        Mathf.RoundToInt(transform.position.x),
        Mathf.RoundToInt(transform.position.y),
        transform.position.z
        );
    }
}
