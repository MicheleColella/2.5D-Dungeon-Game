using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioManager : MonoBehaviour
{
    public GameObject Timemanager;
    public TimeStop timeStop;
    public TimeBody timeBody;
    public TimeManager timeManager;

    [Header("Sound value")]
    public static float General_Sound;
    public static float Music_Sound;
    public static float SFX_Sound;
    public static float Voice_Sound;
    
    [Header("Debug value")]
    public float General_Sound_d;
    public float Music_Sound_d;
    public float SFX_Sound_d;
    public float Voice_Sound_d;

    [Header("Sound value")]
    public Slider General_slider;
    public Slider Music_slider;
    public Slider SFX_slider;
    public Slider Voice_slider;

    [Header("Text value")]
    public TextMeshProUGUI General_text;
    public TextMeshProUGUI Music_text;
    public TextMeshProUGUI SFX_text;
    public TextMeshProUGUI Voice_text;

    [Header("Audio source")]
    public AudioSource[] AudioSources;

    [Header("Music Audio source")]
    public AudioSource[] MusicAudioSources;
    
    [Header("Boss Music Audio source")]
    public AudioSource BossMusicAudioSources;

    public bool PlayMusic;

    [Header("Audio Distortion")]
    public bool Audio_Distortion;

    int rand_music;

    public float[] MusicTime;
    public bool endMusic;

    bool isMusicBoss;
    public GameObject boss;

    float tmpmusictime;
    float addmusictime;
    bool addmusicdone;
    void Start()
    {
        var temp = GameObject.FindGameObjectsWithTag("Audio");
        AudioSources = new AudioSource[temp.Length];

        for (int i = 0; i < AudioSources.Length; i++)
        {
            AudioSources[i] = temp[i].GetComponent<AudioSource>();
        }

        Timemanager = GameObject.Find("TimeManager");
        timeStop = Timemanager.GetComponent<TimeStop>();
        timeBody = Timemanager.GetComponent<TimeBody>();
        timeManager = Timemanager.GetComponent<TimeManager>();

        General_slider.maxValue = 100f;
        Music_slider.maxValue = 100f;
        SFX_slider.maxValue = 100f;
        Voice_slider.maxValue = 100f;

        UpdateText();

        rand_music = Random.Range(0, MusicAudioSources.Length);
        //Debug.Log(rand_music);
        tmpmusictime = MusicTime[rand_music];
        endMusic = true;
        addmusictime = 0;
        //StartCoroutine(startmusic());
    }

    void Update()
    {
        ///Debug
        General_Sound_d = General_Sound;
        Music_Sound_d = Music_Sound;
        SFX_Sound_d = SFX_Sound;
        Voice_Sound_d = Voice_Sound;
        ///Fine debug

        search_audio_source();

        if(boss == null)
        {
            boss = GameObject.Find("Boss1(Clone)");
        }

        if(boss != null && !isMusicBoss)
        {
            for(int i = 0; i < MusicAudioSources.Length; i++)
            {
                MusicAudioSources[i].gameObject.SetActive(false);
            }
            BossMusicAudioSources.Play();
            isMusicBoss = true;
        }

        for (int i = 0; i < MusicAudioSources.Length; i++)
        {
            MusicAudioSources[i].volume = (Music_Sound / 1000)*(General_Sound / 10);
            BossMusicAudioSources.volume = (Music_Sound / 1000) * (General_Sound / 10);
        }

        General_Sound = General_slider.value;
        Music_Sound = Music_slider.value;
        SFX_Sound = SFX_slider.value;
        Voice_Sound = Voice_slider.value;

        if (!addmusicdone && (timeBody.stato || timeStop.stato))
        {
            addmusictime += 5.0f;
            addmusicdone = true;
        }
        else if(!timeBody.stato && !timeStop.stato)
        {
            addmusicdone = false;
        }

        if (Audio_Distortion)
        {
            AudioDistortion_general();
        }

        UpdateText();
        if (endMusic && !timeManager.slowtimeactive && !timeBody.stato && !timeStop.stato)
        {
            StartCoroutine(start_music());
        }
    }

    public void Audio_distorce()
    {
        Audio_Distortion = !Audio_Distortion;
    }

    public void search_audio_source()
    {
        var temp = GameObject.FindGameObjectsWithTag("Audio");
        AudioSources = new AudioSource[temp.Length];

        for (int i = 0; i < AudioSources.Length; i++)
        {
            AudioSources[i] = temp[i].GetComponent<AudioSource>();
        }
    }

    void UpdateText()
    {
        General_text.text = General_slider.value.ToString();
        Music_text.text = Music_slider.value.ToString();
        SFX_text.text = SFX_slider.value.ToString();
        Voice_text.text = Voice_slider.value.ToString();
    }

    void AudioDistortion_general()
    {

        ///Audio armi
        if (timeManager.slowtimeactive && !timeBody.stato && !timeStop.stato)
        {
            for (int i = 0; i < AudioSources.Length; i++)
            {
                AudioSources[i].pitch = 0.25f;
            }
        }
        else if (!timeManager.slowtimeactive && timeBody.stato && !timeStop.stato)
        {
            for (int i = 0; i < AudioSources.Length; i++)
            {
                AudioSources[i].pitch = -1.0f;
            }
        }
        else if (!timeManager.slowtimeactive && !timeBody.stato && timeStop.stato)
        {
            for (int i = 0; i < AudioSources.Length; i++)
            {
                AudioSources[i].pitch = 1.0f;
            }

            if (!isMusicBoss)
            {
                MusicAudioSources[rand_music].Pause();
            }
            else
            {
                MusicAudioSources[rand_music].Pause();
                BossMusicAudioSources.Pause();
            }
            

            StartCoroutine(stopmusic());
        }
        else if (!timeManager.slowtimeactive && !timeBody.stato && !timeStop.stato)
        {
            for (int i = 0; i < AudioSources.Length; i++)
            {
                AudioSources[i].pitch = 1.0f;
            }

            if (!isMusicBoss)
            {
                if (!PlayMusic && !endMusic)
                {
                    MusicAudioSources[rand_music].Play();
                    PlayMusic = true;
                }
            }
            else
            {
                if (!PlayMusic)
                {
                    MusicAudioSources[rand_music].Pause();
                    BossMusicAudioSources.Play();
                    PlayMusic = true;
                }
            }
        }
    }

    IEnumerator start_music()
    {
        endMusic = false;
        rand_music = Random.Range(0, MusicAudioSources.Length);
        //Debug.Log(rand_music);
        if (!isMusicBoss)
        {
            MusicAudioSources[rand_music].Play();
        }
        else
        {
            MusicAudioSources[rand_music].Pause();
            BossMusicAudioSources.Play();
        }
        yield return new WaitForSeconds(MusicTime[rand_music]);
        yield return new WaitForSeconds(addmusictime);
        addmusictime = 0;
        endMusic = true;
    }

    IEnumerator stopmusic()
    {
        PlayMusic = false;
        yield return new WaitForSeconds(5);
        PlayMusic = true;
    }
}
