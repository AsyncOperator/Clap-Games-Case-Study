using UnityEngine;
using UnityEngine.UI;

public sealed class UIProgressIndicator : MonoBehaviour {
    [SerializeField] private Slider progressSlider;
    [SerializeField] private PlayerPathFollower playerPathFollower;

    private void OnEnable() => playerPathFollower.OnCoverGround += UpdateIndicator;

    private void OnDisable() => playerPathFollower.OnCoverGround -= UpdateIndicator;

    // progress is between 0 - 1 and represent ratio of travelled distance of total distance
    private void UpdateIndicator( float progress ) => progressSlider.value = Mathf.Clamp01( progress );
}