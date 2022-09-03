using UnityEngine;
using TMPro;

public sealed class UISceneNameDisplayer : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI sceneNameText;

    private void Start() => sceneNameText.SetText( LevelManager.Instance.GetLevelName() );
}