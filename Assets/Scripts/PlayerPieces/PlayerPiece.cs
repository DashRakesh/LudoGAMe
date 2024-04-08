using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPiece : MonoBehaviour
{
    public bool moveNow;
    public bool isReady;
    public int numberOfStepsToMove;
    public int numberOfStepsAlreadyMoved;

    public PathObjectParent pathParent;
    Coroutine playerMovement;

    public PathPoints previousPathPoint;
    public PathPoints currentPathPoint;

    private void Awake()
    {
        pathParent = FindObjectOfType<PathObjectParent>();
    }

    public void MovePlayer(PathPoints[] pathParent_)
    {
      playerMovement = StartCoroutine(MoveStep_Enum(pathParent_));
    }
    public void MakePlayerReadyToMove(PathPoints[] pathParent_)
    {
        isReady = true;
        transform.position = pathParent_[0].transform.position;
        numberOfStepsAlreadyMoved = 1;
        GameManager.gm.numberOfStpesToMove = 0;

        previousPathPoint = pathParent_[0];
        currentPathPoint = pathParent_[0];
        currentPathPoint.AddPlayerPiece(this);
        GameManager.gm.AddPathPoint(currentPathPoint);

        GameManager.gm.rollingDiceTransfer();
    }
    IEnumerator MoveStep_Enum(PathPoints[] pathParent_)
    {
        yield return new WaitForSeconds(0.25f);
        numberOfStepsToMove = GameManager.gm.numberOfStpesToMove;
        for (int i = numberOfStepsAlreadyMoved; i < (numberOfStepsAlreadyMoved + numberOfStepsToMove); i++)
        {
            if (isPathPointAvailableToMove(numberOfStepsToMove, numberOfStepsAlreadyMoved, pathParent_))
            {
                transform.position = pathParent_[i].transform.position;
                if(GameManager.gm.sound){ GameManager.gm.ads.Play(); }
                yield return new WaitForSeconds(0.35f);
            }
        }
        if (isPathPointAvailableToMove(numberOfStepsToMove, numberOfStepsAlreadyMoved, pathParent_))
        {
            numberOfStepsAlreadyMoved += numberOfStepsToMove;
           

            GameManager.gm.RemovePathPoint(previousPathPoint);
            previousPathPoint.RemovePlayerPiece(this);

            currentPathPoint = pathParent_[numberOfStepsAlreadyMoved - 1];
            bool transfer = currentPathPoint.AddPlayerPiece(this);
            currentPathPoint.RescaleAndRepositionAllPlayerPieces();
            GameManager.gm.AddPathPoint(currentPathPoint);

            previousPathPoint = currentPathPoint;

            if(transfer && GameManager.gm.numberOfStpesToMove != 6)
            {
                GameManager.gm.transferDice = true;
            }
            GameManager.gm.numberOfStpesToMove = 0;

        }
        
        GameManager.gm.canPlayerMove = true;

        GameManager.gm.rollingDiceTransfer();

        if (playerMovement!=null)
        {
            StopCoroutine("MoveStep_Enum()");
        }
    
    }
    public bool isPathPointAvailableToMove(int numbersOfStepsToMove, int numberOfstepsAlredyMove, PathPoints[] pathParent_)
    {
        if (numbersOfStepsToMove==0)
        {
            return false;
        }
        int leftNumberOfPath = pathParent_.Length - numberOfstepsAlredyMove;
        if (leftNumberOfPath>= numbersOfStepsToMove)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
