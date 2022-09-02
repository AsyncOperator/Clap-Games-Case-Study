using System;
using UnityEngine;

public sealed class GameManager : Singleton<GameManager> {
    private int numberOfCollectedCoins;

    [Tooltip( "Debug purposes for developer" )]
    [field: SerializeField, ReadOnly] public GameState CurrentState { get; private set; } = GameState.None;
    
    public enum GameState {
        None = 0,
        TapToStart = 1,
        Ongoing = 2,
        Lose = 4,
        Win = 8
    }

    public event Action<int> OnNumberOfCollectedCoinsChanged;
    public static event Action<GameState> OnGameStateChanged;

    private void OnEnable() {
        LevelManager.OnSceneLoaded += OnSceneChanged;
    }

    private void OnDisable() {
        LevelManager.OnSceneLoaded += OnSceneChanged;
    }

    private void Start() => ChangeGameState( GameState.TapToStart );

    public void ChangeGameState( GameState state ) {
        if ( state == CurrentState ) {
            Debug.LogWarning( $"GameManager is already in { state.ToString() } state" );
            return;
        }

        CurrentState = state;
        OnGameStateChanged?.Invoke( CurrentState );
    }

    public void IncreaseNumberOfCollectedCoins() {
        numberOfCollectedCoins++;
        OnNumberOfCollectedCoinsChanged?.Invoke( numberOfCollectedCoins );
    }

    private void OnSceneChanged() {
        numberOfCollectedCoins = 0;
        ChangeGameState( GameState.TapToStart );
    }
}