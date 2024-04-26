using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite_camera_rotation : MonoBehaviour
{
    public float direction_fire = 1;

    [Header("Rotazione Y")]
    private Camera thecam;
    private bool useStaticBillboard = true;
    SpriteRenderer sprite;
    public Transform enemy;
    bool _facingRight;

    [Header("Rotazione Z")]
    RaycastHit hit;
    public Transform Sprites;
    public LayerMask layerMask;
    public float distance;
    public float Zrot;
    public float actual_rot;


    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        thecam = Camera.main;
    }

    private void LateUpdate()
    {
        if (!useStaticBillboard)
        {
            transform.LookAt(thecam.transform);
        }
        else
        {
            transform.rotation = thecam.transform.rotation;
        }

        /*
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, distance, layerMask))
        {
            Zrot = hit.collider.transform.rotation.eulerAngles.z;
            float rotz;

            ///Legge dell'inclinazione inventata da ME
            if (Zrot > 70 && Zrot < 90)
            {
                rotz = (-((Zrot / 2) - (Zrot - (90 / 2)))) * 2;
            }
            else if(Zrot > 90)
            {
                rotz = (-((Zrot / 2) - (Zrot - (90 / 2)))) * 2;
            }
            else
            {
                rotz = -(Zrot / 2.1f);
            }

            actual_rot = rotz;
            gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.eulerAngles.x, gameObject.transform.rotation.eulerAngles.y, rotz);
        }
        */
    }

    
    void Update()
    {
        //Debug.Log(enemy.rotation.eulerAngles.y);

        /*
        if (((enemy.rotation.eulerAngles.y <= 180 && enemy.rotation.eulerAngles.y >= 0) || (enemy.rotation.eulerAngles.y <= -180 && enemy.rotation.eulerAngles.y >= 0)) && _facingRight)
        {
            Flip();
        }

        if (((enemy.rotation.eulerAngles.y <= 360 && enemy.rotation.eulerAngles.y >= 180) || (enemy.rotation.eulerAngles.y <= -360 && enemy.rotation.eulerAngles.y >= -180)) && !_facingRight)
        {
            Flip();
        }
        */
        if ((enemy.rotation.eulerAngles.y <= 240 && enemy.rotation.eulerAngles.y >= 0) && _facingRight)
        {
            Flip();
            //Debug.Log("Destra");
        }

        if ((enemy.rotation.eulerAngles.y <= 360 && enemy.rotation.eulerAngles.y >= 140) && !_facingRight)
        {
            Flip();
            //Debug.Log("Sinistra");
        }
    }

    void Flip()
    {
        //flips the character along the x axis using the sprite renderer correspondingly
        gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;

        //Sets the facingright bool to the opposite of what its currently at to prevent repeated flipping
        _facingRight = !_facingRight;
        direction_fire = -direction_fire;
        //Flips the character by Rotating the character 180 degrees along the y axis
        //gameObject.GetComponent<Rigidbody2D>().transform.Rotate(0, 180, 0);

        //Flips the character by the changing the scale of the x axis to 1 or minus 1
        //gameObject.GetComponent<Rigidbody2D>().transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
    }

    /*
    private void OnDrawGizmos()
    {
        //Centro
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * distance);
    }
    */

}
