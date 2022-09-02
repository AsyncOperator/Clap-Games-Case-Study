using System.Collections.Generic;
using UnityEngine;

public sealed class BasketCollectionHandler : MonoBehaviour {
    private Stack<GameObject> stack = new();

    [SerializeField, Range( 0f, 1f )] private float offset;
    [SerializeField] private Transform holder;

    public void Push( GameObject go ) {
        int stackCount = stack.Count;

        go.transform.parent = holder;
        go.transform.localPosition = holder.InverseTransformDirection( Vector3.up ) * offset * stackCount;

        stack.Push( go );
    }

    public GameObject Pop() {
        GameObject poppedGameObject = null;

        if ( stack.Count > 0 ) {
            poppedGameObject = stack.Pop();
            if ( poppedGameObject != null ) {
                poppedGameObject.transform.parent = null;
            }
        }

        return poppedGameObject;
    }
}