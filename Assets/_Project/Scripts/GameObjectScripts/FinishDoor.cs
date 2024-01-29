using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDoor : MonoBehaviour
{

    [SerializeField] private Transform _finishDoor;
    [SerializeField] private Transform _endPosition;
    private Transform _playerBall;

    private bool _isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {

        if (_isTriggered)
        {
            return;
        }

        if (other.CompareTag(PlayerPrefKeys.Player))
        {
            _openDoor();

            _playerBall = other.transform;

            _isTriggered = true;

            if (other.TryGetComponent(out BallScript ballScript))
            {
                ballScript.StopBall();
            }
        }
    }

    private void _openDoor()
    {
        _finishDoor.DOMoveY(5f, 1f).OnComplete(() =>
        {
            _movePlayerInside();
        });
    }

    private void _movePlayerInside()
    {
        _playerBall.DOMove(_endPosition.position, 1f).OnComplete(() => 
        {
            CustomEventSystem.CallGameOver(GameResult.Win);
        });
    }
}
