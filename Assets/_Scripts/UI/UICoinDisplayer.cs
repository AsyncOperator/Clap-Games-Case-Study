using UnityEngine;
using TMPro;

public sealed class UICoinDisplayer : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI coinDisplayText;

    private void OnEnable() {
        if ( GameManager.Instance != null ) {
            GameManager.Instance.OnNumberOfCollectedCoinsChanged += UpdateCoinDisplayText;
        }
    }

    private void OnDisable() {
        if ( GameManager.Instance != null ) {
            GameManager.Instance.OnNumberOfCollectedCoinsChanged -= UpdateCoinDisplayText;
        }
    }

    public void UpdateCoinDisplayText( int numberOfCollectedCoins ) => coinDisplayText.SetText( numberOfCollectedCoins.ToString() );
}