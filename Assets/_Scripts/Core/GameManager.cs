using System;
using UnityEngine;

public sealed class GameManager : MonoBehaviour {
    public static GameManager Instance;
    private int numberOfCollectedCoins;

    public event Action<int> OnNumberOfCollectedCoinsChanged;

    private void Awake() => Instance = this;

    public void IncreaseNumberOfCollectedCoins() {
        numberOfCollectedCoins++;
        OnNumberOfCollectedCoinsChanged?.Invoke( numberOfCollectedCoins );
    }
}