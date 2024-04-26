using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash_ghost : MonoBehaviour
{
    public GameObject Sprites;
    public float ghostDelay;
    private float ghostDelaySeconds;
    public GameObject Ghost;
    public bool makeGhost;


    void Start()
    {
        ghostDelaySeconds = ghostDelay;
    }

    
    void Update()
    {
        if (makeGhost)
        {
            if (ghostDelaySeconds > 0)
            {
                ghostDelaySeconds -= Time.deltaTime;
            }
            else
            {
                GameObject CurrentGhost = Instantiate(Ghost, transform.position, transform.rotation);
                Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
                CurrentGhost.transform.localScale = new Vector3(Sprites.transform.localScale.x * this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
                CurrentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;
                ghostDelaySeconds = ghostDelay;
                Destroy(CurrentGhost, 1f);
            }
        }
    }

    public void ActiveGhost(float time)
    {
        StartCoroutine(TimeGhost(time));
    }

    IEnumerator TimeGhost(float time)
    {
        makeGhost = true;
        yield return new WaitForSeconds(time);
        makeGhost = false;
    }
}
