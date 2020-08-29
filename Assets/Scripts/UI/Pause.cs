using System;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Pause : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private GameObject _pauseContent;
    [SerializeField] private Menu _menu;
    [SerializeField] private float _verticalBound = 5f;
    [SerializeField] private float _horizontalBound = 15f;

    private List<IEnemy> _charsOnPauseScreen = new List<IEnemy>();
    private bool _isOnPause = false;

    private void Start()
    {
        if (_player == null)
            _player = FindObjectOfType<PlayerController>();
        if(_pauseContent)
            _pauseContent.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && !_isOnPause)
            StartPause();
        else if (Input.GetButtonDown("Cancel") && _isOnPause)
            StopPause();

    }

    public void StartPause()
    {
        _isOnPause = true;
        
        GetAllObjectsOnPauseScreen();
        StartPauseAllCharsOnScreen();

        _pauseContent.SetActive(true);
    }

    public void StopPause()
    {
        _isOnPause = false;
        
        StopPauseAllCharsOnScreen();
        _charsOnPauseScreen.Clear();
        
        _pauseContent.SetActive(false);
    }

    public void StartMenu()
    {
        if(_pauseContent)
            _pauseContent.SetActive(false);
        
        _menu.StartMenu();
        
        StartPauseAllCharsOnScreen();
    }
    
    public void StopMenu()
    {
        if(_pauseContent)
            _pauseContent.SetActive(false);
        
        _menu.Resume();
        
        StopPauseAllCharsOnScreen();
    }

  
    private void GetAllObjectsOnPauseScreen()
    {
        var boxSize = new Vector2(_horizontalBound, _verticalBound);
        var boxCenter = _player.transform.position;
        Collider2D[] chars = Physics2D.OverlapBoxAll(boxCenter, boxSize, 0f);
        
        foreach (var obj in chars)
        {
            if(obj.GetComponent<IEnemy>() != null)
                _charsOnPauseScreen.Add(obj.GetComponent<IEnemy>());
        }
    }

    private void StartPauseAllCharsOnScreen()
    {
        _player.StartStand();
        
        foreach (var chr in _charsOnPauseScreen)
        {
            chr.StartPause();
        }
    }
    
    private void StopPauseAllCharsOnScreen()
    {
        _player.StopStand();
        
        foreach (var chr in _charsOnPauseScreen)
        {
            chr.StopPause();
        }
    }
}
