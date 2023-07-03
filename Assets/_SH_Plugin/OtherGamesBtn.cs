using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherGamesBtn : MonoBehaviour
{
    private Init initGamePush;

    private void Awake()
    {
        initGamePush = GameObject.FindGameObjectWithTag("GamePush").GetComponent<Init>();
    }
    
    public void OtherGamesOpen()
    {
        initGamePush.OpenOtherGames();
    }
}

