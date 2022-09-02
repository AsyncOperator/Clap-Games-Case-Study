using System;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour {

    [Tooltip( "Object that will spawn after the character dies" )]
    [SerializeField] private PlayerRagdoll playerRagdoll;

    [Tooltip( "This field should point to the root/hips of player" )]
    [SerializeField] private Transform activePlayerRoot;

    [Tooltip( "Touching any object in this layer causes the player to die/lose" )]
    [SerializeField] private LayerMask deathLayer;

    public event Action<Transform> OnPlayerDeath;

    private void OnTriggerEnter( Collider other ) {
        if ( other.transform.TryGetComponent( out ICollectable collectable ) ) {
            if ( collectable.CanCollect ) {
                collectable.Collect();
            }
        }
    }

    private void OnControllerColliderHit( ControllerColliderHit hit ) {
        //  Check if the hit object in deathLayer
        if ( ( deathLayer & ( 1 << hit.gameObject.layer ) ) != 0 ) {

#if UNITY_EDITOR
            var sphere = GameObject.CreatePrimitive( PrimitiveType.Sphere );
            sphere.name = "(DEBUG OBJECT) Show Hit Point";
            sphere.transform.parent = null;
            sphere.transform.position = hit.point;
            sphere.transform.localScale = Vector3.one * 0.01f;
#endif

            PlayerRagdoll playerRagdollInstance = Instantiate( playerRagdoll, transform.position, transform.rotation );
            playerRagdollInstance.MatchAllChildTransforms( activePlayerRoot, playerRagdollInstance.Source );
            OnPlayerDeath?.Invoke( playerRagdollInstance.transform );

            GameManager.Instance.ChangeGameState( GameManager.GameState.Lose );

            Destroy( gameObject );
        }
    }
}