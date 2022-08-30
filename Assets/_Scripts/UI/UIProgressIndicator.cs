using UnityEngine;
using UnityEngine.UI;

public sealed class UIProgressIndicator : MonoBehaviour {
    [SerializeField] private Image indicator;
    private PlayerPathFollower playerPathFollower;

    private void Awake() => playerPathFollower = FindObjectOfType<PlayerPathFollower>();

    private void OnEnable() => playerPathFollower.OnDestinationChanged += UpdateIndicator;

    private void OnDisable() => playerPathFollower.OnDestinationChanged -= UpdateIndicator;

    private void UpdateIndicator( float progress ) {
        progress = Mathf.Clamp( progress, 0f, 1f );
        indicator.fillAmount = progress;
    }
}