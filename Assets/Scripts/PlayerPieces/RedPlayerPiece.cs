using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPlayerPiece : PlayerPiece
{
    RollingDice redhomerollingDice;
    // Start is called before the first frame update
    void Start()
    {
        redhomerollingDice = GetComponentInParent<RedHome>().rollingDice;
       
    }
    public void OnMouseDown()
    {
        if (GameManager.gm.rollingDice != null)
        {
            if (!isReady)
            {
                if (GameManager.gm.rollingDice==redhomerollingDice && GameManager.gm.numberOfStpesToMove==6 && GameManager.gm.canPlayerMove) 
                {
                    GameManager.gm.redOutPlayer += 1;
                    MakePlayerReadyToMove(pathParent.RedPathPoints);
                   GameManager.gm.numberOfStpesToMove = 0;
                    return;
                }
            }
            if (GameManager.gm.rollingDice == redhomerollingDice && isReady && GameManager.gm.canPlayerMove)
            {
                GameManager.gm.canPlayerMove = false;
                MovePlayer(pathParent.RedPathPoints);
            }
        }
    }

}
