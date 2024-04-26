using UnityEngine;
using System.Collections;
//--------------------------------------------------------------------
//Detects when the capsule is being squished (using CapsuleVolumeIntegrity) and attempts to respawn
//Use timer to prevent overzealous respawning
//--------------------------------------------------------------------
public class OnSquishRespawner : MonoBehaviour {
    [SerializeField] CapsuleVolumeIntegrity m_VolumePreserver = null;
    [SerializeField] float m_SquishTime = 0.0f;
    float m_LastSquishTime;
    bool m_IsSquishing;

    public bool squished;

    public float SquishTimer = 1.0f;
    public int SquishDamage = 10;

    private GameObject player;
    public PlayerManager playerManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();
    }

    void FixedUpdate () 
	{
	    if (m_VolumePreserver.IsBeingSquished())
        {
            if (!m_IsSquishing)
            {
                m_LastSquishTime = Time.fixedTime;
            }
            m_IsSquishing = true;
        }
        else
        {
            m_IsSquishing = false;
        }
        if (m_IsSquishing && Time.fixedTime - m_LastSquishTime > m_SquishTime)
        {
            StartCoroutine(TimeSquish());
        }
	}


    IEnumerator TimeSquish()
    {
        playerManager.TakeDamage(SquishDamage);
        squished = true;
        Debug.Log("Schiacciato = " + squished);
        yield return new WaitForSeconds(SquishTimer);
        squished = false;
    }
}
