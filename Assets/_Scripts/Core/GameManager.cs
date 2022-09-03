using System;
using UnityEngine;

public sealed class GameManager : Singleton<GameManager> {

    [Tooltip( "Debug purposes for developer" )]
    [field: SerializeField, ReadOnly] public GameState CurrentState { get; private set; } = GameState.None;
    
    public enum GameState {
        None = 0,
        TapToStart = 1,
        Ongoing = 2,
        Lose = 4,
        Win = 8
    }

    private int numberOfCollectedCoins;

    public event Action<int> OnNumberOfCollectedCoinsChanged;
    public static event Action<GameState> OnGameStateChanged;

    private void OnEnable() => LevelManager.OnSceneLoaded += OnSceneChanged;

    private void OnDisable() => LevelManager.OnSceneLoaded += OnSceneChanged;

    private void Start() => ChangeGameState( GameState.TapToStart );

    public void ChangeGameState( GameState state ) {
        if ( state == CurrentState ) {
#if UNITY_EDITOR
            Debug.LogWarning( $"GameManager is already in { state.ToString() } state" );
#endif
            return;
        }

        CurrentState = state;
        OnGameStateChanged?.Invoke( CurrentState );
    }

    public void IncreaseNumberOfCollectedCoins() => OnNumberOfCollectedCoinsChanged?.Invoke( numberOfCollectedCoins++ );

    private void OnSceneChanged() {
        numberOfCollectedCoins = 0; // Reset collected coins
        ChangeGameState( GameState.TapToStart );
    }
}