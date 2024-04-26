using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_spawn : MonoBehaviour
{
    public bool triggered;

    [Header("Spawner Enemy Type 1")]
    public GameObject[] Spawner_Type_1;
    
    [Header("Spawner Enemy Type 2")]
    public GameObject[] Spawner_Type_2;

    [Header("Enemy Type 1")]
    public GameObject[] Enemy_type_1;

    [Header("Enemy Type 2")]
    public GameObject[] Enemy_type_2;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            if (other.gameObject.tag == "Player")
            {
                //Debug.Log("HIT");

                ///Spawn Enemy 1
                for (int i = 0; i < Spawner_Type_1.Length; i++)
                {
                    //Debug.Log(Spawner1.Count);
                    if (Spawner_Type_1[i] != null)
                    {
                        int not = Random.Range(0, 2);
                        int num = Random.Range(0, Enemy_type_1.Length);
                        //Debug.Log(not);
                        if (not == 0)
                        {
                            Instantiate(Enemy_type_1[num], Spawner_Type_1[i].transform.position, Quaternion.identity);
                        }
                    }
                }

                ///Spawn Enemy 2
                for (int i = 0; i < Spawner_Type_2.Length; i++)
                {
                    //Debug.Log(Spawner1.Count);
                    if (Spawner_Type_2[i] != null)
                    {
                        int not = Random.Range(0, 2);
                        int num = Random.Range(0, Enemy_type_2.Length);
                        if (not == 0)
                        {
                            Instantiate(Enemy_type_2[num], Spawner_Type_2[i].transform.position, Quaternion.identity);
                        }
                    }
                }
                triggered = true;
            }
        }
    }
}
