using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_closer : MonoBehaviour
{
    ///Tentativo 1
    /*
    public List<Collider> colliders;
    public List<Collider> GetColliders() { return colliders; }

    public int Enemy_number;
    private bool isColliding;

    private void Start()
    {
        Enemy_number = 0;
    }

    private void Update()
    {
        Enemy_number = colliders.Count;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isColliding) return;
        isColliding = true;

        if (!colliders.Contains(other)) 
        {
            if (other.gameObject.tag == "Enemy")
            {
                colliders.Add(other);
            }
        }

        StartCoroutine(Reset());
    }

    private void OnTriggerExit(Collider other)
    {
        colliders.Remove(other);
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(0.5f);
        isColliding = false;
    }
    */


    GameObject[] enemies;

    public int enemy_cont;
    public bool isaboss;
    private bool active_counter;
    public bool door_closed;
    public GameObject[] doors;

    private void Start()
    {
        active_counter = false;
        door_closed = false;
        StartCoroutine(active_cont());
    }

    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemy_cont = enemies.Length;

        if (active_counter)
        {
            if (enemy_cont == 0)
            {
                door_closed = false;
                for(int i = 0; i < doors.Length; i++)
                {
                    doors[i].SetActive(false);
                }
            }
            else
            {
                door_closed = true;
                for (int i = 0; i < doors.Length; i++)
                {
                    doors[i].SetActive(true);
                }
            }
        }
    }

    IEnumerator active_cont()
    {
        yield return new WaitForSeconds(0.1f);
        active_counter = true;
    }
}
