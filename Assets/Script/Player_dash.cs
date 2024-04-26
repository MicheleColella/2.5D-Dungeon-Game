using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_dash : MonoBehaviour
{
    public GameObject Sprite;
    public GameObject Dasheffect;
    private bool canDash = true;
    public bool isDashing;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;
    float Direction;
    [SerializeField] private Rigidbody rb;

     //Centro
    RaycastHit hit;
    [Header("Centro")]
    public LayerMask layerMask;
    public float distance;

    RaycastHit hit2;
    public Vector3 raycenterpos2;
    public float distancecenter2;

    RaycastHit hit3;
    public Vector3 raycenterpos3;
    public float distancecenter3;

    
    RaycastHit hithead;
    [Header("Testa")]
    public Vector3 rayheadpos;
    public float distancehead;

    RaycastHit hithead2;
    public Vector3 rayheadpos2;
    public float distancehead2;

    RaycastHit hithead3;
    public Vector3 rayheadpos3;
    public float distancehead3;

    RaycastHit hitfoot;
    [Header("Piedi")]
    public Vector3 rayfootpos;
    public float distancefoot;

    RaycastHit hitfoot2;
    public Vector3 rayfootpos2;
    public float distancefoot2;

    RaycastHit hitfoot3;
    public Vector3 rayfootpos3;
    public float distancefoot3;

    private float actual_posy;

    void Update()
    {
        Direction = Sprite.transform.localScale.x;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right * Direction), out hit, distance, layerMask))
        {
            //Debug.Log(hit.collider.tag);
            StopDash();
            canDash = false;
        }
        else
        {
            canDash = true;
        }

        if (Physics.Raycast(transform.position + raycenterpos2, transform.TransformDirection(Vector3.right * Direction), out hit2, distancecenter2, layerMask))
        {
            StopDash();
            canDash = false;
        }
        else
        {
            canDash = true;
        }

        if (Physics.Raycast(transform.position + raycenterpos3, transform.TransformDirection(Vector3.right * Direction), out hit3, distancecenter3, layerMask))
        {
            StopDash();
            canDash = false;
        }
        else
        {
            canDash = true;
        }

        //Testa
        if (Physics.Raycast(transform.position + rayheadpos, transform.TransformDirection(Vector3.right * Direction), out hithead, distancehead, layerMask))
        {
            StopDash();
            canDash = false;
        }
        else
        {
            canDash = true;
        }

        if (Physics.Raycast(transform.position + rayheadpos2, transform.TransformDirection(Vector3.right * Direction), out hithead2, distancehead2, layerMask))
        {
            StopDash();
            canDash = false;
        }
        else
        {
            canDash = true;
        }

        if (Physics.Raycast(transform.position + rayheadpos3, transform.TransformDirection(Vector3.right * Direction), out hithead3, distancehead3, layerMask))
        {
            StopDash();
            canDash = false;
        }
        else
        {
            canDash = true;
        }

        //Piedi
        if (Physics.Raycast(transform.position + rayfootpos, transform.TransformDirection(Vector3.right * Direction), out hitfoot, distancefoot, layerMask))
        {
            StopDash();
            canDash = false;
        }
        else
        {
            canDash = true;
        }

        if (Physics.Raycast(transform.position + rayfootpos2, transform.TransformDirection(Vector3.right * Direction), out hitfoot2, distancefoot2, layerMask))
        {
            StopDash();
            canDash = false;
        }
        else
        {
            canDash = true;
        }

        if (Physics.Raycast(transform.position + rayfootpos3, transform.TransformDirection(Vector3.right * Direction), out hitfoot3, distancefoot3, layerMask))
        {
            StopDash();
            canDash = false;
        }
        else
        {
            canDash = true;
        }

        if (isDashing)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            Dasheffect.SetActive(isDashing);
            transform.position = new Vector3(transform.position.x, actual_posy, transform.position.z);
            return;
        }

        StopDash();
    }

    private void StopDash()
    {
        Dasheffect.SetActive(false);
        rb.velocity = Vector2.zero;
    }

    private IEnumerator Dash()
    {
        actual_posy = transform.position.y;
        canDash = false;
        isDashing = true;
        rb.velocity = Vector2.right * Direction * dashingPower;
        //Debug.Log(rb.velocity);
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void OnDrawGizmos()
    {
        //Centro
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.right * Direction * distance);
        Gizmos.DrawRay(transform.position + raycenterpos2, Vector3.right * Direction * distancecenter2);
        Gizmos.DrawRay(transform.position + raycenterpos3, Vector3.right * Direction * distancecenter3);
        //Testa
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position + rayheadpos, Vector3.right * Direction * distancehead);
        Gizmos.DrawRay(transform.position + rayheadpos2, Vector3.right * Direction * distancehead2);
        Gizmos.DrawRay(transform.position + rayheadpos3, Vector3.right * Direction * distancehead3);
        //Piedi
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position + rayfootpos, Vector3.right * Direction * distancefoot);
        Gizmos.DrawRay(transform.position + rayfootpos2, Vector3.right * Direction * distancefoot2);
        Gizmos.DrawRay(transform.position + rayfootpos3, Vector3.right * Direction * distancefoot3);
    }

}
