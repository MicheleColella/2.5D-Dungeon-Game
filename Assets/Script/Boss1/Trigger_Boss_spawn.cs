using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Boss_spawn : MonoBehaviour
{
    public bool triggered;

    [Header("Boss")]
    public GameObject Boss;
    public GameObject[] Spawner_Type_1;

    public int waitcamMove;
    private CameraBossSwitcher cameraBossSwitcher;

    private void Start()
    {
        cameraBossSwitcher = GameObject.Find("CM StateDrivenCamera1").GetComponent<CameraBossSwitcher>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            StartCoroutine(MoveCam());
            if (other.gameObject.tag == "Player")
            {
                Instantiate(Boss, Spawner_Type_1[0].transform.position, Quaternion.identity);
                triggered = true;
            }
        }
    }

    IEnumerator MoveCam()
    {
        cameraBossSwitcher.SwitchState();
        yield return new WaitForSeconds(waitcamMove);
        cameraBossSwitcher.SwitchState();
    }
}
