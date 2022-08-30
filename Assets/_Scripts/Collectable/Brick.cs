using UnityEngine;

public sealed class Brick : MonoBehaviour, ICollectable {
    public void OnCollect() {
        FindObjectOfType<Basket>().AddObject( gameObject );
    }
}