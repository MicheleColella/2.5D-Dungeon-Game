using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles_killer : MonoBehaviour
{
    public GameObject timemanager;
    public TimeStop timeStop;
    public TimeBody timeBody;
    public ParticleSystem particles;
    public float kill_time = 9.0f;
    public bool abilityAffect;

    int cont = 0;
    void Start()
    {
        timemanager = GameObject.Find("TimeManager");
        timeStop = timemanager.GetComponent<TimeStop>();
        timeBody = timemanager.GetComponent<TimeBody>();

        StartCoroutine(SelfDestruct());
    }

    private void Update()
    {
        if (!abilityAffect)
        {
            if (timeBody.stato || timeStop.stato)
            {
                StartCoroutine(PauseParticles());
            }

            if (!timeBody.stato && !timeStop.stato && cont >= 1)
            {
                cont = 0;
                if(particles != null)
                {
                    particles.Play();
                }
            }
        }
    }

    IEnumerator PauseParticles()
    {
        yield return new WaitForSeconds(0.1f);
        if (particles != null)
        {
            particles.Pause();
        }
        cont += 1;
    }
    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(kill_time);
        Destroy(gameObject);
    }
}
