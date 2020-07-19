using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarController : MonoBehaviour
{
    public PlayerController Player;
    public Image _backGrondImage;

    private float _maxHealth;
    private float _nowHealth;
    private float _minHealth = 0;
    
    void Start()
    {
        _maxHealth = Player.Health;
        _nowHealth = Player.Health;
    }

    // Update is called once per frame
    void Update()
    {
        if (_nowHealth != Player.Health)
        {
            _nowHealth = Player.Health;
            _backGrondImage.fillAmount = _nowHealth / 100;
        }
    }
}
