using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public TimeManager timemanager;
    bool canAbility1;
    public bool stato;

    public PlayerManager playerManager;
    public GameObject Timemanager;
    public TimeStop timeStop;
    public TimeBody timeBody;

    public bool debug_time;

    private void Start()
    {
        Timemanager = GameObject.Find("TimeManager");
        timeStop = Timemanager.GetComponent<TimeStop>();
        timeBody = Timemanager.GetComponent<TimeBody>();

        StartCoroutine(cooldown());
    }

    
    void Update()
    {
        float y = Input.GetAxis("Fire3");
        //Debug.Log(y);

        if (!playerManager.isGameOver)
        {
            if (!timeStop.stato && !timeBody.stato)
            {
                if ((Input.GetKeyDown(KeyCode.Alpha1) || y == 1) && canAbility1 == true)
                {
                    StartCoroutine(timer());
                    StartCoroutine(cooldown());
                }
            }
        }
    }

    private IEnumerator timer()
    {
        stato = true;
        timemanager.DoSlowmotion();
        yield return new WaitForSeconds(1);
        stato = false;
        timemanager.DontSlowmotion();
    }
    private IEnumerator cooldown()
    {
        canAbility1 = false;
        //Debug.Log(canAbility1);
        yield return new WaitForSeconds(15);
        canAbility1 = true;
        //Debug.Log(canAbility1);
    }
}
