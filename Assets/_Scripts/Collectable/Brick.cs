using UnityEngine;

[RequireComponent( typeof( Collider ) )]
public sealed class Brick : MonoBehaviour, ICollectable {
    [SerializeField, Range( 0f, 0.1f )] private float scaleOnZAxis;
    [SerializeField] private Collider boxCollider;

    public bool CanCollect { get; set; } = true;

    public void Collect() {
        FindObjectOfType<Basket>().AddGameObject( gameObject );
        CanCollect = false; // Since using the same brick as a step we do not want to collect it again
        boxCollider.enabled = false; // To not bother playerCollisionHandler anymore
    }

    public void PlaceAsStep() {
        boxCollider.isTrigger = false; // Make it reactive to collision
        boxCollider.enabled = true;
        transform.localScale = transform.localScale.Change( z: scaleOnZAxis ); // Scale to make it wider
    }
}