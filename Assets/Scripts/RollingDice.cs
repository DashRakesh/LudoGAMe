using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingDice : MonoBehaviour
{
    [SerializeField] Sprite[] numberSprite;
    [SerializeField] SpriteRenderer numberSpriteHolder;
    [SerializeField] SpriteRenderer rollingDiceAnimation;
    [SerializeField] int numberedGot;

    Coroutine generateRandomNumberDice;

    int otPlayers;
    List<PlayerPiece> playerPices;
    PathPoints[] currentPathPoint;
    public PlayerPiece currentPlayerPiece;

    public Dice diceSound;
   
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void OnMouseDown()
    {
        generateRandomNumberDice = StartCoroutine(rollDice());
    }
    public void ComputerRoll()
    {
        generateRandomNumberDice = StartCoroutine(rollDice());
    }
    IEnumerator rollDice()
    {
        yield return new WaitForEndOfFrame();

        if (GameManager.gm.canDiceRoll)
        {
            GameManager.gm.canDiceRoll = false;
            diceSound.playSound();
            numberSpriteHolder.gameObject.SetActive(false);
            rollingDiceAnimation.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            numberedGot = Random.Range(0, 6);
            numberSpriteHolder.sprite = numberSprite[numberedGot];
            numberedGot++;
            GameManager.gm.numberOfStpesToMove = numberedGot;
            GameManager.gm.rollingDice = this;

            numberSpriteHolder.gameObject.SetActive(true);
            rollingDiceAnimation.gameObject.SetActive(false);
            outPlayer();

            if (PlayerCanMove())
            {
                if (otPlayers==0)
                {
                    readyToMove(0);
                    
                }
                else
                {
                    if (GameManager.gm.totalPlayerCanPlay == 1 && otPlayers < 4 && GameManager.gm.numberOfStpesToMove == 6)
                    {
                        RobotOut();


                    }
                    if (GameManager.gm.totalPlayerCanPlay == 1 && otPlayers ==0 && GameManager.gm.numberOfStpesToMove == 6)
                    {
                        currentPlayerPiece.MovePlayer(currentPathPoint);

                    }
                    else
                    {
                        currentPlayerPiece.MovePlayer(currentPathPoint);
                    }
                }
            }
            else
            {
                if (GameManager.gm.numberOfStpesToMove != 6 && otPlayers == 0)
                {
                    yield return new WaitForSeconds(0.5f);
                    GameManager.gm.transferDice = true;
                    GameManager.gm.rollingDiceTransfer();
                }

            }
           



            if (generateRandomNumberDice != null)
            {
                StopCoroutine(rollDice());
            }
        }

    }
    public void outPlayer()
    {
        if (GameManager.gm.rollingDice == GameManager.gm.rollingDiceList[0])
        {
            playerPices = GameManager.gm.redPlayerPices;
            currentPathPoint = playerPices[0].pathParent.RedPathPoints;
            otPlayers = GameManager.gm.redOutPlayer;
        }
        else if (GameManager.gm.rollingDice == GameManager.gm.rollingDiceList[1])
        {
            playerPices = GameManager.gm.greenPlayerPices;
            currentPathPoint = playerPices[0].pathParent.GreenPathPoints;
            otPlayers = GameManager.gm.greenOutPlayer;
        }
        else if (GameManager.gm.rollingDice == GameManager.gm.rollingDiceList[2])
        {
            playerPices = GameManager.gm.yellowPlayerPices;
            currentPathPoint = playerPices[0].pathParent.YellowPathPoints;
            otPlayers = GameManager.gm.yellowOutPlayer;
        }
        else 
        {
            playerPices = GameManager.gm.bluePlayerPices;
            currentPathPoint = playerPices[0].pathParent.BluePathPoints;
            otPlayers = GameManager.gm.blueOutPlayer;
        }
    }
    public bool PlayerCanMove()
    {
        if (GameManager.gm.totalPlayerCanPlay == 1)
        {
            if(GameManager.gm.rollingDice== GameManager.gm.rollingDiceList[2])
            {
                if (otPlayers > 0)
                {
                    for (int i = 0; i < playerPices.Count; i++)
                    {
                        if (playerPices[i].isReady)
                        {
                            if (playerPices[i].isPathPointAvailableToMove(GameManager.gm.numberOfStpesToMove, playerPices[i].numberOfStepsAlreadyMoved, currentPathPoint))
                            {
                                currentPlayerPiece = playerPices[i];
                                return true;
                            }
                        }

                    }

                }
            }
        }

          if (otPlayers==1 && GameManager.gm.numberOfStpesToMove != 6)
        {
            for (int i=0; i< playerPices.Count; i++)
            {
                if (playerPices[i].isReady)
                {
                    if (playerPices[i].isPathPointAvailableToMove(GameManager.gm.numberOfStpesToMove, playerPices[i].numberOfStepsAlreadyMoved,currentPathPoint))
                    {
                        currentPlayerPiece = playerPices[i];
                        return true;
                    }
                }

            }
        }
        else if (otPlayers == 0 && GameManager.gm.numberOfStpesToMove == 6)
        {
            return true;
        }
            return false;
    }
    void readyToMove(int pos)
    {
        if (GameManager.gm.rollingDice == GameManager.gm.rollingDiceList[0])
        {
 
             GameManager.gm.redOutPlayer+=1;
        }
        else if (GameManager.gm.rollingDice == GameManager.gm.rollingDiceList[1])
        {

             GameManager.gm.greenOutPlayer+=1;
        }
        else if (GameManager.gm.rollingDice == GameManager.gm.rollingDiceList[2])
        {

            GameManager.gm.yellowOutPlayer+=1;
        }
        else
        {

             GameManager.gm.blueOutPlayer+=1;
        }
        playerPices[pos].MakePlayerReadyToMove(currentPathPoint);
    }
    void RobotOut()
    {
        for(int i=0; i<playerPices.Count; i++)
        {
            if (!playerPices[i].isReady)
            {
                readyToMove(i);
                return;
            }
        }
    }
}
