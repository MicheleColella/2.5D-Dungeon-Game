using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public Animator anim;
    public HealthBar healthBar;
    public int playerHP = 100;
    public float flashTime = 0.04f;
    public float respawnTime = 1.0f;
    public int currentplayerHP;
    public TextMeshProUGUI playerHPText;
    public bool isGameOver;
    public SpriteRenderer m_SpriteRenderer;
    private bool canDamage;
    public float damagecooltime;
    private Scene scene;

    [Header("Damage Audio")]
    [SerializeField] private AudioSource DamageSource;
    public AudioClip[] DamageSound;

    void Start()
    {
        currentplayerHP = playerHP;
        healthBar.SetMaxHealth(playerHP);
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(currentplayerHP);
        if (currentplayerHP >= 0)
        {
            playerHPText.text = currentplayerHP + "/" + playerHP;
        }
        else if(currentplayerHP <= 0)
        {
            currentplayerHP = 0;
        }

        
        if (isGameOver)
        {
            scene = SceneManager.GetActiveScene();
            StartCoroutine(Respawn());
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (canDamage)
        {
            damageSound();
            StartCoroutine(damageColldown());
            currentplayerHP -= damageAmount;
            StartCoroutine(EFlash());
        }
        healthBar.SetHealth(currentplayerHP);
        if (currentplayerHP <= 0)
        {
            anim.Play("CharacterSprite_Death");
            isGameOver = true;
        }
    }

    public void damageSound()
    {
        int rand = Random.Range(0, DamageSound.Length);
        DamageSource.PlayOneShot(DamageSound[rand], (AudioManager.Voice_Sound / 1000) * (AudioManager.General_Sound / 10));
    }

    IEnumerator damageColldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(damagecooltime);
        canDamage = true;
    }

    IEnumerator EFlash()
    {
        m_SpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashTime);
        m_SpriteRenderer.color = Color.white;
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        SceneManager.LoadScene(scene.name);
    }
}
