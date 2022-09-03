using UnityEngine;

public sealed class Basket : MonoBehaviour {

    [SerializeField] private BasketCollectionHandler basketCollectionHandler;

    public GameObject GetGameObject() => basketCollectionHandler.Pop();

    public void AddGameObject( GameObject go ) => basketCollectionHandler.Push( go );
}