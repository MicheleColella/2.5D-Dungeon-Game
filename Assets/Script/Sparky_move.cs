using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparky_move : MonoBehaviour
{
    /*
    public Animator anim;
    public float speed;
    public float actualspeed;
    private GameObject player;
    public bool Chased;
    public Vector3 walkPoint;
    public bool playerInEnemy;
    public LayerMask whatIsPlayer;
    public float distanzaZ = -1.0f;
    public PlayerManager playerManager;
    public Field_of_view field_Of_View;
    float distanceRemainder;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();
    }

    void Update()
    {
        var maxDistance = 5f * Time.deltaTime;
        var actualDistance = Vector3.Distance(walkPoint, transform.position);
        distanceRemainder = maxDistance - actualDistance;

        //anim.SetFloat("Idle_speed", 1.0f);

        Chased = field_Of_View.canSeePlayer;

        if (player == null)
        {
            return;
        }

        //Follow Animation
        /*
        if (Chased && !melee && !playerInEnemy)
        {
            anim.Play("Enemy_2_Idle");
        }

        //Idle Animation
        if (!Chased && !melee && !playerInEnemy)
        {
            Flip();
            anim.Play("Enemy_2_Idle");
        }
        
        //Follow function
        if (Chased)
        {
            ChasedFlip();
            actualspeed = speed;
            Chase();
        }
    }


    //------------

    IEnumerator ChaseCool()
    {
        Chased = true;
        yield return new WaitForSeconds(5.0f);
        Chased = false;
    }

    private void Chase()
    {
        Vector3 distance = transform.position - player.transform.position;

        //Debug.Log(distance.magnitude);

        if (distance.magnitude > 3.0f)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, actualspeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, distanzaZ);
            walkPoint = transform.position;
            //Debug.Log(transform.position);
        }
    }


    private void Flip()
    {
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        //Debug.Log("distanceToWalkPoint = " + distanceToWalkPoint);

        if (distanceToWalkPoint.x > 0.0f)
        {
            //Debug.Log("Sinistra");
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            //Debug.Log("Destra");
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void ChasedFlip()
    {
        Vector3 distanceToWalkPoint = transform.position - player.transform.position;
        //Debug.Log("distanceToWalkPoint = " + distanceToWalkPoint);

        if (distanceToWalkPoint.x > 0.0f)
        {
            //Debug.Log("Sinistra");
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            //Debug.Log("Destra");
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    */


    public Transform player;
    public float moveSpeed = 5f;
    private float actualspeed;
    private Rigidbody rb;
    private Vector2 movement;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        actualspeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        direction = player.position - transform.position;
        //Debug.Log(direction.magnitude);
        //------------ da aggiustare
        transform.LookAt(-direction);
        float rotation = transform.rotation.eulerAngles.x;

        if (direction.x > 0)
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, -rotation);
        }
        //------------
        direction.Normalize();
        
        movement = direction;
    }
    private void FixedUpdate()
    {
        direction = player.position - transform.position;
        //Debug.Log(direction.magnitude);
        if (direction.magnitude < 4.0f)
        {
            actualspeed = moveSpeed / 2;
            
        }
        else if (direction.magnitude < 3.0f)
        {
            actualspeed = (moveSpeed / 2) -5;
        }
        else
        {
            actualspeed = moveSpeed;
        }

        if (direction.magnitude > 2.0f)
        {
            moveCharacter(movement);
        }

        //Debug.Log(actualspeed);
    }
    void moveCharacter(Vector2 direction)
    {
        //Debug.Log("E");
        rb.MovePosition((Vector2)transform.position + (direction * actualspeed * Time.deltaTime));
    }
}
