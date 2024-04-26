using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObj : MonoBehaviour
{
    [SerializeField]
    private float forceMagnitude;
    [SerializeField]
    private float forceMagnitude2;

    //Centro
    RaycastHit hit;
    public Transform Sprites;
    public LayerMask layerMask;
    float Direction;
    public float distance;

    RaycastHit hit2;
    public Vector3 raycenterpos2;
    public float distancecenter2;

    RaycastHit hit3;
    public Vector3 raycenterpos3;
    public float distancecenter3;

    //Testa
    RaycastHit hithead;
    public Vector3 rayheadpos;
    public float distancehead;

    RaycastHit hithead2;
    public Vector3 rayheadpos2;
    public float distancehead2;

    RaycastHit hithead3;
    public Vector3 rayheadpos3;
    public float distancehead3;

    //Piedi
    RaycastHit hitfoot;
    public Vector3 rayfootpos;
    public float distancefoot;

    RaycastHit hitfoot2;
    public Vector3 rayfootpos2;
    public float distancefoot2;

    RaycastHit hitfoot3;
    public Vector3 rayfootpos3;
    public float distancefoot3;

    //Sopra Testa
    RaycastHit hituphead;
    public Vector3 rayupheadpos;
    public float distanceuphead;

    RaycastHit hituphead2;
    public Vector3 rayupheadpos2;
    public float distanceuphead2;

    RaycastHit hituphead3;
    public Vector3 rayupheadpos3;
    public float distanceuphead3;


    void Update()
    {
        //Centro
        Direction = Sprites.transform.localScale.x;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right * Direction), out hit, distance, layerMask))
        {
            Rigidbody rigidbody = hit.rigidbody;

            if (rigidbody != null)
            {
                Vector3 forceDirection = hit.transform.position - transform.position;
                forceDirection.y = 0;
                forceDirection.Normalize();

                rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
            }
        }

        if (Physics.Raycast(transform.position + raycenterpos2, transform.TransformDirection(Vector3.right * Direction), out hit2, distancecenter2, layerMask))
        {
            Rigidbody rigidbody = hit2.rigidbody;

            if (rigidbody != null)
            {
                Vector3 forceDirection = hit2.transform.position - transform.position;
                forceDirection.y = 0;
                forceDirection.Normalize();

                rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position + raycenterpos2, ForceMode.Impulse);
            }
        }

        if (Physics.Raycast(transform.position + raycenterpos3, transform.TransformDirection(Vector3.right * Direction), out hit3, distancecenter3, layerMask))
        {
            Rigidbody rigidbody = hit3.rigidbody;

            if (rigidbody != null)
            {
                Vector3 forceDirection = hit3.transform.position - transform.position;
                forceDirection.y = 0;
                forceDirection.Normalize();

                rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position + raycenterpos3, ForceMode.Impulse);
            }
        }

        //Testa
        if (Physics.Raycast(transform.position + rayheadpos, transform.TransformDirection(Vector3.right * Direction), out hithead, distancehead, layerMask))
        {
            Rigidbody rigidbody = hithead.rigidbody;

            if (rigidbody != null)
            {
                Vector3 forceDirection = hithead.transform.position - transform.position;
                forceDirection.y = 0;
                forceDirection.Normalize();

                rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position + rayheadpos, ForceMode.Impulse);
            }
        }

        if (Physics.Raycast(transform.position + rayheadpos2, transform.TransformDirection(Vector3.right * Direction), out hithead2, distancehead2, layerMask))
        {
            Rigidbody rigidbody = hithead2.rigidbody;

            if (rigidbody != null)
            {
                Vector3 forceDirection = hithead2.transform.position - transform.position;
                forceDirection.y = 0;
                forceDirection.Normalize();

                rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position + rayheadpos2, ForceMode.Impulse);
            }
        }

        if (Physics.Raycast(transform.position + rayheadpos3, transform.TransformDirection(Vector3.right * Direction), out hithead3, distancehead3, layerMask))
        {
            Rigidbody rigidbody = hithead3.rigidbody;

            if (rigidbody != null)
            {
                Vector3 forceDirection = hithead3.transform.position - transform.position;
                forceDirection.y = 0;
                forceDirection.Normalize();

                rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position + rayheadpos3, ForceMode.Impulse);
            }
        }

        //Piedi
        if (Physics.Raycast(transform.position + rayfootpos, transform.TransformDirection(Vector3.right * Direction), out hitfoot, distancefoot, layerMask))
        {
            Rigidbody rigidbody = hitfoot.rigidbody;

            if (rigidbody != null)
            {
                Vector3 forceDirection = hitfoot.transform.position - transform.position;
                forceDirection.y = 0;
                forceDirection.Normalize();

                rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position + rayfootpos, ForceMode.Impulse);
            }
        }

        if (Physics.Raycast(transform.position + rayfootpos2, transform.TransformDirection(Vector3.right * Direction), out hitfoot2, distancefoot2, layerMask))
        {
            Rigidbody rigidbody = hitfoot2.rigidbody;

            if (rigidbody != null)
            {
                Vector3 forceDirection = hitfoot2.transform.position - transform.position;
                forceDirection.y = 0;
                forceDirection.Normalize();

                rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position + rayfootpos2, ForceMode.Impulse);
            }
        }

        if (Physics.Raycast(transform.position + rayfootpos3, transform.TransformDirection(Vector3.right * Direction), out hitfoot3, distancefoot3, layerMask))
        {
            Rigidbody rigidbody = hitfoot3.rigidbody;

            if (rigidbody != null)
            {
                Vector3 forceDirection = hitfoot3.transform.position - transform.position;
                forceDirection.y = 0;
                forceDirection.Normalize();

                rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position + rayfootpos3, ForceMode.Impulse);
            }
        }

        //Sopra Testa
        if (Physics.Raycast(transform.position + rayupheadpos, transform.TransformDirection(Vector3.up * Direction), out hituphead, distanceuphead, layerMask))
        {
            Rigidbody rigidbody = hituphead.rigidbody;

            if (rigidbody != null)
            {
                Vector3 forceDirection = hituphead.transform.position - transform.position;
                //forceDirection.y = 0;
                forceDirection.Normalize();

                rigidbody.AddForceAtPosition(forceDirection * forceMagnitude2, transform.position + rayupheadpos, ForceMode.Impulse);
            }
        }

        if (Physics.Raycast(transform.position + rayupheadpos2, transform.TransformDirection(Vector3.up * Direction), out hituphead2, distanceuphead2, layerMask))
        {
            Rigidbody rigidbody = hituphead2.rigidbody;

            if (rigidbody != null)
            {
                Vector3 forceDirection = hituphead2.transform.position - transform.position;
                //forceDirection.y = 0;
                forceDirection.Normalize();

                rigidbody.AddForceAtPosition(forceDirection * forceMagnitude2, transform.position + rayupheadpos2, ForceMode.Impulse);
            }
        }

        if (Physics.Raycast(transform.position + rayupheadpos3, transform.TransformDirection(Vector3.up * Direction), out hituphead3, distanceuphead3, layerMask))
        {
            Rigidbody rigidbody = hituphead3.rigidbody;

            if (rigidbody != null)
            {
                Vector3 forceDirection = hituphead3.transform.position - transform.position;
                //forceDirection.y = 0;
                forceDirection.Normalize();

                rigidbody.AddForceAtPosition(forceDirection * forceMagnitude2, transform.position + rayupheadpos3, ForceMode.Impulse);
            }
        }
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
        //Sopra Testa
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position + rayupheadpos, Vector3.up * Direction * distanceuphead);
        Gizmos.DrawRay(transform.position + rayupheadpos2, Vector3.up * Direction * distanceuphead2);
        Gizmos.DrawRay(transform.position + rayupheadpos3, Vector3.up * Direction * distanceuphead3);
    }
}
