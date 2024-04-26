using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{
    public Enemy_Shield enemy_Shield;
    public SpriteRenderer Sprite;
    public TimeStop TimeStop;
    public TimeBody timeBody;
    public Sprite_camera_rotation sprite_camera_rotation;
    public Enemy enemy;
    public Animator anim;
    public float velocity;
    public Vector3 firePoint;

    public bool attacco_dritto = true;
    public bool Agente_fermo = false;

    public bool damage = false;
    public NavMeshAgent agent;
    public Transform player;

    public Enemy enemy_life;

    public LayerMask whatIsGround, whatIsPlayer;

    public PlayerManager playerManager;
    public GameObject player_life;

    //Patroling
    [Header("Patroling")]
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    [Header("Attacking")]
    public float timeBetweenAttacks_min = 0.5f;
    public float timeBetweenAttacks_max = 2.5f;
    private float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    [Header("States")]
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange, playerInmeleeAttackRange, playerInEnemy;

    //CheckBox
    [Header("CheckBox")]
    public Vector3 center;
    public Vector3 halfExtents;
    public Vector3 halfExtents2;
    public Quaternion orientation = Quaternion.identity;
    public QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;

    float nextmove = 0f;
    public float cooldown = 4.0f;

    public bool Chase = false;

    public Field_of_view field_Of_View;

    float nextmeleeAttackTime = 0f;
    public float cooldownTimemelee_sword = 1.0f;

    int cont = 0;
    int cont_salto = 0;
    int cont_sprite = 0;

    [Header("Permission")]
    public bool CandistanceAttack;
    public bool CanmeleeAttack;
    public bool CanShield;
    public bool DontSlowSpeed;
    public bool CanAlwaysSeethePlayer;

    [Header("Boss")]
    public bool HaveIntro;
    public float IntroTime;
    public float StartDelay;
    public bool IntroActive;

    float actualagentspeed;

    Sprite currentSprite;

    [Header("Blaster Audio")]
    [SerializeField] private AudioSource BlasterSource;
    public AudioClip[] BlasterSound;
    
    [Header("Shield Audio")]
    [SerializeField] private AudioSource ShieldSource;
    public AudioClip[] ShieldSound;
    bool shield_audio_can;

    private void Start()
    {
        enemy_Shield = gameObject.GetComponent<Enemy_Shield>();
        actualagentspeed = agent.speed;
        player_life = GameObject.Find("player_controller");
        playerManager = player_life.GetComponent<PlayerManager>();

        TimeStop = GameObject.Find("TimeManager").GetComponent<TimeStop>();
        timeBody = GameObject.Find("TimeManager").GetComponent<TimeBody>();
        if (HaveIntro)
        {
            StartCoroutine(StartIntro());
        }

        BlasterSource.pitch = 1.0f;
        shield_audio_can = true;
    }

    IEnumerator StartIntro()
    {
        IntroActive = true;
        agent.isStopped = IntroActive;
        yield return new WaitForSeconds(StartDelay);
        anim.Play("Intro");
        yield return new WaitForSeconds(IntroTime);
        IntroActive = false;
        agent.isStopped = IntroActive;
    }

    private void Awake()
    {
        player = GameObject.Find("player_controller").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!IntroActive)
        {
            velocity = agent.velocity.magnitude / actualagentspeed;
            Vector3 pos = player.localPosition;
            if (attacco_dritto)
            {
                pos.y = transform.localPosition.y;
            }
            //Debug.Log(timeBody.stato);

            //Check for sight and attack range
            /*
            Debug.Log("transform.position = " + transform.position);
            Debug.Log("halfExtents = " + halfExtents);
            Debug.Log("orientation = " + orientation);
            Debug.Log("whatIsPlayer = " + whatIsPlayer);
            Debug.Log("queryTriggerInteraction = " + queryTriggerInteraction);*/

            playerInEnemy = Physics.CheckBox(transform.position, halfExtents, orientation, whatIsPlayer, QueryTriggerInteraction.Collide);
            //playerInAttackRange = Physics.CheckBox(transform.position, halfExtents2, orientation, whatIsPlayer, QueryTriggerInteraction.Collide);
            playerInAttackRange = field_Of_View.canAttckPlayer;
            playerInSightRange = field_Of_View.canSeePlayer;
            playerInmeleeAttackRange = field_Of_View.canmeleeAttckPlayer;
            //Debug.Log(playerInSightRange);

            //Debug.Log(velocity);
            //Debug.Log("velocity = " + velocity);
            //Debug.Log("playerInSightRange = " + playerInSightRange);
            //Debug.Log("playerInAttackRange = " + playerInAttackRange);
            //Debug.Log("playerInmeleeAttackRange = " + playerInmeleeAttackRange);
            //Debug.Log("playerInEnemy = " + playerInEnemy);
            if (CanAlwaysSeethePlayer)
            {
                playerInSightRange = true;
            }

            if (TimeStop.stato)
            {
                agent.isStopped = true;
                if (cont_sprite == 0)
                {
                    //Debug.Log(cont_sprite);
                    currentSprite = Sprite.sprite;
                }
                //Debug.Log(currentSprite);
                cont_sprite += 1;
                Sprite.sprite = currentSprite;
                anim.SetFloat("MoveSpeed", 0);
            }
            else
            {
                if (!timeBody.stato)
                {
                    cont_sprite = 0;
                    anim.SetFloat("MoveSpeed", 1);
                    agent.isStopped = false;

                    if (enemy.death)
                    {
                        playerInEnemy = false;
                        playerInAttackRange = false;
                        playerInSightRange = false;
                        playerInmeleeAttackRange = false;
                        agent.isStopped = true;
                        anim.Play("Death");
                    }

                    if (playerManager.isGameOver)
                    {
                        playerInEnemy = false;
                        playerInAttackRange = false;
                        playerInSightRange = false;
                        playerInmeleeAttackRange = false;
                    }

                    if (sprite_camera_rotation.Zrot != 0)
                    {
                        playerInAttackRange = false;
                    }

                    if (cont > 0 && !playerInSightRange)
                    {
                        StartCoroutine(wait_patrol());
                    }

                    if (playerInSightRange)
                    {
                        cont += 1;
                    }

                    string linkType = agent.currentOffMeshLinkData.linkType.ToString();

                    if (!enemy.death)
                    {
                        if (linkType == "LinkTypeDropDown")
                        {
                            if (cont_salto == 0)
                            {
                                //Debug.Log("Jump");
                                anim.Play("Jump");
                            }
                            cont_salto += 1;
                        }
                        else if (linkType != "LinkTypeDropDown")
                        {
                            cont_salto = 0;
                        }

                        if (CanShield && enemy_Shield.Shield_starting)
                        {
                            if (shield_audio_can)
                            {
                                shield_audio_can = false;
                                ShieldSource.PlayOneShot(ShieldSound[0], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
                            }
                            
                            agent.isStopped = true;
                            anim.Play("Shield");
                        }
                        else
                        {
                            shield_audio_can = true;
                            agent.isStopped = false;
                            if (velocity >= 0.3f && !playerInSightRange && !playerInAttackRange && !playerInmeleeAttackRange && !playerInEnemy && linkType != "LinkTypeDropDown")
                            {
                                //Debug.Log("Walk");
                                anim.Play("Walk");
                            }
                            else if (velocity >= 0.3f && playerInSightRange && !playerInAttackRange && !playerInmeleeAttackRange && !playerInEnemy && linkType != "LinkTypeDropDown")
                            {
                                //Debug.Log("Run");
                                anim.Play("Run");
                            }
                            else if (velocity >= 0.3f && playerInSightRange && playerInAttackRange && !playerInmeleeAttackRange && !playerInEnemy)
                            {
                                //Debug.Log("Shoot_move");
                                anim.Play("Run");
                            }
                            else if (velocity <= 0.3f && playerInSightRange && playerInAttackRange && !playerInmeleeAttackRange && !playerInEnemy)
                            {
                                //Debug.Log("Shoot");
                                anim.Play("Shoot");
                            }
                            else if (velocity <= 0.3f && !playerInSightRange && !playerInAttackRange && !playerInmeleeAttackRange && !playerInEnemy)
                            {
                                //Debug.Log("Idle");
                                anim.Play("Idle");
                            }
                            else if (velocity <= 0.3f && playerInSightRange && !playerInAttackRange && !playerInmeleeAttackRange && !playerInEnemy)
                            {
                                //Debug.Log("Idle_chased");
                                anim.Play("Idle_chased");
                            }
                        }

                        if (enemy_life.TakeDamager && !playerInAttackRange && !playerInmeleeAttackRange && !playerInEnemy)
                        {
                            ChasePlayer();
                        }

                        if (damage = enemy_life.TakeDamager)
                        {
                            StartCoroutine(damage_cool());
                        }
                        else
                        {
                            if ((!playerInSightRange && !playerInAttackRange && agent.isStopped == false && playerInmeleeAttackRange == false && !playerInEnemy) || Chase == false)
                            {
                                Patroling();
                            }
                            if ((playerInSightRange && !playerInAttackRange && agent.isStopped == false && playerInmeleeAttackRange == false && !playerInEnemy) || Chase == true)
                            {
                                ChasePlayer();
                            }

                            if (CandistanceAttack)
                            {
                                if (playerInAttackRange && playerInSightRange && agent.isStopped == false && playerInmeleeAttackRange == false && !playerInEnemy)
                                {
                                    AttackPlayer(pos);
                                }
                            }

                            if (CanmeleeAttack)
                            {
                                if ((playerInmeleeAttackRange && playerInAttackRange && playerInSightRange && agent.isStopped == false) || (playerInEnemy && agent.isStopped == false))
                                {
                                    agent.isStopped = true;
                                    AttackmeleePlayer();
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    IEnumerator wait_patrol()
    {
        playerInSightRange = true;
        var rand = Random.Range(3, 5);
        yield return new WaitForSeconds(rand);
        playerInSightRange = false;
        cont = 0;
    }

    public void AttackmeleePlayer()
    {
        
        if (Time.time > nextmeleeAttackTime)
        {
            //Debug.Log("Spada");
            anim.Play("Melee");
            nextmeleeAttackTime = Time.time + cooldownTimemelee_sword;
        }
    }

    IEnumerator damage_cool()
    {
        yield return new WaitForSeconds(2.0f);
        //Debug.Log(damage);
        return_damage();
    }

    public bool return_damage()
    {
        return false;
    }

    private void Patroling()
    {
        if (!DontSlowSpeed)
        {
            agent.speed = actualagentspeed / 2;
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
            nextmove = Time.time + cooldown + wait_cool;
        }

        if (walkPointSet)
        {
            //Debug.Log("Stato 2");
            //Debug.Log(walkPoint);
            agent.SetDestination(walkPoint);
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
        //float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.speed = actualagentspeed;
        //Debug.Log("Stato 4");
        agent.SetDestination(player.position);
    }

    private void AttackPlayer(Vector3 pos)
    {
        agent.speed = actualagentspeed;
        //Debug.Log("Stato 5");
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        //Debug.Log("ATTACCO");
        //Debug.Log(pos);
        transform.LookAt(pos);

        if (!alreadyAttacked)
        {
            //float posx = 0.0f;
            //float posy = 0.6f;
            ///Attack code here
            //Debug.Log(transform.rotation.eulerAngles.y);
            if (transform.rotation.eulerAngles.y <= 100 && transform.rotation.eulerAngles.y >= 80)
            {
                //posx = 1.6f;
                //Debug.Log("Destra");
                if (firePoint.x < 0)
                {
                    firePoint.x = -firePoint.x * 1;
                }
            }
            else if (transform.rotation.eulerAngles.y <= 280 && transform.rotation.eulerAngles.y >= 260)
            {
                //Debug.Log("Sinistra");
                if (firePoint.x > 0)
                {
                    firePoint.x = -firePoint.x * 1;
                }

                //posx = -1.6f;
            }
            /*
            Rigidbody rb = Instantiate(projectile, transform.position + firePoint, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            */
            Instantiate(projectile, transform.position + firePoint, Quaternion.identity);
            ///End of attack code

            alreadyAttacked = true;
            float timeBetweenAttacks = Random.Range(timeBetweenAttacks_min, timeBetweenAttacks_max);
            //Debug.Log(timeBetweenAttacks);
            BlasterSource.PlayOneShot(BlasterSound[0], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, halfExtents * 2);
    }
}
