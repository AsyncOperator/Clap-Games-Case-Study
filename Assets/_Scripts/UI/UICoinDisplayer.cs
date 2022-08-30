using UnityEngine;
using TMPro;

public sealed class UICoinDisplayer : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI coinTMP;

    private void OnEnable() {
        if ( GameManager.Instance != null ) {
            GameManager.Instance.OnNumberOfCollectedCoinsChanged += UpdateDisplayText;
        }
        else {
            Debug.LogError( "Could not subscribe GameManager event since its null" );
        }
    }

    private void OnDisable() {
        if ( GameManager.Instance != null ) {
            GameManager.Instance.OnNumberOfCollectedCoinsChanged -= UpdateDisplayText;
        }
        else {
            Debug.LogError( "Could not unsubscribe GameManager event since its null" );
        }
    }

    public void UpdateDisplayText( int value ) => coinTMP.SetText( value.ToString() );
}