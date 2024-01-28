using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] private Rigidbody _ballRb;

    [SerializeField] private float _ballSpeedCoef;
    [SerializeField] private float _ballAreaCoef;
    [SerializeField] private float _ballDestroyDelay;
    [SerializeField] private float _ballSelfDestructionTime;

    private bool _isKicked = false;
    private bool _isObstacleHit = false;

    private void Start()
    {
        _ballRb.isKinematic = true;

        DOVirtual.DelayedCall(_ballSelfDestructionTime, () => _destroyBall());
    }

    private void OnEnable()
    {
        CustomEventSystem.OnBallKicked += KickBall;
    }

    private void OnDisable()
    {
        CustomEventSystem.OnBallKicked -= KickBall;
    }

    public void KickBall()
    {
        if(_isKicked)
        {
            return;
        }

        _isKicked = true;

        _ballRb.isKinematic = false;
        _ballRb.AddForce((_ballSpeedCoef * this.transform.localScale.x) * transform.forward, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == PlayerPrefKeys.Obstacle)
        {
            if (_isObstacleHit)
            {
                return;
            }

            _isKicked = true;
 
            _stopBall();

            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, this.transform.localScale.x * _ballAreaCoef);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.TryGetComponent(out ObstacleScript obstacle))
                {
                    obstacle.DestoyObstacle();
                }
            }

            DOVirtual.DelayedCall(_ballDestroyDelay, () => _destroyBall());
        }
    }

    private void _stopBall()
    {
        _ballRb.velocity = Vector3.zero;
        _ballRb.angularVelocity = Vector3.zero;
        _ballRb.isKinematic = true;
    }

    private void _destroyBall()
    {
        //explosion sfx

        CustomEventSystem.CallBallExploded();

        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, this.transform.localScale.x * _ballAreaCoef);
    }
}
