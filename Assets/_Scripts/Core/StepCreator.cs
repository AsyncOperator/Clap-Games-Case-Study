using UnityEngine;

public sealed class StepCreator : MonoBehaviour {
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector2 placementOffset;
    [SerializeField] private Basket basket;

    public void TryPlaceStep() {
        GameObject removedObject = basket.GetGameObject();
        if ( removedObject != null ) {

            removedObject.transform.position = playerTransform.position + playerTransform.TransformDirection( new Vector3( 0f, placementOffset.x, placementOffset.y ) );
            Quaternion rotation = playerTransform.rotation * Quaternion.Euler( 0f, 90f, 0f );
            removedObject.transform.rotation = rotation;

            if ( removedObject.TryGetComponent( out Brick brick ) ) {
                brick.PlaceAsStep();
            }
        }
    }

    private void OnDrawGizmos() {
        if ( playerTransform != null ) {
            Gizmos.color = new Color( 1f, 1f, 1f, 0.6f );
            Vector3 increment = playerTransform.TransformDirection( new Vector3( 0f, placementOffset.x, placementOffset.y ) );
            Vector3 position = playerTransform.position + increment;
            Gizmos.DrawSphere( position, 0.005f );
        }
    }
}