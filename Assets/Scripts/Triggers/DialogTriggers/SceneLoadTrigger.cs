using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadTrigger : MonoBehaviour
{
    private SceneController _sceneController;
    void Start()
    {
        _sceneController = GetComponent<SceneController>();
    }
    void FixedUpdate()
    {
    }
    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.name == "Player")
        {
            _sceneController.LoadLevel();
        }
    }
}
