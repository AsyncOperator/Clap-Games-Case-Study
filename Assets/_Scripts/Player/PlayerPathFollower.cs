using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using PathCreation;

public sealed class PlayerPathFollower : MonoBehaviour {
    [SerializeField] private CharacterController characterController;
    [SerializeField] private PathCreator pathCreator;

    [SerializeField, Range( 5f, 10f )] private float rotationSpeed;
    [Tooltip( "How fast the character moves while on the ground" )]
    [SerializeField, Min( 0f )] private float groundSpeed;
    [Tooltip( "How fast the character moves while gliding" )]
    [SerializeField, Min( 0f )] private float glideSpeed;
    [SerializeField] private float gravity;

#if UNITY_EDITOR
    [SerializeField] private bool debug;
#endif

    private float distanceTravelled;
    private bool canMove = false;

    private VertexPath vertexPath;
    private new Transform transform;

    [Tooltip( "Debug purposes for developer" )]
    [field: SerializeField, ReadOnly] public bool IsGrounded { get; private set; }

    public event Action<float> OnCoverGround;

    private void OnValidate() => gravity = -Mathf.Abs( gravity );

    private void Awake() {
        vertexPath = pathCreator.path;
        transform = gameObject.transform;
    }

    private void OnEnable() => GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;

    private void OnDisable() => GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;

    private void Update() {
        if ( !canMove ) {
            return;
        }

#if UNITY_EDITOR
        if ( debug ) {
            CollisionFlagDebugging();
        }
#endif

        distanceTravelled = vertexPath.GetClosestDistanceAlongPath( transform.position );
        Vector3 directionXZ = vertexPath.GetDirectionAtDistance( distanceTravelled ).normalized.Change( y: 0f );
        // To make character face into direction
        transform.rotation = Quaternion.Slerp( transform.rotation, Quaternion.LookRotation( directionXZ ), rotationSpeed * Time.deltaTime );

        directionXZ = IsGrounded ? directionXZ.Change( y: -0.2f ) : directionXZ.Change( y: gravity );

        float speedXZ = IsGrounded ? groundSpeed : glideSpeed;
        Vector3 velocity = directionXZ * speedXZ * Time.deltaTime;

        characterController.Move( velocity );

        float ratioOfTravelledDistance = vertexPath.GetClosestTimeOnPath( transform.position );
        OnCoverGround?.Invoke( ratioOfTravelledDistance );

        IsGrounded = characterController.isGrounded;

        if ( Mathf.Approximately( ratioOfTravelledDistance, 1f ) ) {
            GameManager.Instance.ChangeGameState( GameManager.GameState.Win );
        }
    }

    private void GameManager_OnGameStateChanged( GameManager.GameState currentState ) => canMove = ( currentState & GameManager.GameState.Ongoing ) != 0;

#if UNITY_EDITOR
    private void CollisionFlagDebugging() {
        CollisionFlags bitmask = characterController.collisionFlags & ~CollisionFlags.None;

        if ( bitmask == 0 ) {
            Debug.Log( "Touching anything <color=red><b>EDITOR ONLY LOG</b></color>" );
        }

        if ( ( bitmask & CollisionFlags.Below ) != 0 ) {
            Debug.Log( "Touching below <color=red><b>EDITOR ONLY LOG</b></color>" );
        }

        if ( ( bitmask & CollisionFlags.Above ) != 0 ) {
            Debug.Log( "Touching above <color=red><b>EDITOR ONLY LOG</b></color>" );
        }

        if ( ( bitmask & CollisionFlags.Sides ) != 0 ) {
            Debug.Log( "Touching sides <color=red><b>EDITOR ONLY LOG</b></color>" );
        }
    }
#endif

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        if ( vertexPath != null ) {
            // Adding a bit up offset to draw the line on player's head level
            Vector3 startPosition = transform.position + transform.up * 0.05f;
            Vector3 endPosition = vertexPath.GetDirectionAtDistance( distanceTravelled );

            Handles.zTest = UnityEngine.Rendering.CompareFunction.Always;
            Handles.color = Color.red;
            Handles.DrawAAPolyLine( 5f, startPosition, startPosition + ( endPosition * 0.05f ) );
        }
    }
#endif
}