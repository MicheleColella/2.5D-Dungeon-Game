using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_GUI_counter : MonoBehaviour
{
    public Trigger_Boss_spawn trigger_Boss_Spawn;

    public GameObject BossBar;
    public GameObject FillBoss;

    public Room_closer room_Closer;
    public int enemies_count;
    public GameObject TextEnemy;
    private Text enemyText;
    private Animator enemyTextAnim;

    private Animator BossBarAnim;
    private Animator FillBossAnim;

    private void Start()
    {
        BossBar = GameObject.Find("BossBarBack");
        FillBoss = GameObject.Find("FillBoss");

        if (TextEnemy != null)
        {
            enemyText = TextEnemy.GetComponent<Text>();
            enemyTextAnim = enemyText.GetComponent<Animator>();
        }
        BossBarAnim = BossBar.GetComponent<Animator>();
        FillBossAnim = FillBoss.GetComponent<Animator>();
    }

    private void Update()
    {
        enemies_count = room_Closer.enemy_cont;

        if(TextEnemy != null)
        {
            enemyText.text = enemies_count.ToString();

            if (enemies_count == 0)
            {
                enemyTextAnim.Play("Disable_text");
            }
            else
            {
                enemyTextAnim.Play("Enable_text");
            }
        }

        if(trigger_Boss_Spawn != null)
        {
            if (trigger_Boss_Spawn.triggered)
            {
                if (enemies_count == 0)
                {
                    BossBarAnim.Play("Disappear");
                    FillBossAnim.Play("Disappear");
                    //BossBar.SetActive(false);
                }
                else
                {
                    BossBarAnim.Play("Appear");
                    FillBossAnim.Play("Appear");
                    //BossBar.SetActive(true);
                }
            }
        }
    }
}
