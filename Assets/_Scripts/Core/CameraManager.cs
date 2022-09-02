using UnityEngine;
using Cinemachine;

public sealed class CameraManager : MonoBehaviour {
    [SerializeField] private CinemachineVirtualCamera followCamera;
    [SerializeField] private PlayerCollisionHandler playerCollisionHandler;

    private void OnEnable() => playerCollisionHandler.OnPlayerDeath += FocusToNewTarget;

    private void OnDisable() => playerCollisionHandler.OnPlayerDeath -= FocusToNewTarget;

    private void FocusToNewTarget( Transform newTarget ) {
        followCamera.Follow = newTarget;
        followCamera.LookAt = newTarget;
    }
}