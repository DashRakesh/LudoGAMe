using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoints : MonoBehaviour
{
    public PathObjectParent pathObjectParent;
    public List<PlayerPiece> playerPiecesList = new List<PlayerPiece>();
    PathPoints[] pathPointsToMoveOn_;
 
    void Start()
    {
        pathObjectParent = GetComponentInParent<PathObjectParent>();
    }
    public bool AddPlayerPiece(PlayerPiece playerpce_)
    {
        if (this.name == "ComonCenterPathPoint")
        {
            addPlayer(playerpce_);
            Complete(playerpce_);
            return false;
        }
        if (!pathObjectParent.safePoint.Contains(this))
        {
            if (playerPiecesList.Count == 1)
            {
                string prePlayerPieceName = playerPiecesList[0].name;
                string curPlayerPieceName = playerpce_.name;
                curPlayerPieceName = curPlayerPieceName.Substring(0, curPlayerPieceName.Length - 4);

                if (!prePlayerPieceName.Contains(curPlayerPieceName))
                {
                    playerPiecesList[0].isReady = false;
                    StartCoroutine(revertOnStart(playerPiecesList[0]));

                    playerPiecesList[0].numberOfStepsAlreadyMoved = 0;
                    RemovePlayerPiece(playerPiecesList[0]);
                    playerPiecesList.Add(playerpce_);
                    return false;
                }

            }
        }
        addPlayer(playerpce_);
        return true;
    }
    IEnumerator revertOnStart(PlayerPiece playerpce_)
    {
        if (playerpce_.name.Contains("red")) { GameManager.gm.redOutPlayer -= 1; pathPointsToMoveOn_ = pathObjectParent.RedPathPoints; }
        else if (playerpce_.name.Contains("green")) { GameManager.gm.greenOutPlayer -= 1; pathPointsToMoveOn_ = pathObjectParent.GreenPathPoints; }
        else if (playerpce_.name.Contains("yellow")) { GameManager.gm.yellowOutPlayer -= 1; pathPointsToMoveOn_ = pathObjectParent.YellowPathPoints; }
        else { GameManager.gm.blueOutPlayer -= 1; pathPointsToMoveOn_ = pathObjectParent.BluePathPoints; }

        for(int i=playerpce_.numberOfStepsAlreadyMoved-1; i>=0; i--)
        {
            playerpce_.transform.position = pathPointsToMoveOn_[i].transform.position;
            yield return new WaitForSeconds(0.02f);
        }

        playerpce_.transform.position = pathObjectParent.BasePoints[BasePointPosition(playerpce_.name)].transform.position;

    }
    int BasePointPosition(string name)
    {
        for (int i = 0; i < pathObjectParent.BasePoints.Length; i++)
        {
            if (pathObjectParent.BasePoints[i].name == name)
            {
                return i;
            }
        }
        return -1;
    }
    void addPlayer(PlayerPiece playerPce_)
    {
        playerPiecesList.Add(playerPce_);
        RescaleAndRepositionAllPlayerPieces();

    }
    public void RemovePlayerPiece(PlayerPiece playerPce_)
    {
        if (playerPiecesList.Contains(playerPce_))
        {
            playerPiecesList.Remove(playerPce_);
            RescaleAndRepositionAllPlayerPieces();
        }
    }

    void Complete(PlayerPiece playerpce_)
    {
        int totalCompletePlayer;
        if (playerpce_.name.Contains("red")) { GameManager.gm.redOutPlayer -= 1; totalCompletePlayer= GameManager.gm.redCompletePlayer += 1; }
        else if (playerpce_.name.Contains("green")) { GameManager.gm.greenOutPlayer -= 1; totalCompletePlayer= GameManager.gm.greenCompletePlayer += 1; }
        else if (playerpce_.name.Contains("yellow")) { GameManager.gm.yellowOutPlayer -= 1; totalCompletePlayer= GameManager.gm.yellowCompletePlayer += 1; }
        else { GameManager.gm.blueOutPlayer -= 1; totalCompletePlayer= GameManager.gm.blueCompletePlayer += 1; }

        if (totalCompletePlayer==4)
        {
            
        }

    }


    public void RescaleAndRepositionAllPlayerPieces()
    {
        int plsCount = playerPiecesList.Count;
        //  bool isOdd = (plsCount % 2) == 0 ? false : true;
        int spriteLayer = 0;

        bool isOdd;
        if (plsCount % 2 == 0)
        {
            isOdd = false;
        }
        else
        {
            isOdd = true;
        }

        int extent = plsCount / 2;
        int counter = 0;

        if (isOdd)
        {
            for (int i = -extent; i <= extent; i++)
            {
                playerPiecesList[counter].transform.localScale = new Vector3(pathObjectParent.scales[plsCount - 1], pathObjectParent.scales[plsCount - 1], 1f);
                playerPiecesList[counter].transform.position =
   new Vector3(transform.position.x + (i * pathObjectParent.positionDifference[plsCount - 1]), transform.position.y, 0f);
                counter++;

            }
        }
        else
        {
            for (int i = -extent; i < extent; i++)
            {
                playerPiecesList[counter].transform.localScale = new Vector3(pathObjectParent.scales[plsCount - 1], pathObjectParent.scales[plsCount - 1], 1f);
                playerPiecesList[counter].transform.position =
   new Vector3(transform.position.x + (i * pathObjectParent.positionDifference[plsCount - 1]), transform.position.y, 0f);
                counter++;
            }

        }
        for (int i = 0; i < playerPiecesList.Count; i++)
        {
            playerPiecesList[i].GetComponentInChildren<SpriteRenderer>().sortingOrder = spriteLayer;
            spriteLayer++;
        }
    }
}
