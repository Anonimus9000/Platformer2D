using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _menuContent;
    [SerializeField] private PlayerController _player;
    [SerializeField] private float _verticalBound = 5f;
    [SerializeField] private float _horizontalBound = 15f;
    
    private bool _isActive;
    private List<IEnemy> _charsOnPauseScreen = new List<IEnemy>();
    private bool _isOnPause = false;

    private void Start()
    {
        if (_player != null)
            _player = FindObjectOfType<PlayerController>();
    }

    public void StartMenu()
    {
        _isActive = true;
        _menuContent.SetActive(true);
        StartPauseAllCharsOnScreen();
    }
    
    public void Resume()
    {
        _isActive = false;
        _menuContent.SetActive(false);
        StopPauseAllCharsOnScreen();
    }
    
    public void ExitGame()
    {
        _isActive = false;
        Application.Quit();
    }

    public bool IsActive()
    {
        return _isActive;
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
