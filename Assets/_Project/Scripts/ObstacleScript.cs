using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;

    public void DestoyObstacle()
    {
        _renderer.material.DOColor(Color.red, 1f).OnComplete(() => 
            Destroy(this.gameObject)
        );
    }
}
