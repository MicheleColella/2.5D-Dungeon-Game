using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake1 : MonoBehaviour
{
    public static CameraShake1 Instance { get; private set; }
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float ShakeTimer;
    public GameObject camerap;

    private void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float time, float delay)
    {
        StartCoroutine(Shake(intensity, time, delay));
    }

    IEnumerator Shake(float intensity, float time, float delay)
    {
        yield return new WaitForSeconds(delay);
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cinemachineVirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        ShakeTimer = time;
    }

    private void Update()
    {
        if(ShakeTimer > 0)
        {
            ShakeTimer -= Time.deltaTime;
            if(ShakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = 
                    cinemachineVirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0.0f;
#pragma warning disable CS0618 // Il tipo o il membro è obsoleto
                camerap.transform.rotation = Quaternion.EulerAngles(0.0f, 0.0f, 0.0f);
#pragma warning restore CS0618 // Il tipo o il membro è obsoleto
            }
        }
    }
}
