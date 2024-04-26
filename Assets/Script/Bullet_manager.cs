using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_manager : MonoBehaviour
{
    public GameObject[] Bullets;
    public int actualBullet;

    public PlayerManager playerManager;
    public GameObject player;

    [Header("BulletGUI")]
    public GameObject bulletGUI1;
    public GameObject bulletGUI2;
    public GameObject bulletGUI3;
    public GameObject bulletGUI4;
    public GameObject bulletGUI5;
    public GameObject bulletGUI6;

    private void Start()
    {
        bulletGUI1 = GameObject.Find("Bullet1GUI");
        bulletGUI2 = GameObject.Find("Bullet2GUI");
        bulletGUI3 = GameObject.Find("Bullet3GUI");
        bulletGUI4 = GameObject.Find("Bullet4GUI");
        bulletGUI5 = GameObject.Find("Bullet5GUI");
        bulletGUI6 = GameObject.Find("Bullet6GUI");

        player = GameObject.Find("player_controller");
        playerManager = player.GetComponent<PlayerManager>();

        actualBullet = 0;
    }

    void Update()
    {
        var d = Input.GetAxis("Mouse ScrollWheel");
        if (!playerManager.isGameOver)
        {
            if (d > 0f || Input.GetButtonDown("RB"))
            {
                actualBullet += 1;
            }
            else if (d < 0f || Input.GetButtonDown("LB"))
            {
                actualBullet -= 1;
            }

            if (Bullets.Length <= actualBullet)
            {
                actualBullet = Bullets.Length - 1;
            }
            else if (actualBullet <= 0)
            {
                actualBullet = 0;
            }
        }

        switch (actualBullet)
        {
            case 0:
                bulletGUI1.SetActive(true);
                bulletGUI2.SetActive(false);
                bulletGUI3.SetActive(false);
                bulletGUI4.SetActive(false);
                bulletGUI5.SetActive(false);
                bulletGUI6.SetActive(false);
                break;
            
            case 1:
                bulletGUI1.SetActive(false);
                bulletGUI2.SetActive(true);
                bulletGUI3.SetActive(false);
                bulletGUI4.SetActive(false);
                bulletGUI5.SetActive(false);
                bulletGUI6.SetActive(false);
                break;

            case 2:
                bulletGUI1.SetActive(false);
                bulletGUI2.SetActive(false);
                bulletGUI3.SetActive(true);
                bulletGUI4.SetActive(false);
                bulletGUI5.SetActive(false);
                bulletGUI6.SetActive(false);
                break;

            case 3:
                bulletGUI1.SetActive(false);
                bulletGUI2.SetActive(false);
                bulletGUI3.SetActive(false);
                bulletGUI4.SetActive(true);
                bulletGUI5.SetActive(false);
                bulletGUI6.SetActive(false);
                break;

            case 4:
                bulletGUI1.SetActive(false);
                bulletGUI2.SetActive(false);
                bulletGUI3.SetActive(false);
                bulletGUI4.SetActive(false);
                bulletGUI5.SetActive(true);
                bulletGUI6.SetActive(false);
                break;
            case 5:
                bulletGUI1.SetActive(false);
                bulletGUI2.SetActive(false);
                bulletGUI3.SetActive(false);
                bulletGUI4.SetActive(false);
                bulletGUI5.SetActive(false);
                bulletGUI6.SetActive(true);
                break;
        }
    }

    //Debug.Log(Bullet[actualBullet]);
}
