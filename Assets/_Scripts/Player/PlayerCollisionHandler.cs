using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour {

    private void OnTriggerEnter( Collider other ) {
        if ( other.transform.TryGetComponent( out ICollectable collectable ) ) {
            collectable.OnCollect();
        }
    }
}