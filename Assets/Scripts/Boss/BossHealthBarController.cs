using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBarController : MonoBehaviour
{
    public AbstructBoss Boss;
    public Image _backGrondImage;

    private float _maxHealth;
    private float _nowHealth;
    private float _minHealth = 0;
    void Start()
    {
        _maxHealth = Boss.Health;
        _nowHealth = Boss.Health;
    }

    // Update is called once per frame
    void Update()
    {
        if (_nowHealth != Boss.Health)
        {
            _nowHealth = Boss.Health;
            _backGrondImage.fillAmount = _nowHealth / 100;
        }
    }
}
