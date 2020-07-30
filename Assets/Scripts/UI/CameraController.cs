using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public float UpBound;
    [SerializeField] public float DownBound;
    [SerializeField] public float LeftBound;
    [SerializeField] public float RightBound;

    public float Dumping = 1.5f;
    public Vector2 Offset = new Vector2(2f, 1f);
    public bool IsLeft;
    public GameObject MainObject;
     
    private Transform _player;
    private int _lastX;

    void Start()
    {
        _player = MainObject.transform;
        Offset = new Vector2(Mathf.Abs(Offset.x), Offset.y);
        FindPlayer(IsLeft);
    }

    // Update is called once per frame
    void Update()
    {
        if (_player)
        {
            int currentX = Mathf.RoundToInt(_player.position.x);
            if (currentX > _lastX)
                IsLeft = false;
            else if (currentX < _lastX)
                IsLeft = true;
            _lastX = Mathf.RoundToInt(_player.position.x);

            Vector3 target;

            if (IsLeft)
            {
                target = new Vector3(_player.position.x - Offset.x, _player.position.y + Offset.y,
                    transform.position.z);
            }
            else
            {
                target = new Vector3(_player.position.x + Offset.x, _player.position.y + Offset.y,
                    transform.position.z);
            }

            Vector3 currentPosition = Vector3.Lerp(transform.position, target, Dumping * Time.deltaTime);
            transform.position = currentPosition;
        }
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, LeftBound, RightBound),
            Mathf.Clamp(transform.position.y, DownBound, UpBound),
            transform.position.z
            );
    }

    public void FindPlayer(bool playerIsLeft)
    {
        _lastX = Mathf.RoundToInt(_player.position.x);
        if (playerIsLeft)
        {
            transform.position = new Vector3(_player.position.x - Offset.x, _player.position.y + Offset.y,
                transform.position.z);
        }
        else
        {
            transform.position = new Vector3(_player.position.x + Offset.x, _player.position.y + Offset.y,
                transform.position.z);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(LeftBound, UpBound), new Vector2(RightBound, UpBound));
        Gizmos.DrawLine(new Vector2(LeftBound, DownBound), new Vector2(RightBound, DownBound));
        Gizmos.DrawLine(new Vector2(LeftBound, UpBound), new Vector2(LeftBound, DownBound));
        Gizmos.DrawLine(new Vector2(RightBound, UpBound), new Vector2(RightBound, DownBound));
    }
}