using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallScript : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;
    private GameObject _spawnedBall;

    [SerializeField] private Vector3 _spawnOffset;
    [SerializeField] private float _sizeIncreaseCoefficient;

    private bool _isSpawned = false;
    private bool _isGameStarted = false;
    private bool _isBallKicked = false;

    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    private void OnDisable()
    {
        CustomEventSystem.OnStartGame -= _onGameStart;
        CustomEventSystem.OnBallExploded -= _onBallExploded;
    }

    private void _spawnBall()
    {
        if (_isSpawned)
        {
            return;
        }

        Vector3 spawnPos = transform.position + _spawnOffset;

        GameObject ball = Instantiate(_ballPrefab, spawnPos, Quaternion.identity, transform.root);

        ball.transform.localScale = this.transform.localScale * 0.5f;

        _spawnedBall = ball;


        _isSpawned = true;
    }

    private void _reducePlayerBallSize()
    {
        this.transform.localScale += Vector3.one * -_sizeIncreaseCoefficient;
    }

    private void _increaseBallSize()
    {
        _spawnedBall.transform.localScale += Vector3.one * _sizeIncreaseCoefficient;
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
}
