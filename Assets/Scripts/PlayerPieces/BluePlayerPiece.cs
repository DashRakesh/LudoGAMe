using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlayerPiece : PlayerPiece
{
    RollingDice bluehomerollingDice;
    // Start is called before the first frame update
    void Start()
    {
        bluehomerollingDice = GetComponentInParent<BlueHome>().rollingDice;
    }

    public void OnMouseDown()
    {
        if (GameManager.gm.rollingDice!= null) {
            if (!isReady)
            {
                if (GameManager.gm.rollingDice == bluehomerollingDice && GameManager.gm.numberOfStpesToMove==6 && GameManager.gm.canPlayerMove)
                {
                    GameManager.gm.blueOutPlayer += 1;
                    MakePlayerReadyToMove(pathParent.BluePathPoints);
                   GameManager.gm.numberOfStpesToMove = 0;
                    return;
                }
            }
            if (GameManager.gm.rollingDice == bluehomerollingDice && isReady && GameManager.gm.canPlayerMove) {
                GameManager.gm.canPlayerMove = false;
                MovePlayer(pathParent.BluePathPoints);
            }
        }
    }
}
