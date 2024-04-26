using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    public CameraLogic cameraLogic;
    public ControlledCapsuleCollider m_ControlledCollider;
    public GroundedCharacterController m_CharacterController;
    public Bullet_manager bullet_Manager;
    public Transform firePoint_obj;
    //public GameObject bulletPrefab;
    public GameObject Player_shoot;
    public GameObject Player_sprites;
    public AbilityManager abilityManager;
    public PlayerManager playerManager;
    [SerializeField] CapsuleCollider capsule_player;

    public GameObject Timemanager;
    public TimeStop timeStop;
    public TimeBody timeBody;
    public TimeManager timeManager;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public LayerMask objLayers;

    public float cooldownTime = 0.15f;
    private float nextFireTime = 0;

    public int sword_damage_start = 20;
    public int sword_damage_end = 40;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public float cooldownTime_sword = 0.5f;
    public float forza_colpo_spada = 10f;
    private float actualbulletcooldown;
    private float actualswordcooldown;

    public bool canGun;
    public bool canSpada;

    public bool attacco_salto;

    public Vector3 fire_point_height;

    public GameObject Sprites;
    public float Direction;

    public GameObject ImpactSword;

    public bool canMove;

    public float attackmeleeTime;
    public float attackmeleemove1Time;
    public float attackmeleemove2Time;
    public float attackmelejumpTime;

    public AnimatorClipInfo[] clipInfos;
    public float duration;
    private bool canAnim;

    private AnimationClip[] clips;

    public GameObject Muzzleflash;
    public Vector3 MuzzleflashPos;

    [Header("Ammo Weapon")]
    public int ammoWeapon1;
    public int ammoWeapon2;
    public int ammoWeapon3;
    public int ammoWeapon4;
    public int ammoWeapon5;

    public bool haveammo1;
    public bool haveammo2;
    public bool haveammo3;
    public bool haveammo4;
    public bool haveammo5;

    public TextMeshProUGUI ammoGui1;
    public TextMeshProUGUI ammoGui2;
    public TextMeshProUGUI ammoGui3;
    public TextMeshProUGUI ammoGui4;
    public TextMeshProUGUI ammoGui5;

    [Header("Blaster Audio")]
    [SerializeField] private AudioSource BlasterSource;
    public AudioClip[] BlasterSound;

    [Header("Sword Audio")]
    [SerializeField] private AudioSource SwordSource;
    public AudioClip[] SwordSound;

    void Awake()
    {
        capsule_player = transform.GetComponent<CapsuleCollider>();
    }

    void Start()
    {
        Timemanager = GameObject.Find("TimeManager");
        timeStop = Timemanager.GetComponent<TimeStop>();
        timeBody = Timemanager.GetComponent<TimeBody>();
        timeManager = Timemanager.GetComponent<TimeManager>();

        actualbulletcooldown = cooldownTime;
        actualswordcooldown = cooldownTime_sword;
        canGun = true;
        canSpada = true;
        canAnim = true;
        firePoint_obj.localPosition = fire_point_height;
        //new Vector3(1.88f, 1.53f, 0);

        ///Ammo
        haveammo1 = true;
        haveammo2 = true;
        haveammo3 = true;
        haveammo4 = true;
        haveammo5 = true;

        ammoGui1.text = ammoWeapon1.ToString();
        ammoGui2.text = ammoWeapon2.ToString();
        ammoGui3.text = ammoWeapon3.ToString();
        ammoGui4.text = ammoWeapon4.ToString();
        ammoGui5.text = ammoWeapon5.ToString();

        SwordSource.pitch = 1.0f;
        BlasterSource.pitch = 1.0f;
    }

    void Update()
    {
        /*
        clipInfos = Player_shoot.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);
        AnimationClip firstClip = clipInfos[0].clip;
        duration = firstClip.averageDuration;
        */
        ammoGui1.text = ammoWeapon1.ToString();
        ammoGui2.text = ammoWeapon2.ToString();
        ammoGui3.text = ammoWeapon3.ToString();
        ammoGui4.text = ammoWeapon4.ToString();
        ammoGui5.text = ammoWeapon5.ToString();

        Direction = Sprites.transform.localScale.x;
        bool isGrounded = m_ControlledCollider.IsGrounded();
        bool edge_touch = m_ControlledCollider.IsTouchingEdge();
        bool wall_touch = m_ControlledCollider.IsCompletelyTouchingWall();
        bool dangling = m_ControlledCollider.GetGroundedInfo().IsDangling();
        var capsule_height = capsule_player.height * transform.localScale.y;

        float left = Input.GetAxis("Fire1");
        float right = Input.GetAxis("Fire2");

        //Cooldown in base ai proiettili
        switch (bullet_Manager.actualBullet) 
        {
            case 0:
                if (abilityManager.stato)
                {
                    actualbulletcooldown = 0.12f;
                }
                else
                {
                    actualbulletcooldown = cooldownTime;
                }
                break;

            case 1:
                if (abilityManager.stato)
                {
                    actualbulletcooldown = 0.12f;
                }
                else
                {
                    actualbulletcooldown = cooldownTime;
                }
                break;

            case 2:
                if (abilityManager.stato)
                {
                    actualbulletcooldown = 0.2f;
                }
                else if(timeBody.stato || timeStop.stato)
                {
                    actualbulletcooldown = cooldownTime;
                }
                else
                {
                    actualbulletcooldown = (float)(cooldownTime + 0.3f);
                }
                break;

            case 3:
                if (abilityManager.stato)
                {
                    actualbulletcooldown = 0.34f;
                }
                else
                {
                    actualbulletcooldown = (float)(cooldownTime + 0.8f);
                }
                break;

            case 4:
                if (abilityManager.stato)
                {
                    actualbulletcooldown = cooldownTime;
                }
                else
                {
                    actualbulletcooldown = (float)(cooldownTime - 0.05f);
                }
                break;
        }


        //aumento della velocità della spada
        if (abilityManager.stato)
        {
            actualswordcooldown = 0.23f;
        }
        else
        {
            actualswordcooldown = attackmeleeTime;
        }

        //Debug utilizzo armi
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            canSpada = !canSpada;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            canGun = !canGun;
        }

        ///Utilizzo armi
        if (!playerManager.isGameOver)
        {
            //Spada
            if (canAnim)
            {
                if (Time.time > nextAttackTime)
                {
                    if (right == 1 && canSpada == true)
                    {
                        if (right == 1 && capsule_height == 2.1f && movimento() == false && isGrounded == true && edge_touch == false && dangling == false)
                        {
                            //Debug.Log("Fermo");
                            SwordSource.PlayOneShot(SwordSound[0], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
                            StartCoroutine(stopfromSword());
                            Player_shoot.GetComponent<Animator>().Play("CharacterSprite_Attack_melee");
                            StartCoroutine(wait_melee());
                        }
                        else if (right == 1 && capsule_height == 2.1f && movimento() == true && isGrounded == true && edge_touch == false && dangling == false)
                        {
                            //Debug.Log("In movimento");
                            int tipo_attacco = Random.Range(1, 3);
                            //Debug.Log(tipo_attacco);
                            
                            if (tipo_attacco == 1)
                            {
                                SwordSource.PlayOneShot(SwordSound[1], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
                                Player_shoot.GetComponent<Animator>().Play("CharacterSprite_Attack_melee_movement");
                                StartCoroutine(wait_melee_Move1());
                            }
                            else if (tipo_attacco == 2)
                            {
                                SwordSource.PlayOneShot(SwordSound[2], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
                                Player_shoot.GetComponent<Animator>().Play("CharacterSprite_Attack_melee_movement_2");
                                StartCoroutine(wait_melee_Move2());
                            }
                        }
                        else if (right == 1 && capsule_height == 2.1f && isGrounded == false && edge_touch == false && wall_touch == false)
                        {
                            //Debug.Log("In aria")
                            SwordSource.PlayOneShot(SwordSound[3], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
                            Player_shoot.GetComponent<Animator>().Play("CharacterSprite_Attack_melee_Jump");
                            StartCoroutine(wait_melee_Jump());
                            attacco_salto = true;
                        }
                        nextAttackTime = Time.time + actualswordcooldown;
                    }

                }
                //Fucile
                if (Time.time > nextFireTime)
                {
                    if (left == 1 && canGun == true)
                    {
                        
                        ///Ammo0
                        if (bullet_Manager.actualBullet == 0)
                        {
                            
                            if (left == 1 && capsule_height == 2.1f && movimento() == false && isGrounded == true && edge_touch == false && wall_touch == false && dangling == false)
                            {
                                //Animazione di sparo da fermo
                                firePoint_obj.localPosition = fire_point_height;
                                Shoot();
                                Player_shoot.GetComponent<Animator>().Play("CharacterSprite_Shoot");
                                BlasterSource.PlayOneShot(BlasterSound[bullet_Manager.actualBullet], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
                                //Debug.Log("Fermo");
                            }
                            else if (left == 1 && capsule_height == 1.6f && movimento() == false && isGrounded == true && edge_touch == false && wall_touch == false && dangling == false)
                            {
                                //Animazione di sparo da fermo accovacciato
                                firePoint_obj.localPosition = new Vector3(1.88f, 1, 0);
                                Shoot();
                                Player_shoot.GetComponent<Animator>().Play("CharacterSprite_Attack_Gun_Shoot");
                                BlasterSource.PlayOneShot(BlasterSound[bullet_Manager.actualBullet], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
                                //Debug.Log("Accovacciato");
                            }
                            else if (left == 1 && capsule_height == 2.1f && movimento() == true && isGrounded == true && edge_touch == false && wall_touch == false && dangling == false)
                            {
                                //Animazione di sparo in movimento
                                firePoint_obj.localPosition = fire_point_height;
                                Shoot();
                                Player_shoot.GetComponent<Animator>().Play("CharacterSprite_Attack_Gun_Movement");
                                BlasterSource.PlayOneShot(BlasterSound[bullet_Manager.actualBullet], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
                                //Debug.Log("In movimento");
                            }
                            else if (left == 1 && capsule_height == 2.1f && isGrounded == false && edge_touch == false && wall_touch == false)
                            {
                                //Animazione di sparo in aria
                                firePoint_obj.localPosition = fire_point_height;
                                Shoot();
                                Player_shoot.GetComponent<Animator>().Play("CharacterSprite_Attack_Gun_Jump");
                                BlasterSource.PlayOneShot(BlasterSound[bullet_Manager.actualBullet], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
                                //Debug.Log("In aria");
                            }
                        }

                        ///Ammo1
                        if (bullet_Manager.actualBullet == 1 && ammoWeapon1 > 0 && haveammo1)
                        {
                            if (left == 1 && capsule_height == 2.1f && movimento() == false && isGrounded == true && edge_touch == false && wall_touch == false && dangling == false)
                            {
                                //Animazione di sparo da fermo
                                firePoint_obj.localPosition = fire_point_height;
                                Shoot();
                                Player_shoot.GetComponent<Animator>().Play("CharacterSprite_Shoot");
                                BlasterSource.PlayOneShot(BlasterSound[bullet_Manager.actualBullet], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
                                ammoWeapon1 -= 1;
                                ammoGui1.text = ammoWeapon1.ToString();
                                //Debug.Log("Fermo");
                            }
                            else if (left == 1 && capsule_height == 1.6f && movimento() == false && isGrounded == true && edge_touch == false && wall_touch == false && dangling == false)
                            {
                                //Animazione di sparo da fermo accovacciato
                                firePoint_obj.localPosition = new Vector3(1.88f, 1, 0);
                                Shoot();
                                Player_shoot.GetComponent<Animator>().Play("CharacterSprite_Attack_Gun_Shoot");
                                BlasterSource.PlayOneShot(BlasterSound[bullet_Manager.actualBullet], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
                                ammoWeapon1 -= 1;
                                ammoGui1.text = ammoWeapon1.ToString();
                                //Debug.Log("Accovacciato");
                            }
                            else if (left == 1 && capsule_height == 2.1f && movimento() == true && isGrounded == true && edge_touch == false && wall_touch == false && dangling == false)
                            {
                                //Animazione di sparo in movimento
                                firePoint_obj.localPosition = fire_point_height;
                                Shoot();
                                Player_shoot.GetComponent<Animator>().Play("CharacterSprite_Attack_Gun_Movement");
                                BlasterSource.PlayOneShot(BlasterSound[bullet_Manager.actualBullet], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
                                ammoWeapon1 -= 1;
                                ammoGui1.text = ammoWeapon1.ToString();
                                //Debug.Log("In movimento");
                            }
                            else if (left == 1 && capsule_height == 2.1f && isGrounded == false && edge_touch == false && wall_touch == false)
                            {
                                //Animazione di sparo in aria
                                firePoint_obj.localPosition = fire_point_height;
                                Shoot();
                                Player_shoot.GetComponent<Animator>().Play("CharacterSprite_Attack_Gun_Jump");
                                BlasterSource.PlayOneShot(BlasterSound[bullet_Manager.actualBullet], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
                                ammoWeapon1 -= 1;
                                ammoGui1.text = ammoWeapon1.ToString();
                                //Debug.Log("In aria");
                            }
                        }
                        
                        if(bullet_Manager.actualBullet == 1 && ammoWeapon1 == 0)
                        {
                            haveammo1 = false;
                        }
                        else if(bullet_Manager.actualBullet == 1 && ammoWeapon1 > 0)
                        {
                            haveammo1 = true;
                        }

                        ///Ammo2
                        if (bullet_Manager.actualBullet == 2 && ammoWeapon2 > 0 && haveammo2)
                        {
                            if (left == 1 && capsule_height == 2.1f && movimento() == false && isGrounded == true && edge_touch == false && wall_touch == false && dangling == false)
                            {
                                //Animazione di sparo da fermo
                                firePoint_obj.localPosition = fire_point_height;
                                Shoot();
                                Player_shoot.GetComponent<Animator>().Play("CharacterSprite_Shoot");
                                BlasterSource.PlayOneShot(BlasterSound[bullet_Manager.actualBullet], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
                                ammoWeapon2 -= 1;
                                ammoGui2.text = ammoWeapon2.ToString();
                                //Debug.Log("Fermo");
                            }
                            else if (left == 1 && capsule_height == 1.6f && movimento() == false && isGrounded == true && edge_touch == false && wall_touch == false && dangling == false)
                            {
                                //Animazione di sparo da fermo accovacciato
                                firePoint_obj.localPosition = new Vector3(1.88f, 1, 0);
                                Shoot();
                                Player_shoot.GetComponent<Animator>().Play("CharacterSprite_Attack_Gun_Shoot");
                                BlasterSource.PlayOneShot(BlasterSound[bullet_Manager.actualBullet], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
                                ammoWeapon2 -= 1;
                                ammoGui2.text = ammoWeapon2.ToString();
                                //Debug.Log("Accovacciato");
                            }
                            else if (left == 1 && capsule_height == 2.1f && movimento() == true && isGrounded == true && edge_touch == false && wall_touch == false && dangling == false)
                            {
                                //Animazione di sparo in movimento
                                firePoint_obj.localPosition = fire_point_height;
                                Shoot();
                                Player_shoot.GetComponent<Animator>().Play("CharacterSprite_Attack_Gun_Movement");
                                BlasterSource.PlayOneShot(BlasterSound[bullet_Manager.actualBullet], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
                                ammoWeapon2 -= 1;
                                ammoGui2.text = ammoWeapon2.ToString();
                                //Debug.Log("In movimento");
                            }
                            else if (left == 1 && capsule_height == 2.1f && isGrounded == false && edge_touch == false && wall_touch == false)
                            {
                                //Animazione di sparo in aria
                                firePoint_obj.localPosition = fire_point_height;
                                Shoot();
                                Player_shoot.GetComponent<Animator>().Play("CharacterSprite_Attack_Gun_Jump");
                                BlasterSource.PlayOneShot(BlasterSound[bullet_Manager.actualBullet], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
                                ammoWeapon2 -= 1;
                                ammoGui2.text = ammoWeapon2.ToString();
                                //Debug.Log("In aria");
                            }
                        }
                        
                        if(bullet_Manager.actualBullet == 2 && ammoWeapon2 == 0)
                        {
                            haveammo2 = false;
                        }
                        else if (bullet_Manager.actualBullet == 2 && ammoWeapon2 > 0)
                        {
                            haveammo2 = true;
                        }

                        ///Ammo3
                        if (bullet_Manager.actualBullet == 3 && ammoWeapon3 > 0 && haveammo3)
                        {
                            
                            if (left == 1 && capsule_height == 2.1f && movimento() == false && isGrounded == true && edge_touch == false && wall_touch == false && dangling == false)
                            {
                                //Animazione di sparo da fermo
                                firePoint_obj.localPosition = fire_point_height;
                                Shoot();
                                Player_shoot.GetComponent<Animator>().Play("CharacterSprite_Shoot");
                                BlasterSource.PlayOneShot(BlasterSound[bullet_Manager.actualBullet], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
                                ammoWeapon3 -= 1;
                                ammoGui3.text = ammoWeapon3.ToString();
                                //Debug.Log("Fermo");
                            }
                            else if (left == 1 && capsule_height == 1.6f && movimento() == false && isGrounded == true && edge_touch == false && wall_touch == false && dangling == false)
                            {
                                //Animazione di sparo da fermo accovacciato
                                firePoint_obj.localPosition = new Vector3(1.88f, 1, 0);
                                Shoot();
                                Player_shoot.GetComponent<Animator>().Play("CharacterSprite_Attack_Gun_Shoot");
                                BlasterSource.PlayOneShot(BlasterSound[bullet_Manager.actualBullet], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
                                ammoWeapon3 -= 1;
                                ammoGui3.text = ammoWeapon3.ToString();
                                //Debug.Log("Accovacciato");
                            }
                            else if (left == 1 && capsule_height == 2.1f && movimento() == true && isGrounded == true && edge_touch == false && wall_touch == false && dangling == false)
                            {
                                //Animazione di sparo in movimento
                                firePoint_obj.localPosition = fire_point_height;
                                Shoot();
                                Player_shoot.GetComponent<Animator>().Play("CharacterSprite_Attack_Gun_Movement");
                                BlasterSource.PlayOneShot(BlasterSound[bullet_Manager.actualBullet], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
                                ammoWeapon3 -= 1;
                                ammoGui3.text = ammoWeapon3.ToString();
                                //Debug.Log("In movimento");
                            }
                            else if (left == 1 && capsule_height == 2.1f && isGrounded == false && edge_touch == false && wall_touch == false)
                            {
                                //Animazione di sparo in aria
                                firePoint_obj.localPosition = fire_point_height;
                                Shoot();
                                Player_shoot.GetComponent<Animator>().Play("CharacterSprite_Attack_Gun_Jump");
                                BlasterSource.PlayOneShot(BlasterSound[bullet_Manager.actualBullet], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
                                ammoWeapon3 -= 1;
                                ammoGui3.text = ammoWeapon3.ToString();
                                //Debug.Log("In aria");
                            }
                        }

                        if (bullet_Manager.actualBullet == 3 && ammoWeapon3 == 0)
                        {
                            haveammo3 = false;
                        }
                        else if (bullet_Manager.actualBullet == 3 && ammoWeapon3 > 0)
                        {
                            haveammo3 = true;
                        }

                        ///Ammo4
                        if (bullet_Manager.actualBullet == 4 && ammoWeapon4 > 0 && haveammo4)
                        {
                            
                            if (left == 1 && capsule_height == 2.1f && movimento() == false && isGrounded == true && edge_touch == false && wall_touch == false && dangling == false)
                            {
                                //Animazione di sparo da fermo
                                firePoint_obj.localPosition = fire_point_height;
                                Shoot();
                                Player_shoot.GetComponent<Animator>().Play("CharacterSprite_Shoot");
                                BlasterSource.PlayOneShot(BlasterSound[bullet_Manager.actualBullet], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
                                ammoWeapon4 -= 1;
                                ammoGui4.text = ammoWeapon4.ToString();
                                //Debug.Log("Fermo");
                            }
                            else if (left == 1 && capsule_height == 1.6f && movimento() == false && isGrounded == true && edge_touch == false && wall_touch == false && dangling == false)
                            {
                                //Animazione di sparo da fermo accovacciato
                                firePoint_obj.localPosition = new Vector3(1.88f, 1, 0);
                                Shoot();
                                Player_shoot.GetComponent<Animator>().Play("CharacterSprite_Attack_Gun_Shoot");
                                BlasterSource.PlayOneShot(BlasterSound[bullet_Manager.actualBullet], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
                                ammoWeapon4 -= 1;
                                ammoGui4.text = ammoWeapon4.ToString();
                                //Debug.Log("Accovacciato");
                            }
                            else if (left == 1 && capsule_height == 2.1f && movimento() == true && isGrounded == true && edge_touch == false && wall_touch == false && dangling == false)
                            {
                                //Animazione di sparo in movimento
                                firePoint_obj.localPosition = fire_point_height;
                                Shoot();
                                Player_shoot.GetComponent<Animator>().Play("CharacterSprite_Attack_Gun_Movement");
                                BlasterSource.PlayOneShot(BlasterSound[bullet_Manager.actualBullet], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
                                ammoWeapon4 -= 1;
                                ammoGui4.text = ammoWeapon4.ToString();
                                //Debug.Log("In movimento");
                            }
                            else if (left == 1 && capsule_height == 2.1f && isGrounded == false && edge_touch == false && wall_touch == false)
                            {
                                //Animazione di sparo in aria
                                firePoint_obj.localPosition = fire_point_height;
                                Shoot();
                                Player_shoot.GetComponent<Animator>().Play("CharacterSprite_Attack_Gun_Jump");
                                BlasterSource.PlayOneShot(BlasterSound[bullet_Manager.actualBullet], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
                                ammoWeapon4 -= 1;
                                ammoGui4.text = ammoWeapon4.ToString();
                                //Debug.Log("In aria");
                            }
                        }

                        if (bullet_Manager.actualBullet == 4 && ammoWeapon4 == 0)
                        {
                            haveammo4 = false;
                        }
                        else if (bullet_Manager.actualBullet == 4 && ammoWeapon4 > 0)
                        {
                            haveammo4 = true;
                        }

                        ///Ammo5
                        if (bullet_Manager.actualBullet == 5 && ammoWeapon5 > 0 && haveammo5)
                        {
                            
                            if (left == 1 && capsule_height == 2.1f && movimento() == false && isGrounded == true && edge_touch == false && wall_touch == false && dangling == false)
                            {
                                //Animazione di sparo da fermo
                                firePoint_obj.localPosition = fire_point_height;
                                Shoot();
                                Player_shoot.GetComponent<Animator>().Play("CharacterSprite_Shoot");
                                BlasterSource.PlayOneShot(BlasterSound[bullet_Manager.actualBullet], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
                                ammoWeapon5 -= 1;
                                ammoGui5.text = ammoWeapon5.ToString();
                                //Debug.Log("Fermo");
                            }
                            else if (left == 1 && capsule_height == 1.6f && movimento() == false && isGrounded == true && edge_touch == false && wall_touch == false && dangling == false)
                            {
                                //Animazione di sparo da fermo accovacciato
                                firePoint_obj.localPosition = new Vector3(1.88f, 1, 0);
                                Shoot();
                                Player_shoot.GetComponent<Animator>().Play("CharacterSprite_Attack_Gun_Shoot");
                                BlasterSource.PlayOneShot(BlasterSound[bullet_Manager.actualBullet], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
                                ammoWeapon5 -= 1;
                                ammoGui5.text = ammoWeapon5.ToString();
                                //Debug.Log("Accovacciato");
                            }
                            else if (left == 1 && capsule_height == 2.1f && movimento() == true && isGrounded == true && edge_touch == false && wall_touch == false && dangling == false)
                            {
                                //Animazione di sparo in movimento
                                firePoint_obj.localPosition = fire_point_height;
                                Shoot();
                                Player_shoot.GetComponent<Animator>().Play("CharacterSprite_Attack_Gun_Movement");
                                BlasterSource.PlayOneShot(BlasterSound[bullet_Manager.actualBullet], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
                                ammoWeapon5 -= 1;
                                ammoGui5.text = ammoWeapon5.ToString();
                                //Debug.Log("In movimento");
                            }
                            else if (left == 1 && capsule_height == 2.1f && isGrounded == false && edge_touch == false && wall_touch == false)
                            {
                                //Animazione di sparo in aria
                                firePoint_obj.localPosition = fire_point_height;
                                Shoot();
                                Player_shoot.GetComponent<Animator>().Play("CharacterSprite_Attack_Gun_Jump");
                                BlasterSource.PlayOneShot(BlasterSound[bullet_Manager.actualBullet], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
                                ammoWeapon5 -= 1;
                                ammoGui5.text = ammoWeapon5.ToString();
                                //Debug.Log("In aria");
                            }
                        }

                        if (bullet_Manager.actualBullet == 5 && ammoWeapon5 == 0)
                        {
                            haveammo5 = false;
                        }
                        else if (bullet_Manager.actualBullet == 5 && ammoWeapon5 > 0)
                        {
                            haveammo5 = true;
                        }

                        nextFireTime = Time.time + actualbulletcooldown;
                    }
                }
            }
        }
        UpdateAnimClipTimes();
    }

    IEnumerator wait_melee()
    {
        canAnim = false;
        yield return new WaitForSeconds(attackmeleeTime);
        canAnim = true;
    }

    IEnumerator wait_melee_Move1()
    {
        canAnim = false;
        yield return new WaitForSeconds(attackmeleemove1Time);
        canAnim = true;
    }

    IEnumerator wait_melee_Move2()
    {
        canAnim = false;
        yield return new WaitForSeconds(attackmeleemove2Time);
        canAnim = true;
    }

    IEnumerator wait_melee_Jump()
    {
        canAnim = false;
        yield return new WaitForSeconds(attackmelejumpTime);
        canAnim = true;
    }

    bool movimento()
    {
        if(m_CharacterController.GetInputMovement().x > 0.000f || m_CharacterController.GetInputMovement().x < 0.000f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Shoot()
    {
        //Instantiate(bulletPrefab, firePoint_obj.position, firePoint_obj.rotation);
        Instantiate(bullet_Manager.Bullets[bullet_Manager.actualBullet], firePoint_obj.position, firePoint_obj.rotation);
        if(Direction > 0)
        {
            Instantiate(Muzzleflash, firePoint_obj.position + MuzzleflashPos * Direction, firePoint_obj.rotation);
        }
        else
        {
            Instantiate(Muzzleflash, firePoint_obj.position + MuzzleflashPos * Direction, Quaternion.EulerAngles(firePoint_obj.rotation.x, 180f, firePoint_obj.rotation.z));
        }
        
    }

    public void Attack()
    {
        Collider[] hitinfo = Physics.OverlapSphere(attackPoint.position, attackRange, objLayers);
        foreach(Collider obj in hitinfo)
        {
            //Debug.Log(obj.ToString());
            var rig = obj.GetComponent<Rigidbody>();

            if (rig != null)
            {
                float direction = Player_sprites.transform.localScale.x;

                if (rig.gameObject.tag != "Sparky")
                {
                    Instantiate(ImpactSword, rig.transform.position, Quaternion.identity);
                }
                
                if(rig.GetComponent<Enemy>() != null)
                {
                    int sword_damage = 0;
                    //Debug.Log("Colpito");
                    colpito();
                    if (attacco_salto)
                    {
                        //Debug.Log("Salto");
                        sword_damage = Random.Range(sword_damage_start, sword_damage_end);
                        sword_damage = sword_damage + 10;
                    }
                    else
                    {
                        //Debug.Log("No Salto");
                        sword_damage = Random.Range(sword_damage_start, sword_damage_end);
                    }
                    
                    //Debug.Log(sword_damage);
                    rig.GetComponent<Enemy>().TakeDamage(sword_damage);
                    if(rig.GetComponent<Enemy>().death == false)
                    {
                        //Abilitare lo shake della telecamera;
                        //cameraLogic.Shake();
                    }
                    
                    attacco_salto = false;
                }
                //Debug.Log(direction);
                //Debug.Log(rig.gameObject.tag);
                if(rig.gameObject.tag != "Player" && rig.gameObject.tag != "Enemy")
                {
                    rig.AddForce(transform.right * forza_colpo_spada * direction, ForceMode.VelocityChange);
                }
            }
        }
    }

    public bool colpito()
    {
        return true;
    }

    public void UpdateAnimClipTimes()
    {
        clips = Player_shoot.GetComponent<Animator>().runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "CharacterSprite_Attack_melee":
                    attackmeleeTime = clip.length;
                    break;
                case "CharacterSprite_Attack_melee_movement":
                    attackmeleemove1Time = clip.length;
                    break;
                case "CharacterSprite_Attack_melee_movement_2":
                    attackmeleemove2Time = clip.length;
                    break;
                case "CharacterSprite_Attack_melee_Jump":
                    attackmelejumpTime = clip.length;
                    break;
            }
        }
    }

    IEnumerator stopfromSword()
    {
        canMove = false;
        //Debug.Log(canMove);
        yield return new WaitForSeconds(attackmeleeTime - 0.1f);
        canMove = true;
        //Debug.Log(canMove);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Color preColor = Gizmos.color;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.color = preColor;
    }
}