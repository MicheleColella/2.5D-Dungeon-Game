using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public Animator Player_sprites;
    public float actualtime = 1f;
    public float slowdownFactor = 0.25f;
    public float slowdownLength = 2f;
    public bool slowtimeactive = false;

    public void DoSlowmotion()
    {
        slowtimeactive = true;
        slowtime();
        speed_animation();
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
    public void DontSlowmotion()
    {
        slowtimeactive = false;
        slowtime();
        normal_animation();
        Time.timeScale = actualtime;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public bool slowtime()
    {
        if (slowtimeactive)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void speed_animation()
    {
        Player_sprites.SetFloat("animspeed", 3);
    }

    private void normal_animation()
    {
        Player_sprites.SetFloat("animspeed", 1);
    }
}
