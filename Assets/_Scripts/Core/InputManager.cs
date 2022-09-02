using UnityEngine;

public sealed class InputManager : MonoBehaviour {
    [SerializeField] private StepCreator stepCreator;
    [SerializeField] private float waitTimeBetweenClick;
    private float? lastRegisteredClickTime;

    private void Update() {
        if ( Input.GetMouseButton( 0 ) ) {
            if ( lastRegisteredClickTime == null ) {
                lastRegisteredClickTime = Time.time;
                stepCreator?.TryPlaceStep();
            }
            else if ( Time.time - lastRegisteredClickTime >= waitTimeBetweenClick ) {
                lastRegisteredClickTime = Time.time;
                stepCreator?.TryPlaceStep();
            }
        }
    }
}