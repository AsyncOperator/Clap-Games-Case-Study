using UnityEngine;

public sealed class Basket : MonoBehaviour {

    [SerializeField] private BasketCollectionHandler basketCollectionHandler;

    public GameObject RemoveObject() => basketCollectionHandler.Pop();

    public void AddObject( GameObject go ) => basketCollectionHandler.Push( go );
}