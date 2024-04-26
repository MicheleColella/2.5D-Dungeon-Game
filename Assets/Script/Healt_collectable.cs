using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healt_collectable : MonoBehaviour
{
    public bool RandAmount;
    public int Health_to_add;

    private void Start()
    {
        if (RandAmount)
        {
            Health_to_add = Random.Range(5, 20);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject colGo = other.gameObject;
        if (colGo.tag == "Player")
        {
            for(int i = 0; i < Health_to_add; i++)
            {
                if(colGo.GetComponent<PlayerManager>().currentplayerHP < colGo.GetComponent<PlayerManager>().playerHP)
                {
                    colGo.GetComponent<PlayerManager>().currentplayerHP += 1;
                }
            }
            CameraShake1.Instance.ShakeCamera(0.2f, 0.2f, 0f);
            Destroy(gameObject);
        }
    }
}
