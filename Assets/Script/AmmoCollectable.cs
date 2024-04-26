using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollectable : MonoBehaviour
{
    public Material[] material;
    public bool RandAmount;
    public bool RandType;
    public int ammoToAdd;
    public int ammoType;

    public int randminammoToAdd;
    public int randmaxammoToAdd;

    public GameObject Bullet;
    void Start()
    {
        if (RandAmount)
        {
            ammoToAdd = Random.Range(randminammoToAdd, randmaxammoToAdd);
        }
        
        if (RandType)
        {
            ammoType = Random.Range(1, 6);
        }

        Bullet.GetComponent<Renderer>().material = material[ammoType - 1];
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject colGo = other.gameObject;
        if(colGo.tag == "Player")
        {
            switch (ammoType) 
            {
                case 1:
                    colGo.GetComponent<Weapon>().ammoWeapon1 += ammoToAdd;
                    break;
                case 2:
                    colGo.GetComponent<Weapon>().ammoWeapon2 += ammoToAdd;
                    break;
                case 3:
                    colGo.GetComponent<Weapon>().ammoWeapon3 += ammoToAdd;
                    break;
                case 4:
                    colGo.GetComponent<Weapon>().ammoWeapon4 += ammoToAdd;
                    break;
                case 5:
                    colGo.GetComponent<Weapon>().ammoWeapon4 += ammoToAdd;
                    break;
                case 6:
                    colGo.GetComponent<Weapon>().ammoWeapon5 += ammoToAdd;
                    break;
                default:
                    Debug.Log("Ammo out of scale");
                    break;
            }
            CameraShake1.Instance.ShakeCamera(0.2f, 0.2f, 0f);
            Destroy(gameObject);
        }
    }
}
