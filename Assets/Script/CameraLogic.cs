using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraLogic : MonoBehaviour
{
    public GroundedCharacterController groundedCharacter;
    public bool hide_mouse;
    public int Limit_framerate;
    public CinemachineVirtualCamera virtualCamera;
    public float camdis;
    public float PlayerVelocity;
    public GameObject Sprites;
    public float Direction;
    public float Actual_Camera_distance;
    public float limitCameraZoom;

    private void Start()
    {
        groundedCharacter = GameObject.Find("player_controller").GetComponent<GroundedCharacterController>();
        //Vector3 cameraPos = Vector3.Lerp(cam.transform.position, controlledcapsulecollider.transform.position, 3 * Time.deltaTime);
        //cameraPos.z = -10;
        Actual_Camera_distance = camdis + groundedCharacter.Actual_velocity.x * Direction;
    }
    void Update()
    {
        Direction = Sprites.transform.localScale.x;
        PlayerVelocity = groundedCharacter.Actual_velocity.x + groundedCharacter.Actual_velocity.y;

        CinemachineComponentBase componentBase = virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        if (componentBase is CinemachineFramingTransposer)
        {
            Actual_Camera_distance = camdis + groundedCharacter.Actual_velocity.x * Direction;
            if (Actual_Camera_distance < limitCameraZoom && Actual_Camera_distance > camdis)
            {
                (componentBase as CinemachineFramingTransposer).m_CameraDistance = Actual_Camera_distance;
            }
        }
        Cursor.visible = !hide_mouse;
        Application.targetFrameRate = Limit_framerate;//limite FPS
    }
}

