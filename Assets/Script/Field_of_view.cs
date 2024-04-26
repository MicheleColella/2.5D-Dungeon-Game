using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field_of_view : MonoBehaviour
{
    public GameObject player;
    public CapsuleCollider playercapsule;

    public float radius;
    [Range(0, 360)]
    public float angle;
    private float actualradius;

    public float radiusAttack;
    [Range(0, 360)]
    public float angleAttack;
    private float actualAttackradius;

    public float radiusmeleeAttack;
    [Range(0, 360)]
    public float anglemeleeAttack;
    private float actualmeleeAttackradius;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;
    public bool canAttckPlayer;
    public bool canmeleeAttckPlayer;

    public MeleeEnemy meleeEnemy;
    private bool chased;

    int cont = 0;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    private void Awake()
    {
        player = GameObject.Find("player_controller");
        playercapsule = player.transform.GetComponent<CapsuleCollider>();
    }
    private void Start()
    {
        actualradius = radius;
        actualAttackradius = radiusAttack;
        actualmeleeAttackradius = radiusmeleeAttack;
        StartCoroutine(FOVRoutine());
    }
    private void Update()
    {
        if (meleeEnemy != null)
        {
            chased = meleeEnemy.playerInSightRange;
        }

        var capsule_height = playercapsule.height * transform.localScale.y;
        if (capsule_height == 1.6f && chased == false)
        {
            if(cont == 0)
            {
                radius /= 2;
                radiusAttack /= 2;
                radiusmeleeAttack /= 2;
            }
            cont++;
        }
        else
        {
            cont = 0;
            radius = actualradius;
            radiusAttack = actualAttackradius;
            radiusmeleeAttack = actualmeleeAttackradius;
        }
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
            FieldOfViewAttack();
            FieldOfViewmeleeAttack();
        }
    }

    private void FieldOfViewCheck()
    {
        visibleTargets.Clear();
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask, QueryTriggerInteraction.Collide);
        //Debug.Log(rangeChecks);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    visibleTargets.Add(target);
                    canSeePlayer = true;
                }
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }

    private void FieldOfViewAttack()
    {
        visibleTargets.Clear();
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radiusAttack, targetMask, QueryTriggerInteraction.Collide);
        //Debug.Log(rangeChecks);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angleAttack / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    visibleTargets.Add(target);
                    canAttckPlayer = true;
                }
                else
                    canAttckPlayer = false;
            }
            else
                canAttckPlayer = false;
        }
        else if (canAttckPlayer)
            canAttckPlayer = false;
    }

    private void FieldOfViewmeleeAttack()
    {
        visibleTargets.Clear();
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radiusmeleeAttack, targetMask, QueryTriggerInteraction.Collide);
        //Debug.Log(rangeChecks);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < anglemeleeAttack / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    visibleTargets.Add(target);
                    canmeleeAttckPlayer = true;
                }
                else
                    canmeleeAttckPlayer = false;
            }
            else
                canmeleeAttckPlayer = false;
        }
        else if (canmeleeAttckPlayer)
            canmeleeAttckPlayer = false;
    }

    /*
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public bool canSeePlayer;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    void Start()
    {
        StartCoroutine("FindTargetsWithDelay", .2f);
    }


    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask, QueryTriggerInteraction.Collide);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    canSeePlayer = true;
                    visibleTargets.Add(target);
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
    }


    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }*/
}
