using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : Singleton<GameController>
{ 

    public void Init()
    { 
        SetStatus(Status.ready);
    }


    private void OnEnable()
    {
        CustomEventSystem.OnNewLevelLoad += OnNewLevelLoad;
    }

    private void OnDisable()
    {
        CustomEventSystem.OnNewLevelLoad -= OnNewLevelLoad;
    }

    private void OnNewLevelLoad()
    {

    }
}
