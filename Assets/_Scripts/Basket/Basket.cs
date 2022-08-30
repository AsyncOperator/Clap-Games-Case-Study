using UnityEngine;

public sealed class Basket : MonoBehaviour {

    [SerializeField] private BasketCollectionHandler basketCollectionHandler;

    private int numberOfObjects;

    public void RemoveObject() {
        if ( numberOfObjects > 0 ) {
            numberOfObjects--;
        }
    }

    public void AddObject( GameObject go ) {
        basketCollectionHandler.Push( go );
        numberOfObjects++;
    }
}