using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrackingPlayer : MonoBehaviour
{
    [SerializeField]
    public float UpBound;
    [SerializeField]
    public float DownBound;
    [SerializeField]
    public float LeftBound;
    [SerializeField]
    public float RightBound;

    private PlayerController _player;
    private Camera _camera;
    void Start()
    {
        _camera = GetComponent<Camera>();
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
