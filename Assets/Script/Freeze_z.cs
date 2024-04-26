using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze_z : MonoBehaviour
{
    public float Z;
    private void Update()
    {
        if (transform.position.z > Z)
            transform.position = new Vector3(transform.position.x, transform.position.y, Z);
        if (transform.position.z < Z)
            transform.position = new Vector3(transform.position.x, transform.position.y, Z);
    }
}
