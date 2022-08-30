using UnityEngine;

public sealed class Coin : MonoBehaviour, ICollectable {

    public void OnCollect() {
        if ( GameManager.Instance != null ) {
            GameManager.Instance.IncreaseNumberOfCollectedCoins();
            Destroy( gameObject );
        }
    }
}