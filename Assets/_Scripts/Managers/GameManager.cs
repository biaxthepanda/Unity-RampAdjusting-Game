using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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


    public void GameOverWithWin()
    {
        Debug.LogAssertion("GAME OVER YOU WIN!");
    }

    public void GameOver()
    {
        Debug.LogAssertion("GAME OVER");
    }
}
