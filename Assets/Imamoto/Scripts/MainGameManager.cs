using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    public enum GameState
    {
        None = 0,
        Title = 1,
        MainGameStart = 2,
        MainGameFisnih = 3,
    }

    public void GameStart()
    {
        _state = GameState.MainGameStart;
    }

    public void GameFisnih()
    {
        _state = GameState.MainGameFisnih;
    }

    // Start is called before the first frame update
    void Start()
    {
        _state = GameState.Title;
    }

    private GameState _state;
}
