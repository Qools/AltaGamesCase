using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;

    [SerializeField] private Color32 _color;
    [SerializeField] private float _destractionTime;
    [SerializeField] private float _maxSize = 1f;


    private void Start()
    {
        _setObstacleHeight();
    }

    private void _setObstacleHeight()
    {
        float random = Random.Range(0.5f, 1.1f);

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
            Destroy(this.gameObject)
        );
    }
}