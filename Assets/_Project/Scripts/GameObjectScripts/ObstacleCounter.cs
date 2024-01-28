using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCounter : MonoBehaviour
{
    [SerializeField] private int _counter;

    [SerializeField] private Vector3 _boxColliderSize;

    private void Start()
    {
        _counter = 0;

        _checkObstaclesCount();
    }

    private void FixedUpdate()
    {
        _checkObstaclesCount();
    }

    private void _checkObstaclesCount()
    {
        _counter = 0;

        Collider[] hitColliders = Physics.OverlapBox(this.transform.position, _boxColliderSize, Quaternion.identity);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(PlayerPrefKeys.Obstacle))
            {
                _counter++;
            }
        }

        if (_counter == 0)
        {
            CustomEventSystem.CallObstaclesCleared();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, _boxColliderSize);
    }
}
