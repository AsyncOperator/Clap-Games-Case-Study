using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public sealed class UIManager : MonoBehaviour {
    [SerializeField, Range( 0f, 3f )] private float waitTimeUntilDisplayLoseScreen;
    [SerializeField, Range( 0f, 3f )] private float waitTimeUntilDisplayWinScreen;

    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    [SerializeField] private Button retryButton;
    [SerializeField] private Button nextLevelButton;

    private void OnEnable() => GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;

    private void OnDisable() => GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;

    private void Start() {
        retryButton.onClick.AddListener( () => {
            loseScreen.SetActive( false );
            LevelManager.Instance.RestartLevel();
        } );
        nextLevelButton.onClick.AddListener( () => {
            winScreen.SetActive( false );
            LevelManager.Instance.TryLoadNextLevel();
        } );
    }

    private void GameManager_OnGameStateChanged( GameManager.GameState currentState ) {
        if ( ( currentState & GameManager.GameState.Lose ) != 0 ) {
            _ = StartCoroutine( DisplayLoseScreen() );
        }
        else if ( ( currentState & GameManager.GameState.Win ) != 0 ) {
            _ = StartCoroutine( DisplayWinScreen() );
        }
    }

    private IEnumerator DisplayLoseScreen() {
        yield return new WaitForSeconds( waitTimeUntilDisplayLoseScreen );
        loseScreen.SetActive( true );
    }

    private IEnumerator DisplayWinScreen() {
        yield return new WaitForSeconds( waitTimeUntilDisplayWinScreen );
        winScreen.SetActive( true );
    }
}