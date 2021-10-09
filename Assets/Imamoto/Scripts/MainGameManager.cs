using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : SingletonMonoBehaviour<MainGameManager>
{
    protected override void Awake()
    {
        base.Awake();
        _state = GameState.Title;
    }

    public enum GameState
    {
        Title = 0,
        MainGame = 1,
        MainGameClear = 2,
        MainGameFailed = 3,
    }

    public void GameStart()
    {
        _state = GameState.MainGame;
    }

    public void GameClear()
    {
        _state = GameState.MainGameClear;
        Debug.Log("GAME CLEAR !!!!!");
    }

    private GameState _state;
}
