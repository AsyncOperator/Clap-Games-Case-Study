using System;
using UnityEngine;
using PathCreation;

public sealed class PlayerPathFollower : MonoBehaviour {
    [SerializeField] private PathCreator pathCreator;
    [SerializeField] private float moveSpeed;
    private float distanceTravelled;

    private VertexPath vertexPath;
    private new Transform transform;

    public event Action<float> OnDestinationChanged;

    private void Awake() {
        vertexPath = pathCreator.path;
        transform = gameObject.transform;
    }

    private void Update() {
        distanceTravelled += moveSpeed * Time.deltaTime;
        transform.position = vertexPath.GetPointAtDistance( distanceTravelled );
        transform.forward = vertexPath.GetDirectionAtDistance( distanceTravelled );

        OnDestinationChanged?.Invoke( vertexPath.GetClosestTimeOnPath( transform.position ) );
    }
}