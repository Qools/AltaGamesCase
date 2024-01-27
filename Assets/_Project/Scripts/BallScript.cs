using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] private Rigidbody _ballRb;
    [SerializeField] private SphereCollider _explosiveCollider;

    private void Start()
    {
    }

    private void OnEnable()
    {
        CustomEventSystem.OnBallKicked += KickBall;
        CustomEventSystem.OnBallKicked += _setExplosideColliderRadius;
    }

    private void OnDisable()
    {
        CustomEventSystem.OnBallKicked -= KickBall;
        CustomEventSystem.OnBallKicked -= _setExplosideColliderRadius;
    }

    public void KickBall()
    {
        Debug.Log("force " + (10f * this.transform.localScale.x) * transform.forward);
        _ballRb.AddForce((50f * this.transform.localScale.x) * transform.forward, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerPrefKeys.Obstacle))
        {
            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, _explosiveCollider.radius);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.TryGetComponent(out ObstacleScript obstacle))
                {
                    obstacle.DestoyObstacle();
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(PlayerPrefKeys.Obstacle))
        {
            _stopBall();

            DOVirtual.DelayedCall(1.5f, () => _destroyBall());
        }
    }

    private void _stopBall()
    {
        //_ballRb.velocity = Vector3.zero;
        //_ballRb.angularVelocity = Vector3.zero;
    }

    private void _destroyBall()
    {
        //explosion sfx

        CustomEventSystem.CallBallExploded();

        Destroy(this.gameObject);
    }

    private void _setExplosideColliderRadius()
    {
        _explosiveCollider.radius = this.transform.localScale.x * 3f;
    }
}
