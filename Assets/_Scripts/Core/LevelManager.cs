using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class LevelManager : Singleton<LevelManager> {
    [SerializeField] private GameObject loadingScreen;

    public static event Action OnSceneLoaded;

    public string GetLevelName() => SceneManager.GetActiveScene().name;

    public void RestartLevel() => StartCoroutine( LoadAsynchronouly( SceneManager.GetActiveScene().buildIndex ) );

    /// <summary>
    /// If the current scene is not the last one in settings it will load next level otherwise load first scene
    /// </summary>
    public void TryLoadNextLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalSceneCount = SceneManager.sceneCountInBuildSettings;

        int sceneIndexToLoad = currentSceneIndex <= totalSceneCount - 1 ? currentSceneIndex++ : 0;
        StartCoroutine( LoadAsynchronouly( sceneIndexToLoad ) );
    }

    private IEnumerator LoadAsynchronouly( int sceneIndex ) {
        AsyncOperation operation = SceneManager.LoadSceneAsync( sceneIndex );

        loadingScreen.SetActive( true );
        //operation.allowSceneActivation = false;


        yield return new WaitUntil( () => operation.isDone );
        //while ( !operation.isDone ) {
        //    yield return null;
        //}

        //operation.allowSceneActivation = true;
        loadingScreen.SetActive( false );

        OnSceneLoaded?.Invoke();
    }
}