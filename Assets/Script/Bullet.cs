using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage_start = 10;
    public int damage_end = 20;
    public Rigidbody rb;
    public GameObject impactEffect;
    public GameObject WallimpactEffect;
    public GameObject timemanager;
    public TimeStop timeStop;
    public TimeBody timeBody;
    public float Direction;
    public float forza_colpo_proiettile = 10f;
    GameObject Sprites;
    Transform playerTransform;
    GameObject firepoint;
    Transform firepoint_pos;
    public float actualPlayerpos;
    float scalingFramesLeft = 0.0f;

    public Vector3 WallImpactpos;
    public Quaternion WallImpactrot;

    public Vector3 Impactpos;
    public Quaternion Impactrot;
    public bool ShakeImpact;
    public float IntencityShake;

    void Start()
    {
        Sprites = GameObject.Find("Sprites");
        firepoint = GameObject.Find("FirePoint");

        timemanager = GameObject.Find("TimeManager");
        timeStop = timemanager.GetComponent<TimeStop>();
        timeBody = timemanager.GetComponent<TimeBody>();

        //Debug.Log(Sprites.transform.localScale.x);
        playerTransform = GameObject.FindGameObjectWithTag("sprite_player").transform;
        firepoint_pos = firepoint.GetComponent<Transform>();
        actualPlayerpos = firepoint_pos.position.x;
        Direction = Sprites.transform.localScale.x;
        rb.velocity = transform.right * Direction * speed;
        Destroy(gameObject, 6.0f);
    }

    private void Update()
    {
        
        //-------------------------Rimpicciolimento
        StartCoroutine(destroycool());

        if (scalingFramesLeft > 0)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.0000f, 0.0000f, 0.0000f), Time.deltaTime * 30);

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = this.gameObject.transform.GetChild(i);
                child.transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.0000f, 0.0000f, 0.0000f), Time.deltaTime * 30);
            }

            scalingFramesLeft--;
        }
        //--------------------------

        if (timeStop.stato)
        {
            rb.velocity = transform.right * Direction * 0;
        }
        else if (timeBody.stato)
        {
            if((transform.position.x > actualPlayerpos && Direction > 0) || (transform.position.x < actualPlayerpos && Direction < 0))
            {
                rb.velocity = transform.right * -Direction * speed;
            }
            else
            {
                rb.velocity = transform.right * Direction * 0;
            }
        }
        else
        {
            rb.velocity = transform.right * Direction * speed;
        }
    }

    IEnumerator destroycool()
    {
        yield return new WaitForSeconds(5.9f);
        scalingFramesLeft = 15;
    }

    void OnTriggerEnter(Collider hitInfo)
    {
        GameObject Sprites = GameObject.Find("Sprites");
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        
        if(enemy != null)
        {
            int damage = Random.Range(damage_start, damage_end);
            //Debug.Log(damage);
            enemy.TakeDamage(damage);
        }
        var rig = hitInfo.GetComponent<Rigidbody>();
        
        if(rig != null && rig.gameObject.tag != "Enemy")
        {
            rig.AddForce(transform.right * forza_colpo_proiettile * Direction, ForceMode.VelocityChange);
        }

        if (hitInfo != null && hitInfo.tag == "Interior_Map")
        {
            if(Direction > 0)
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
}
