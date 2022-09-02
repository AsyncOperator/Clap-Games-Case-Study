using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent( typeof( Image ) )]
public sealed class UITapToStart : MonoBehaviour, IPointerClickHandler {
    [SerializeField] private Image transparentPanel;
    [SerializeField] private TextMeshProUGUI animatedText;
    [Tooltip( "Specifies how long it takes to complete one loop at FadeInOut coroutine" )]
    [SerializeField, Range( 0.1f, 5f )] private float animationDuration;

    [Tooltip( "Debug purposes for developer - its ( 1 / animationDuration )" )]
    [SerializeField, ReadOnly]
    private float animationSpeed;

    private void OnValidate() => animationSpeed = Mathf.Pow( animationDuration, -1f );

    private void Awake() => OnValidate();

    private void OnEnable() => GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;

    private void OnDisable() => GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;

    void IPointerClickHandler.OnPointerClick( PointerEventData eventData ) {
        transparentPanel.raycastTarget = false;
        animatedText.enabled = false;
        StopAllCoroutines();

        GameManager.Instance.ChangeGameState( GameManager.GameState.Ongoing );
    }

    private void GameManager_OnGameStateChanged( GameManager.GameState currentState ) {
        if ( ( currentState & GameManager.GameState.TapToStart ) != 0 ) {
            FadeInOutTextTransparency();
        }
    }

    private void FadeInOutTextTransparency() {
        transparentPanel.raycastTarget = true;
        animatedText.enabled = true;
        _ = StartCoroutine( FadeInOutTransparencyCO() );
        IEnumerator FadeInOutTransparencyCO() {
            while ( true ) {
                animatedText.color = animatedText.color.ChangeAlpha( Mathf.PingPong( Time.time * animationSpeed, 1f ) );
                yield return null;
            }
        }
    }
}