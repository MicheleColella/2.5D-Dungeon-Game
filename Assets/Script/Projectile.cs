using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    /*
    public GameObject impactEffect;
    public float radius = 3;
    public int damageAmount = 15;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject impact = Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(impact, 2);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider nearbyObject in colliders)
        {
            if(nearbyObject.tag == "Player")
            {
                PlayerManager.TakeDamage(damageAmount);
            }
        }
        Destroy(gameObject);
    }*/

    //public int damageAmount = 20;
    public float speed;
    public int damageAmount_max = 20;
    public int damageAmount_min = 10;

    public float radius = 3;
    float scalingFramesLeft = 0.0f;

    public Rigidbody rb;
    public GameObject impactEffect;

    public PlayerManager playerManager;
    public GameObject player_life;

    public GameObject Timemanager;
    public TimeStop timeStop;
    public TimeBody timeBody;

    public Vector3 center;
    public float range;
    public bool right;
    public LayerMask EnemyMask;
    private Vector3 actualPosition;

    public Vector3 center2;
    public float range2;
    public bool left;

    private int Direction;

    private float actualBulletpos;
    private float actualPlayerpos;

    public Vector3 Impactpos;
    public Quaternion Impactrot;

    private void Start()
    {
        Timemanager = GameObject.Find("TimeManager");
        timeStop = Timemanager.GetComponent<TimeStop>();
        timeBody = Timemanager.GetComponent<TimeBody>();

        player_life = GameObject.Find("player_controller");
        playerManager = player_life.GetComponent<PlayerManager>();

        actualPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        right = Physics.CheckSphere(actualPosition + center, range, EnemyMask);
        //Debug.Log(right);
        left = Physics.CheckSphere(actualPosition + center2, range2, EnemyMask);
        //Debug.Log(left);

        actualBulletpos = transform.position.x;
        actualPlayerpos = player_life.transform.position.x;
        if (!right && left)
        {
            Direction = 1;
        }
        else if(right && !left)
        {
            Direction = -1;
        }
        else 
        {
            if (actualBulletpos > actualPlayerpos)
            {
                Direction = -1;
            }
            else
            {
                Direction = 1;
            }
        }
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

        /*
        if (timeStop.stato || timeBody.stato)
        {
            rb.velocity = transform.right * 0;
        }
        */

        if (timeStop.stato)
        {
            rb.velocity = transform.right * Direction * 0;
        }
        else if (timeBody.stato)
        {
            if ((transform.position.x > actualBulletpos && Direction > 0) || (transform.position.x < actualBulletpos && Direction < 0))
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
        yield return new WaitForSeconds(5);
        scalingFramesLeft = 15;
    }

    void OnTriggerEnter(Collider hitInfo)
    {
        var rig = hitInfo.GetComponent<Rigidbody>();
        //Debug.Log(rig.gameObject.tag);
        if (rig != null && rig.gameObject.tag == "Player")
        {
            //Debug.Log("Colpito");
            int damageAmount = Random.Range(damageAmount_min, damageAmount_max);
            playerManager.TakeDamage(damageAmount);
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
        //Destroy(impactEffect, 2);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(actualPosition + center, range);
        Gizmos.DrawWireSphere(actualPosition + center2, range2);
    }
}
