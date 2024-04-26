using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet3 : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public float speedincreased;
    public int damage_start = 10;
    public int damage_end = 20;
    public float Direction;
    public GameObject impactEffect;
    public GameObject player;
    public AbilityManager abilityManager;
    public GameObject timemanager;
    public TimeStop timeStop;
    public TimeBody timeBody;
    GameObject Sprites;
    float scalingFramesLeft = 0.0f;

    public float forza_colpo_proiettile = 10f;

    public GameObject WallimpactEffect;
    public Vector3 WallImpactpos;
    public Quaternion WallImpactrot;

    public Vector3 Impactpos;
    public Quaternion Impactrot;

    public bool ShakeImpact;
    public float IntencityShake;
    void Start()
    {
        player = GameObject.Find("player_controller");
        abilityManager = player.GetComponent<AbilityManager>();

        timemanager = GameObject.Find("TimeManager");
        timeStop = timemanager.GetComponent<TimeStop>();
        timeBody = timemanager.GetComponent<TimeBody>();

        Sprites = GameObject.Find("Sprites");
        //Debug.Log(Sprites.transform.localScale.x);
        Transform playerTransform = GameObject.FindGameObjectWithTag("sprite_player").transform;
        Direction = Sprites.transform.localScale.x;
        rb.velocity = transform.right * Direction * speed;
        Destroy(gameObject, 1.0f);
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

        if (abilityManager.stato)
        {
            damage_start = 20;
            damage_end = 30;
            rb.velocity = transform.right * Direction * speed * speedincreased;
        }
        else if(timeStop.stato || timeBody.stato)
        {
            damage_start = 20;
            damage_end = 30;
        }
        else
        {
            rb.velocity = transform.right * Direction * speed;
        }
    }

    IEnumerator destroycool()
    {
        yield return new WaitForSeconds(0.9f);
        scalingFramesLeft = 15;
    }

    void OnTriggerEnter(Collider hitInfo)
    {
        GameObject Sprites = GameObject.Find("Sprites");
        Enemy enemy = hitInfo.GetComponent<Enemy>();

        if (enemy != null)
        {
            int damage = Random.Range(damage_start, damage_end);
            //Debug.Log(damage);
            enemy.TakeDamage(damage);
        }
        var rig = hitInfo.GetComponent<Rigidbody>();
        //Debug.Log(rig);
        
        if (rig != null && rig.gameObject.tag != "Enemy")
        {
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
}
