using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowPlayerPiece : PlayerPiece
{
    RollingDice yellowhomerollingDice;
    // Start is called before the first frame update
    void Start()
    {
        yellowhomerollingDice = GetComponentInParent<YellowHome>().rollingDice;
    }

    public void OnMouseDown()
    {
        if (GameManager.gm.rollingDice != null)
        {
            if (!isReady)
            {
                if (GameManager.gm.rollingDice == yellowhomerollingDice && GameManager.gm.numberOfStpesToMove == 6 && GameManager.gm.canPlayerMove)
                {
                    GameManager.gm.yellowOutPlayer += 1;
                    MakePlayerReadyToMove(pathParent.YellowPathPoints);
                    GameManager.gm.numberOfStpesToMove = 0;
                    return;
                }
            }
            if (GameManager.gm.rollingDice == yellowhomerollingDice && isReady && GameManager.gm.canPlayerMove)
            {
                GameManager.gm.canPlayerMove = false;
                MovePlayer(pathParent.YellowPathPoints);
            }
        }
    }
}


