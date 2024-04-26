using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Shield : MonoBehaviour
{
    public TimeBody timeBody;
    public TimeStop timeStop;

    public Enemy enemy;
    public GameObject Shield;
    public bool Shield_attivo;
    public bool Shield_starting;
    public float target_life;
    public float target_distance_life;
    public int Shield_time;

    private Animator ShieldAnim;

    void Start()
    {
        timeBody = GameObject.Find("TimeManager").GetComponent<TimeBody>();
        timeStop = GameObject.Find("TimeManager").GetComponent<TimeStop>();
        enemy = gameObject.GetComponent<Enemy>();
        ShieldAnim = Shield.GetComponent<Animator>();
        Shield_attivo = false;
        target_life = enemy.health - target_distance_life;
    }

    
    void Update()
    {
        if (target_life < 0)
        {
            target_life = -100;
        }

        if ((timeStop.stato || timeBody.stato) && Shield_attivo)
        {
            StartCoroutine(Still_Active_shield());
        }
        else if((!timeStop.stato || !timeBody.stato) && !Shield_attivo)
        {
            ShieldAnim.Play("Disappear");
        }

        if(target_life >= enemy.health && !Shield_attivo && (!timeStop.stato && !timeBody.stato))
        {
            enemy.canTakeDamage = false;
            Shield_attivo = true;
            Shield.SetActive(Shield_attivo);
            StartCoroutine(Active_shield());
            StartCoroutine(shield_starting());
        }
        else if (target_life >= enemy.health)
        {
            target_life = enemy.health - (target_distance_life / 2);
            if(target_life < 0)
            {
                target_life = -1;
            }
        }

        if (enemy.death)
        {
            Shield_attivo = false;
        }
    }

    IEnumerator Active_shield()
    {
        ShieldAnim.Play("Appear");
        yield return new WaitForSeconds(Shield_time);
        if (!timeStop.stato && !timeBody.stato)
        {
            Shield_attivo = false;
            enemy.canTakeDamage = true;
        }
        target_life = enemy.health - target_distance_life;
        if (target_life < 0)
        {
            target_life = -1;
        }
        ShieldAnim.Play("Disappear");
        yield return new WaitForSeconds(1);
        Shield.SetActive(Shield_attivo);
    }
    IEnumerator Still_Active_shield()
    {
        ShieldAnim.Play("active");
        yield return new WaitForSeconds(Shield_time);
        if (!timeStop.stato && !timeBody.stato)
        {
            Shield_attivo = false;
            enemy.canTakeDamage = true;
        }
        target_life = enemy.health - target_distance_life;
        if (target_life < 0)
        {
            target_life = -1;
        }
        ShieldAnim.Play("Disappear");
        yield return new WaitForSeconds(1);
        Shield.SetActive(Shield_attivo);
    }

    IEnumerator shield_starting()
    {
        Shield_starting = true;
        yield return new WaitForSeconds(1.5f);
        Shield_starting = false;
    }
}
