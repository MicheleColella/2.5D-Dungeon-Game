using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;
using System.Collections.Generic;

public class VolumeActive : MonoBehaviour
{
    public Dash_ghost dash_Ghost;
    public float cooldownt= 1.0f;
    public float cooldownt2 = 5.0f;

    private Volume volume;
    private ChromaticAberration chromaticAberration;
    private ColorAdjustments colorAdjustments;
    private Bloom bloom;
    private Tonemapping tonemapping;
    private Vignette vignette;
    public GameObject Blitz;

    public float minimum = 0.0f;
    public float maximum = 1.0f;

    public float minimum2 = 0.0f;
    public float maximum2 = -100.0f;

    int cont = 0;
    int cont2 = 0;

    private bool attivo;
    bool canAbility1 = true;

    private bool attivo2;
    bool canAbility2 = true;

    bool canAbility3 = true;

    // starting value for the Lerp
    static float t = 0.0f;
    static float t2 = 0.0f;

    bool Ability1 = true;
    bool Ability2 = true;

    public GameObject Timemanager;
    public TimeStop timeStop;
    public TimeBody timeBody;
    public TimeManager timeManager;

    public PlayerManager playerManager;
    public GameObject player;

    void Start()
    {
        player = GameObject.Find("player_controller");
        playerManager = player.GetComponent<PlayerManager>();
        dash_Ghost = player.GetComponentInChildren<Dash_ghost>();

        Timemanager = GameObject.Find("TimeManager");
        timeStop = Timemanager.GetComponent<TimeStop>();
        timeBody = Timemanager.GetComponent<TimeBody>();
        timeManager = Timemanager.GetComponent<TimeManager>();

        StartCoroutine(cooldown());
        StartCoroutine(cooldown2());
        StartCoroutine(cooldown3());
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out chromaticAberration);
        volume.profile.TryGet(out colorAdjustments);
        volume.profile.TryGet(out bloom);
        volume.profile.TryGet(out tonemapping);
        volume.profile.TryGet(out vignette);
        colorAdjustments.saturation.value = 0.0f;
        chromaticAberration.intensity.value = 0.0f;
    }

    void Update()
    {
        float y = Input.GetAxis("Fire3");
        float x = Input.GetAxis("Fire4");

        if (!playerManager.isGameOver)
        {
            //Abilità 1----------------------------------------------------------------------------------
            if (!timeStop.stato && !timeBody.stato)
            {
                if ((Input.GetKeyDown(KeyCode.Alpha1) || y == 1) && canAbility1 == true)
                {
                    dash_Ghost.ActiveGhost(1f);
                    attivo = true;
                    StartCoroutine(cooldown());
                    StartCoroutine(coolab());
                }
            }


            float val = Mathf.Lerp(minimum, maximum, t);
            chromaticAberration.intensity.value = val;
            //Debug.Log(val);

            if (attivo)
                t += 5f * Time.deltaTime;

            if (Ability1)
            {
                if (t > 1.0f)
                {
                    cont += 1;
                    //Debug.Log(cont);
                    float temp = maximum;
                    maximum = minimum;
                    minimum = temp;
                    t = 0.0f;
                    if (cont == 2)
                    {
                        attivo = false;
                        cont = 0;
                    }
                }
                //Debug.Log(nextTime);
            }

            //Abilità 2----------------------------------------------------------------------------------

            if (!timeBody.stato && !timeManager.slowtimeactive)
            {
                if ((Input.GetKeyDown(KeyCode.Alpha3) || x == -1) && canAbility2 == true)
                {
                    dash_Ghost.ActiveGhost(5f);
                    attivo2 = true;
                    StartCoroutine(cooldown2start());
                    StartCoroutine(coolab2());
                }
            }

            float val2 = Mathf.Lerp(minimum2, maximum2, t2);
            colorAdjustments.saturation.value = val2;
            //Debug.Log(val);

            if (attivo2)
                t2 += 5f * Time.deltaTime;

            if (Ability2)
            {
                if (t2 > 1.0f)
                {
                    cont2 += 1;
                    //Debug.Log(cont);
                    float temp2 = maximum2;
                    maximum2 = minimum2;
                    minimum2 = temp2;
                    t2 = 0.0f;
                    if (cont2 == 2)
                    {
                        attivo2 = false;
                        cont2 = 0;
                    }
                }
                //Debug.Log(nextTime);
            }

            //Abilità 3----------------------------------------------------------------------------------

            if (!timeStop.stato && !timeManager.slowtimeactive)
            {
                if ((Input.GetKeyDown(KeyCode.Alpha2) || y == -1) && canAbility3 == true)
                {
                    dash_Ghost.ActiveGhost(5f);
                    StartCoroutine(cooldown3start());
                    StartCoroutine(coolab3());
                }
            }
        }
    }

    private IEnumerator coolab()
    {
        Ability1 = false;
        //Debug.Log(Ability1);
        yield return new WaitForSeconds(1);
        Ability1 = true;
        //Debug.Log(Ability1);
    }

    private IEnumerator coolab2()
    {
        Ability2 = false;
        //Debug.Log(Ability2);
        yield return new WaitForSeconds(5);
        Ability2 = true;
        //Debug.Log(Ability2);
    }

    private IEnumerator coolab3()
    {
        Blitz.SetActive(true);
        //Debug.Log(Ability2);
        yield return new WaitForSeconds(5);
        Blitz.SetActive(false);
        //Debug.Log(Ability2);
    }

    private IEnumerator cooldown()
    {
        canAbility1 = false;
        //Debug.Log(canAbility1);
        yield return new WaitForSeconds(15);
        canAbility1 = true;
        //Debug.Log(canAbility1);
    }

    private IEnumerator cooldown2()
    {
        canAbility2 = false;
        //Debug.Log(canAbility2);
        yield return new WaitForSeconds(15);
        canAbility2 = true;
        //Debug.Log(canAbility2);
    }
    
    private IEnumerator cooldown2start()
    {
        canAbility2 = false;
        //Debug.Log(canAbility2);
        yield return new WaitForSeconds(20);
        canAbility2 = true;
        //Debug.Log(canAbility2);
    }

    private IEnumerator cooldown3()
    {
        canAbility3 = false;
        //Debug.Log(canAbility3);
        yield return new WaitForSeconds(15);
        canAbility3 = true;
        //Debug.Log(canAbility3);
    }
    private IEnumerator cooldown3start()
    {
        canAbility3 = false;
        //Debug.Log(canAbility3);
        yield return new WaitForSeconds(20);
        canAbility3 = true;
        //Debug.Log(canAbility3);
    }

    ///Graphics setting
    
    public void LowGraph()
    {
        Debug.Log("Low");
        bloom.intensity.value = 0.0f;
        bloom.highQualityFiltering.value = false;
        colorAdjustments.contrast.value = 0.0f;
        tonemapping.mode.value = 0;
        vignette.intensity.value = 0.0f; 
    }
    public void MedGraph()
    {
        Debug.Log("Medium");
        bloom.intensity.value = 2.02f;
        bloom.highQualityFiltering.value = false;
        colorAdjustments.contrast.value = 7.95f;
        tonemapping.mode.value = (TonemappingMode)2;
        vignette.intensity.value = 0.15f;
    }
    public void HighGraph()
    {
        Debug.Log("High");
        bloom.intensity.value = 5.02f;
        bloom.highQualityFiltering.value = true;
        colorAdjustments.contrast.value = 15.9f;
        tonemapping.mode.value = (TonemappingMode)2;
        vignette.intensity.value = 0.25f;
    }
    public void FanGraph()
    {
        Debug.Log("Fantastic");
        bloom.intensity.value = 7.02f;
        bloom.highQualityFiltering.value = true;
        colorAdjustments.contrast.value = 15.9f;
        tonemapping.mode.value = (TonemappingMode)2;
        vignette.intensity.value = 0.25f;
    }
}
