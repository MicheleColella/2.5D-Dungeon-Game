using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet2 : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject impactEffect;
    public float speed;
    public int damage_start = 10;
    public int damage_end = 20;
    public float forza_colpo_proiettile = 10f;
    public bool follow;
    private GameObject Sprites;
    private Transform Enemy_pos;
    int cont = 0;
    //int cont2 = 0;
    int cont3 = 0;
    private Vector3 Enemy_position;
    private Vector3 actual_position;
    public GameObject timemanager;
    public TimeStop timeStop;
    public TimeBody timeBody;
    Transform playerTransform;
    GameObject firepoint;
    Transform firepoint_pos;
    public float actualPlayerpos;
    public float Direction;
    public Vector3 velocity_bullet;

    public float checkRadius;
    public LayerMask checkLayers;

    public GameObject WallimpactEffect;
    public Vector3 WallImpactpos;
    public Quaternion WallImpactrot;

    public Vector3 Impactpos;
    public Quaternion Impactrot;

    int scalingFramesLeft = 0;
    bool stateactive;
    Vector3 distanceToWalkPoint;

    public bool ShakeImpact;
    public float IntencityShake;
    public bool follow_Player;

    void Start()
    {
        Sprites = GameObject.Find("Sprites");
        firepoint = GameObject.Find("FirePoint");

        timemanager = GameObject.Find("TimeManager");
        timeStop = timemanager.GetComponent<TimeStop>();
        timeBody = timemanager.GetComponent<TimeBody>();

        Sprites = GameObject.Find("Sprites");
        //Debug.Log(Sprites.transform.localScale.x);
        playerTransform = GameObject.FindGameObjectWithTag("sprite_player").transform;
        firepoint_pos = firepoint.GetComponent<Transform>();
        actualPlayerpos = firepoint_pos.position.x;
        Direction = Sprites.transform.localScale.x;
        rb.velocity = transform.right * Sprites.transform.localScale.x * (speed / 2);
        Destroy(gameObject, 7.0f);
    }

    private void Update()
    {
        //-------------------------Rimpicciolimento
        StartCoroutine(destroycool());
        if (timeStop.stato || timeBody.stato)
        {
            StartCoroutine(inTime());
        }

        if(stateactive)
        {
            scalingFramesLeft = 15;
        }

        if (scalingFramesLeft > 0)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.0000f, 0.0000f, 0.0000f), Time.deltaTime * 30);

            for(int i = 0; i<transform.childCount; i++)
            {
                Transform child = this.gameObject.transform.GetChild(i);
                child.transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.0000f, 0.0000f, 0.0000f), Time.deltaTime * 30);
            }
            
            scalingFramesLeft--;
        }
        //--------------------------

        StartCoroutine(follow_cooldown());

        Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, checkLayers);
        Array.Sort(colliders, new DistanceComparer(transform));

        foreach (Collider item in colliders)
        {
            if (cont3 == 0)
            {
                //Debug.Log(item.name);
                Enemy_position = item.transform.position;
                cont3 += 1;
            }
        }

        if (follow && cont == 0)
        {
            if (follow_Player)
            {
                Enemy_position = new Vector3(playerTransform.position.x, playerTransform.position.y + 1.6f, playerTransform.position.z);
            }

            if (distanceToWalkPoint.x < -1.0f || distanceToWalkPoint.x > 1.0f)
            {
                distanceToWalkPoint = transform.position - Enemy_position;
            }
            
            
            damage_start = 15;
            damage_end = 25;

            cont += 1;
            if (Enemy_position != new Vector3(0.0f, 0.0f, 0.0f))
            {
                rb.velocity = (Enemy_position - transform.position).normalized * speed;

                Vector3 distanceToWalkPoint = transform.position - Enemy_position;
                //Debug.Log(distanceToWalkPoint);

                Vector3 relativePos = Enemy_position - transform.position;
                transform.LookAt(Enemy_position);
                float rotation = transform.rotation.eulerAngles.x;

                if (distanceToWalkPoint.x > 0)
                {
                    transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0.0f, 0.0f, -rotation);
                }
            }
            else
            {
                rb.velocity = transform.right * Direction * speed;
            }
            
        }
        else if (timeStop.stato || timeBody.stato)
        {
            rb.velocity = transform.right * Direction * 0;
        }
    }
    IEnumerator inTime()
    {
        yield return new WaitForSeconds(5);
        stateactive = true;
    }

    IEnumerator destroycool()
    {
        yield return new WaitForSeconds(6.9f);
        scalingFramesLeft = 15;
    }

    IEnumerator follow_cooldown()
    {
        yield return new WaitForSeconds(0.5f);
        follow = true;
    }


    void OnTriggerEnter(Collider hitInfo)
    {
        GameObject Sprites = GameObject.Find("Sprites");
        Enemy enemy = hitInfo.GetComponent<Enemy>();

        if (enemy != null)
        {
            int damage = UnityEngine.Random.Range(damage_start, damage_end);
            //Debug.Log(damage);
            enemy.TakeDamage(damage);
        }
        var rig = hitInfo.GetComponent<Rigidbody>();
        //Debug.Log(rig);
        
        if (rig != null && rig.gameObject.tag != "Enemy")
        {
            if (distanceToWalkPoint.x < 0 && follow)
            {
                Direction = -Direction;
            }
            rig.AddForce(transform.right * forza_colpo_proiettile * Direction, ForceMode.VelocityChange);
        }

        if (hitInfo != null && hitInfo.tag == "Interior_Map")
        {
            if (Direction > 0)
            {
                Instantiate(WallimpactEffect, transform.position + WallImpactpos, WallImpactrot);
            }
            else
            {
                Vector3 actualwallpos = new Vector3(-WallImpactpos.x, WallImpactpos.y, WallImpactpos.z);
                Quaternion rot = Quaternion.Euler(WallImpactrot.x, 90.0f, WallImpactrot.z);
                Instantiate(WallimpactEffect, transform.position + actualwallpos, rot);
            }
        }

        if (Direction > 0)
        {
            Instantiate(impactEffect, transform.position + Impactpos, Quaternion.identity);
        }
        else
        {
            Vector3 actualimpactpos = new Vector3(-Impactpos.x, Impactpos.y, Impactpos.z);
            Quaternion rotimpact = Quaternion.Euler(Impactrot.x, Impactrot.y, Impactrot.z);
            Instantiate(impactEffect, transform.position + actualimpactpos, rotimpact);
        }

        if (ShakeImpact)
        {
            CameraShake1.Instance.ShakeCamera(IntencityShake, 0.1f, 0f);
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
