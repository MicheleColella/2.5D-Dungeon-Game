using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visual_map : MonoBehaviour
{
    private bool active_zoom_map;

    public GameObject principal_map;
    public GameObject zoom_map;

    private bool canZoom;



    void Start()
    {
        canZoom = true;
        active_zoom_map = false;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Fire4");
        if(Input.GetKeyDown(KeyCode.V))
        {
            active_zoom_map = !active_zoom_map;
            zoom_map.SetActive(active_zoom_map);
            principal_map.SetActive(!active_zoom_map);
        }

        if(x == 1 && canZoom)
        {
            active_zoom_map = !active_zoom_map;
            zoom_map.SetActive(active_zoom_map);
            principal_map.SetActive(!active_zoom_map);
            StartCoroutine(cooldown2());
        }
    }

    private IEnumerator cooldown2()
    {
        canZoom = false;
        //Debug.Log(canAbility2);
        yield return new WaitForSeconds(0.5f);
        canZoom = true;
        //Debug.Log(canAbility2);
    }
}
