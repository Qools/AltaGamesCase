using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;

    [SerializeField] private GameObject _explosionVfx;

    [SerializeField] private Color32 _color;
    [SerializeField] private float _destractionTime;
    [SerializeField] private float _maxSize = 1f;


    private void Start()
    {
        _enableVfx(false);
        _setObstacleHeight();
    }

    private void _setObstacleHeight()
    {
        float random = Random.Range(0.7f, 1.1f);

        this.transform.localScale = new Vector3(1f, random, 1f);

        _setObstacleYPosition();
    }

    private void _setObstacleYPosition()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.localScale.y * 1.5f, this.transform.position.z);
    }

    public void DestoyObstacle()
    {
        _renderer.material.DOColor(_color, _destractionTime).OnComplete(() =>
        {
            _enableVfx(true);
            //DOVirtual.DelayedCall(0.33f, ()=> this.gameObject.SetActive(false));

            StartCoroutine(_disableObstacle(0.33f));
        });
    }

    private void _enableVfx(bool isEnable)
    {
        _explosionVfx.SetActive(isEnable);
    }


    private IEnumerator _disableObstacle(float delay)
    {
        yield return new WaitForSeconds(delay);

        this.gameObject.SetActive(false);
    }
}
