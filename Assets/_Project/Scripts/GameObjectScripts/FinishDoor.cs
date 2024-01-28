using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerPrefKeys.Player))
        {
            CustomEventSystem.CallGameOver(GameResult.Win);
        }
    }
}
