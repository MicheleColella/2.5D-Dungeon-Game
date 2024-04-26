using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Boss_GUI_Health boss_GUI_Health;
    public TimeStop timeStop;
    public Collider colliders;
    public GameObject Ammo_Drops;
    public GameObject Health_Drops;
    public GameObject FloatingTextPrefab;
    //public MeleeEnemy meleeenemy;
    public MeleeEnemy meleeenemy;
    public SpriteRenderer m_SpriteRenderer;
    public float flashTime = 0.04f;
    public float health = 100f;
    public bool TakeDamager = false;
    public bool damage_taken;
    public float enemy_defence = 1f;
    public int destroyerTimer;
    public bool canTakeDamage;

    public GameObject deadEffect;
    public bool death = false;

    public Vector3 posizione;
    int cont_d = 0;

    public bool useBossBar;

    private void Start()
    {
        boss_GUI_Health = GameObject.Find("BossHealthBAr").GetComponent<Boss_GUI_Health>();
        float maxhe = health;
        if (useBossBar)
        {
            boss_GUI_Health.SetMaxHealth(Mathf.RoundToInt(maxhe));
        }
        
        colliders = gameObject.GetComponent<Collider>();
        m_SpriteRenderer = this.transform.Find("Sprites").GetComponent<SpriteRenderer>();
        timeStop = GameObject.Find("TimeManager").GetComponent<TimeStop>();
        //m_SpriteRenderer = GetComponents<SpriteRenderer>();
        m_SpriteRenderer.color = Color.white;
        canTakeDamage = true;
    }


    public void TakeDamage(int damage)
    {
        if (useBossBar)
        {
            boss_GUI_Health.SetHealth(Mathf.RoundToInt(health));
        }
            
        StartCoroutine(Damage_bool());
        if (meleeenemy != null)
        {
            StartCoroutine(ChaseCool());
        }

        if (canTakeDamage)
        {
            //Debug.Log(damage * enemy_defence);
            health -= Mathf.RoundToInt(damage * enemy_defence);
            StartCoroutine(EFlash());
            //Inserire l'animazione di danno ottenuto
            //Debug.Log(health);

            if (FloatingTextPrefab && health > 0)
            {
                ShowFloatingText(Mathf.RoundToInt(damage * enemy_defence));
            }

            if (health <= 0)
            {
                death = true;
            }
        }
        CameraShake1.Instance.ShakeCamera(1, 0.1f, 0f);
    }

    IEnumerator Damage_bool()
    {
        damage_taken = true;
        yield return new WaitForSeconds(2.0f);
        damage_taken = false;
    }

    IEnumerator ChaseCool()
    {
        meleeenemy.Chase = true;
        yield return new WaitForSeconds(5.0f);
        meleeenemy.Chase = false;
    }


    private void Update()
    {
        if (useBossBar)
        {
            boss_GUI_Health.SetHealth(Mathf.RoundToInt(health));
        } 
        posizione = transform.position;

        if(death && !timeStop.stato && cont_d == 0)
        {
            cont_d += 1;
            Die();
        }
    }

    void ShowFloatingText(float damage)
    {
        Quaternion rot = Quaternion.Euler(0f, 0f, 0f);
        var go = Instantiate(FloatingTextPrefab, transform.position, rot, transform);
        go.GetComponent<TextMesh>().text = damage.ToString();
    }

    void FlashStart()
    {
        m_SpriteRenderer.color = Color.red;
        Invoke("FlashStop", flashTime);
    }

    void FlashStop()
    {
        m_SpriteRenderer.color = Color.white;
    }

    IEnumerator EFlash()
    {
        TakeDamager = true;
        m_SpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashTime);
        m_SpriteRenderer.color = Color.white;
        if (meleeenemy != null)
        {
            TakeDamager = meleeenemy.return_damage();
        }
    }
    public void Die()
    {
        colliders.enabled = false;
        int rand_ammo = Random.Range(0, 1);
        int rand_health = Random.Range(0, 1);

        if(rand_ammo == 0)
        {
            Instantiate(Ammo_Drops, posizione, Quaternion.identity);
        }

        if (rand_health == 0)
        {
            Instantiate(Health_Drops, posizione, Quaternion.identity);
        } 

        Instantiate(deadEffect, posizione, Quaternion.identity);
        StartCoroutine(destroyer());
    }

    IEnumerator destroyer()
    {
        yield return new WaitForSeconds(destroyerTimer);
        Destroy(gameObject);
    }
}
