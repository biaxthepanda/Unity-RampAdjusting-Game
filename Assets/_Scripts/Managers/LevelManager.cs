using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    
    public Level CurrentLevel;
    private GameObject  _currentLevelObject;
    [SerializeField]private int _levelIndex = -1;
    public Level[] Levels;

    private bool _isRestarting = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
        
    }

    private void Start()
    {
        //NextLevel(); //Delete Later
    }

    public void StartGame()
    {
        NextLevel();
    }

    public void NextLevel()
    {
        _levelIndex++;
        UIManager.Instance.UpdateLevelText(_levelIndex+1);

        if (_currentLevelObject != null) Destroy(_currentLevelObject);
        if (_levelIndex > Levels.Length - 1)
        {
            GameManager.Instance.GameOverWithWin(); return;
        }

        CurrentLevel = Instantiate(Levels[_levelIndex], transform.position, Quaternion.identity);
        _currentLevelObject = CurrentLevel.gameObject;
    }

    public void RestartLevel()
    {
        _isRestarting = false;
        if (_currentLevelObject != null) Destroy(_currentLevelObject);

        CurrentLevel = Instantiate(Levels[_levelIndex], transform.position, Quaternion.identity);
        _currentLevelObject = CurrentLevel.gameObject;
    }

    public void StartLevel()
    {
        CurrentLevel.StartLevel();
    }

    public void PlayerLostLevel()
    {
        if (_isRestarting) return;
        _isRestarting = true;
        Invoke("RestartLevel",1f);
    }

}
