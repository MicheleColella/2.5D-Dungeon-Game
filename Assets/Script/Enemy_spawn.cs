using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_spawn : MonoBehaviour
{
    public RoomTemplates roomTemplates;

    public GameObject trigger1;
    public GameObject trigger2;

    [Header("Spawner1")]
    public List<GameObject> Spawner1;
    public List<GameObject> Spawner2;

    [Header("Nemici")]
    public GameObject Enemy_type_1;

    [Header("ManagerTrigger")]
    public List<GameObject> ManagerTriggers;

    bool end1 = true;
    bool end2 = true;
    private GameObject[] gos;
    public List<int> spawns;

    private void Start()
    {
        StartCoroutine(GetSpawn());
    }

    void Update()
    {
        ///Stanze
        for (int d = 0; d < ManagerTriggers.Count; d++)
        {
            if (ManagerTriggers[d] != null)
            {
                if (ManagerTriggers[d].GetComponent<Room_trigger>().triggered && end1)
                {
                    for (int i = 0; i < Spawner1.Count; i++)
                    {
                        //Debug.Log(Spawner1.Count);
                        if (Spawner1[i] != null)
                        {
                            Instantiate(Enemy_type_1, Spawner1[i].transform.position, Quaternion.identity);
                        }
                    }
                    end1 = false;
                }
            }
        }

        /*
        ///Stanza2
        //Debug.Log(trigger2 == null);
        if (trigger2 != null)
        {
            if (GameObject.Find("TriggerMaster2").GetComponent<Room_trigger>().triggered && end2)
            {
                for (int i = 0; i < Spawner2.Count; i++)
                {
                    //Debug.Log(Spawner2.Count);
                    if (Spawner2[i] != null)
                    {
                        Instantiate(Enemy_type_1, Spawner2[i].transform.position, Quaternion.identity);
                    }
                }
                end2 = false;
            }
        }
        */
    }

    IEnumerator GetSpawn()
    {
        yield return new WaitForSeconds(2.0f);
        TakeTrigger();
        //Debug.Log("Fase1");
        for (int d = 0; d < ManagerTriggers.Count; d++)
        {
            spawns.Add(ManagerTriggers[d].GetComponent<Room_trigger>().Spawner.Length);
        }
        
        //trigger1 = GameObject.Find("TriggerMaster1");
        //trigger2 = GameObject.Find("TriggerMaster2");
        for (int i = 0; i < ManagerTriggers.Count; i++)
        {
            for (int j = 0; j < spawns[i]; j++)
            {
                Spawner1.Add(ManagerTriggers[i].GetComponent<Room_trigger>().Spawner[j]);
            }
        }
        yield return new WaitForSeconds(1.0f);
        //Debug.Log("Fase2");
    }

    void TakeTrigger()
    {
        int cont = 0;
        gos = (GameObject[])FindObjectsOfType(typeof(GameObject));
        for (int i = 0; i < gos.Length; i++)
        {
            if (gos[i].name.Contains("TriggerMaster"))
            {
                ManagerTriggers.Add(gos[i]);
                cont += 1;
            }
        }
        //Debug.Log(cont);
    }
}
