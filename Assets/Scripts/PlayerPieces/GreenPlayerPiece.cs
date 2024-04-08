using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPlayerPiece : PlayerPiece
{
    RollingDice greenhomerollingDice;
    // Start is called before the first frame update
    void Start()
    {
        greenhomerollingDice = GetComponentInParent<GreenHome>().rollingDice;
    }

    public void OnMouseDown()
    {
        if (GameManager.gm.rollingDice != null)
        {
            if (!isReady)
            {
                if (GameManager.gm.rollingDice==greenhomerollingDice && GameManager.gm.numberOfStpesToMove==6 && GameManager.gm.canPlayerMove) 
                {
                    GameManager.gm.greenOutPlayer += 1;
                    MakePlayerReadyToMove(pathParent.GreenPathPoints);
                    GameManager.gm.numberOfStpesToMove = 0;
                    return;
                }
            }
            if (GameManager.gm.rollingDice == greenhomerollingDice && isReady && GameManager.gm.canPlayerMove) {
                GameManager.gm.canPlayerMove = false;
                MovePlayer(pathParent.GreenPathPoints);
            }
        }
    }
}
