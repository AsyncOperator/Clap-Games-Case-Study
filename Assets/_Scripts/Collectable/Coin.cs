using UnityEngine;

public sealed class Coin : MonoBehaviour, ICollectable {

    public bool CanCollect { get; set; } = true;
    public void Collect() {
        if ( GameManager.Instance != null ) {
            GameManager.Instance.IncreaseNumberOfCollectedCoins();
            Destroy( gameObject );
        }
    }
}