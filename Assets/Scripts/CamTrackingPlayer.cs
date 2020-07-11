using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrackingPlayer : MonoBehaviour
{
    private PlayerController _player;
    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, -10f);
    }

    void FixedUpdate()
    {
       
    }
}
