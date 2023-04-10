using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
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


    [SerializeField] TMP_Text _levelText;
    [SerializeField] GameObject _canvasGame;
    [SerializeField] GameObject _canvasMenu;


    public void StartLevelButton()
    {
        LevelManager.Instance.StartLevel();
    }
    public void RestartLevelButton()
    {
        LevelManager.Instance.RestartLevel();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void UpdateLevelText(int level)
    {
        _levelText.text = level.ToString();
    }

    public void HideMenuShowGame()
    {
        _canvasGame.SetActive(true);
        _canvasMenu.SetActive(false);
        LevelManager.Instance.StartGame();
    }
}
