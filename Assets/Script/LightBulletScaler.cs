using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulletScaler : MonoBehaviour
{
    public Transform Bullet;
    int scalingFramesLeft = 0;
    public Light lt;
    public float speed;
    private Vector3 actualscale;

    private void Start()
    {
        lt = GetComponent<Light>();
        actualscale = Bullet.transform.localScale;
    }
    void Update()
    {
        
        if (Bullet.transform.localScale != actualscale)
        {
            scalingFramesLeft = 10;
        }

        if (scalingFramesLeft > 0)
        {
            lt.range = Mathf.Lerp(2, 0.0f, Time.deltaTime * speed);

            scalingFramesLeft--;
        }
    }
}
