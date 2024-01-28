using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallScript : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;
    private GameObject _spawnedBall;


    [SerializeField] private Rigidbody _playerRb;

    [SerializeField] private Vector3 _spawnOffset;
    [SerializeField] private float _sizeIncreaseCoef;
    [SerializeField] private float _sizeDecreaseCoef;
    [SerializeField] private float _loseThreshold;
    [SerializeField] private float _ballSpeedCoef;

    private float _loseSize;

    private bool _isSpawned = false;
    private bool _isGameStarted = false;
    private bool _isBallKicked = false;


    // Start is called before the first frame update
    void Start()
    {
        _loseSize = this.transform.localScale.x * _loseThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isGameStarted)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _spawnBall();
        }

        if (_isSpawned)
        {
            if (Input.GetMouseButtonUp(0))
            {
                _isBallKicked = true;
                CustomEventSystem.CallBallKicked();

                _checkBallSize();
            }
        }
    }

    private void FixedUpdate()
    {

        if (!_isGameStarted)
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            if (!_isSpawned)
            {
                return;
            }

            if (_isBallKicked)
            {
                return;
            }

            _reducePlayerBallSize();
            _increaseBallSize();
        }
    }

    private void OnEnable()
    {
        CustomEventSystem.OnStartGame += _onGameStart;
        CustomEventSystem.OnBallExploded += _onBallExploded;
        CustomEventSystem.OnObstaclesCleared += _moveToFinish;
    }

    private void OnDisable()
    {
        CustomEventSystem.OnStartGame -= _onGameStart;
        CustomEventSystem.OnBallExploded -= _onBallExploded;
        CustomEventSystem.OnObstaclesCleared -= _moveToFinish;
    }

    private void _spawnBall()
    {
        if (_isSpawned)
        {
            return;
        }

        Vector3 spawnPos = transform.position + _spawnOffset;

        GameObject ball = Instantiate(_ballPrefab, spawnPos, Quaternion.identity, transform.root);

        ball.transform.localScale = Vector3.one * 0.1f;

        _spawnedBall = ball;


        _isSpawned = true;
    }

    private void _reducePlayerBallSize()
    {
        this.transform.localScale += Vector3.one * -_sizeDecreaseCoef;
    }

    private void _increaseBallSize()
    {
        _spawnedBall.transform.localScale += Vector3.one * _sizeIncreaseCoef;
    }

    private void _onGameStart()
    {
        _isGameStarted = true;
    }

    private void _onBallExploded()
    {
        _isBallKicked = false;
        _isSpawned = false;
    }

    private void _checkBallSize()
    {
        if (this.transform.localScale.x <= _loseSize)
        {
            CustomEventSystem.CallGameOver(GameResult.Lose);

            _onGameOver();
        }
    }

    private void _onGameOver()
    {
        _isGameStarted = false;
    }

    private void _moveToFinish()
    {
        _isBallKicked = true;
        _isSpawned = true;

        _playerRb.isKinematic = false;
        _playerRb.AddForce((_ballSpeedCoef * this.transform.localScale.x) * transform.forward, ForceMode.Impulse);
    }
}
