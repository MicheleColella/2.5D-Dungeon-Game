using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloaringText : MonoBehaviour
{
    public float DestroyTime = 3f;
    public Vector3 offset = new Vector3(0, 2, 0);
    public Vector3 RandomizeIntensity = new Vector3(0.5f, 0, 0);

    private Camera thecam;

    public bool useStaticBillboard;
    void Start()
    {
        Destroy(gameObject, DestroyTime);
        transform.localPosition += offset;
        transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
        Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y),
        Random.Range(-RandomizeIntensity.z, RandomizeIntensity.z));
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
