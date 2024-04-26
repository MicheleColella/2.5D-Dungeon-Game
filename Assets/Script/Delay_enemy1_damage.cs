using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delay_enemy1_damage : MonoBehaviour
{
    public PlayerManager playerManager;
    public GameObject player_life;

    public int sword_enemy_damage_min = 20;
    public int sword_enemy_damage_max = 30;
    public int sword_enemy_damage;

    bool damage = false;

    [Header("Sword Audio")]
    [SerializeField] private AudioSource SwordSource;
    public AudioClip[] SwordSound;

    private void Start()
    {
        player_life = GameObject.Find("player_controller");
        playerManager = player_life.GetComponent<PlayerManager>();

        SwordSource.pitch = 1.0f;
    }

    public void attiva_danno_giocatore()
    {
        damage = true;
        //Debug.Log("Attivo");
    }
    public void meleeSound()
    {
        int rand = Random.Range(0, 3);
        SwordSource.PlayOneShot(SwordSound[0], (AudioManager.SFX_Sound / 1000) * (AudioManager.General_Sound / 10));
    }

    private void Update()
    {
        if (damage)
        {
            sword_enemy_damage = Random.Range(sword_enemy_damage_min, sword_enemy_damage_max);
            playerManager.TakeDamage(sword_enemy_damage);
            damage = false;
        }
    }
}
