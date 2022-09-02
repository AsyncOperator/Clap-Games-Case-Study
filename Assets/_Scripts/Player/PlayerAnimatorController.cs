using UnityEngine;
using Animancer;

public sealed class PlayerAnimatorController : MonoBehaviour {
    [SerializeField] private PlayerPathFollower playerPathFollower;

    [SerializeField] private AnimancerComponent animancer;
    [SerializeField] private ClipTransition runAnimation;
    [SerializeField] private ClipTransition fallAnimation;

    private void Update() {
        if ( playerPathFollower.IsGrounded ) {
            animancer.Play( runAnimation );
        }
        else {
            animancer.Play( fallAnimation );
        }
    }
}