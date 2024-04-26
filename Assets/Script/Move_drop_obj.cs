using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_drop_obj : MonoBehaviour
{
    public Transform Target;
    public float MinModifier;
    public float MaxModifier;

    Vector3 _velocity = Vector3.zero;
    public bool _isFollowing = false;
    void Start()
    {
        Target = GameObject.Find("player_controller").GetComponent<Transform>();
    }

    public void StartFollowing()
    {
        _isFollowing = true;
    }

    void Update()
    {
        if (_isFollowing)
        {
            transform.position = Vector3.SmoothDamp(transform.position, Target.position, ref _velocity, Time.deltaTime * Random.Range(MinModifier, MaxModifier));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
