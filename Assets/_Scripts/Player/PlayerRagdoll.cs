using UnityEngine;

public sealed class PlayerRagdoll : MonoBehaviour {
    [field: SerializeField] public Transform Source { get; private set; }

    public void MatchAllChildTransforms( Transform root, Transform clone ) {
        foreach ( Transform child in root ) {
            Transform cloneChild = clone.Find( child.name );
            if ( cloneChild != null ) {
                cloneChild.SetPositionAndRotation( child.position, child.rotation );
                MatchAllChildTransforms( child, cloneChild );
            }
        }
    }
}