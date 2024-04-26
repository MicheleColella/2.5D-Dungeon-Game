using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_move : MonoBehaviour
{
    public float DestroyTime = 6f;
    public Vector3 offset = new Vector3(0, 2, 0);
    public Vector3 RandomizeIntensity = new Vector3(0.5f, 0, 0);

    private Camera thecam;

    public bool useStaticBillboard;
    void Start()
    {
        Destroy(gameObject, DestroyTime);
        transform.localPosition += offset;
        thecam = Camera.main;
    }

    private void LateUpdate()
    {
        if (!useStaticBillboard)
        {
            transform.LookAt(thecam.transform);
        }
        else
        {
            transform.rotation = thecam.transform.rotation;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
