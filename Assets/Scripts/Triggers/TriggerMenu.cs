using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMenu : MonoBehaviour
{
    [SerializeField] private Menu _menu;
    
    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.name == "Player")
        {
            _menu.StartMenu();
            Destroy(gameObject);
        }
    }
}
