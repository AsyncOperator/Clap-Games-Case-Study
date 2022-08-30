using System.Collections.Generic;
using UnityEngine;

public sealed class BasketCollectionHandler : MonoBehaviour {
    private static Stack<GameObject> stack = new();

    [SerializeField, Range( 0f, 1f )] private float offset;
    [SerializeField] private Transform holder;

    public void Push( GameObject go ) {
        int stackCount = stack.Count;

        go.transform.parent = holder;
        go.transform.localPosition = Vector3.forward * offset * stackCount;

        stack.Push( go );
    }

    public void Pop() {
        var popppedGameObject = stack.Pop();
        Destroy( popppedGameObject );
    }
}