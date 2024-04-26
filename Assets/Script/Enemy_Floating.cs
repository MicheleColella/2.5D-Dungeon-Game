using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Floating : MonoBehaviour
{
    public TimeStop timeStop;
    public TimeBody timeBody;
    public SpriteRenderer Sprite;
    Sprite currentSprite;

    public Animator anim;
    public float speed;
    public float actualspeed;
    public bool walkPointSet;
    private GameObject player;
    public bool Chased;
    public bool melee;
    float nextmeleeAttackTime = 0f;
    float nextmove = 0f;
    public float cooldownTimemelee_sword = 1.0f;
    public float cooldown_move = 1.0f;
    public float walkPointRange = 5.0f;
    public Vector3 walkPoint;
    public Vector3 StartPosition;
    public bool playerInEnemy;
    public Vector3 halfExtents;
    public Quaternion orientation = Quaternion.identity;
    public LayerMask whatIsPlayer;
    public float distanzaZ = -1.0f;

    public PlayerManager playerManager;
    public Enemy enemy;
    public Field_of_view field_Of_View;
    float distanceRemainder;

    int cont_sprite = 0;
    bool returntostart;

    [Header("Melee Audio")]
    [SerializeField] private AudioSource MeleeSource;
    public AudioClip[] MeleeSound;
    bool Can_melee_sound;

    void Start()
    {
        StartPosition = transform.position;
        timeStop = GameObject.Find("TimeManager").GetComponent<TimeStop>();
        timeBody = GameObject.Find("TimeManager").GetComponent<TimeBody>();

        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();

        Can_melee_sound = true;
    }


    void Update()
    {
        var maxDistance = 5f * Time.deltaTime;
        var actualDistance = Vector3.Distance(walkPoint, transform.position);
        distanceRemainder = maxDistance - actualDistance;

        
        anim.SetFloat("Idle_speed", 1.0f);
        playerInEnemy = Physics.CheckBox(transform.position, halfExtents, orientation, whatIsPlayer, QueryTriggerInteraction.Collide);

        Chased = field_Of_View.canSeePlayer;
        melee = field_Of_View.canmeleeAttckPlayer;

        if (timeStop.stato)
        {
            if (cont_sprite == 0)
            {
                //Debug.Log(cont_sprite);
                currentSprite = Sprite.sprite;
            }
            //Debug.Log(currentSprite);
            cont_sprite += 1;
            Sprite.sprite = currentSprite;
            anim.SetFloat("Idle_speed", 0);
        }
        else
        {
            cont_sprite = 0;
            anim.SetFloat("Idle_speed", 1);

            if (playerManager.isGameOver)
            {
                playerInEnemy = false;
                Chased = false;
                melee = false;
            }

            if (player == null)
            {
                return;
            }

            if (enemy.damage_taken)
            {
                StartCoroutine(ChaseCool());
            }

            if (Chased && !melee && !playerInEnemy)
            {
                anim.Play("Enemy_2_Idle");
            }

            if (!Chased && !melee && !playerInEnemy)
            {
                Flip();
                anim.Play("Enemy_2_Idle");
                Patroling();
            }

            if (Chased && !timeBody.stato && !timeStop.stato)
            {
                ChasedFlip();
                actualspeed = speed;
                Chase();
            }
            if (melee && !timeBody.stato && !timeStop.stato)
            {
                actualspeed = speed;
                Melee();
            }
            else if (playerInEnemy && !timeBody.stato && !timeStop.stato)
            {

                actualspeed = speed;
                Melee();
            }
            Can_melee_sound = true;
        }
    }

    //------------
    private void Patroling()
    {
        actualspeed = speed / 2;

        Vector3 distancetostart = new Vector3(transform.position.x - StartPosition.x, transform.position.y - StartPosition.y, transform.position.z - StartPosition.z);
        

        if ((distancetostart.x > 10 || distancetostart.x < -10) || (distancetostart.y > 10 || distancetostart.y < -10))
        {
            Debug.Log(distancetostart.x + " | " + distancetostart.y);
            returntostart = true;
        }
        else
        {
            returntostart = false;
        }

        

        if (Time.time > nextmove)
        {
            if (!walkPointSet)
            {
                //Debug.Log("Stato 1");
                SearchWalkPoint();
                StartCoroutine(cooldown_patrol());
            }
            int wait_cool = Random.Range(1, 4);
            nextmove = Time.time + cooldown_move + wait_cool;
        }

        if (walkPointSet)
        {
            //Debug.Log("Stato 2");
            //Debug.Log(walkPoint);

            if (returntostart)
            {
                walkPoint = StartPosition;
            }

            transform.position = Vector2.MoveTowards(transform.position, walkPoint, actualspeed * Time.deltaTime);
            
            transform.position = new Vector3(transform.position.x, transform.position.y, distanzaZ);
        }


        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            //Debug.Log("Stato 3");
            walkPointSet = false;
        }

    }

    IEnumerator cooldown_patrol()
    {
        int wait_cool = Random.Range(5, 15);
        yield return new WaitForSeconds(wait_cool);
        walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomY = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z);

        
        walkPointSet = true;
        
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

        if(distance.magnitude > 1.0f)
        {
            if (distanceRemainder > 0.0001)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, actualspeed * Time.deltaTime);
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, distanzaZ);
            walkPoint = transform.position;
            //Debug.Log(transform.position);
        }
    }

    private void Melee()
    {
        //Danno giocatore
        if (Time.time > nextmeleeAttackTime)
        {
            //Debug.Log("Spada");
            if(Can_melee_sound)
            {
                Can_melee_sound = false;
                int rand = Random.Range(0, MeleeSound.Length);
                MeleeSource.PlayOneShot(MeleeSound[rand], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
            }
            anim.Play("Enemy_2_Melee");
            nextmeleeAttackTime = Time.time + cooldownTimemelee_sword;
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, halfExtents * 2);
    }
}
