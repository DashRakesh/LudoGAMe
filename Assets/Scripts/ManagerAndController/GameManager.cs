using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public int numberOfStpesToMove;
    public RollingDice rollingDice;
    public bool canPlayerMove = true;

    public bool canDiceRoll = true;
    public bool transferDice = false;

    List<PathPoints> PlayersOnpathpointList = new List<PathPoints>();
    public List<RollingDice> rollingDiceList;

    public int redOutPlayer;
    public int greenOutPlayer;
    public int yellowOutPlayer;
    public int blueOutPlayer;

    public int redCompletePlayer;
    public int greenCompletePlayer;
    public int yellowCompletePlayer;
    public int blueCompletePlayer;

    public int totalPlayerCanPlay;
    public List<GameObject> playerHomes;

    public List<PlayerPiece> redPlayerPices;
    public List<PlayerPiece> greenPlayerPices;
    public List<PlayerPiece> yellowPlayerPices;
    public List<PlayerPiece> bluePlayerPices;

    public AudioSource ads;
    public bool sound = true;

    private void Awake()
    {
        gm = this;
        ads = GetComponent<AudioSource>();
    }

    public void AddPathPoint(PathPoints pathPoint)
    {
        PlayersOnpathpointList.Add(pathPoint);
    }
    public void RemovePathPoint(PathPoints pathPoint)
    {
        if (PlayersOnpathpointList.Contains(pathPoint))
        {
            PlayersOnpathpointList.Remove(pathPoint);
        }
    }
    public void rollingDiceTransfer()
    {
    
        if (transferDice)
        {
            transferRollingDice();
          
        }
        else
        {
            if (GameManager.gm.totalPlayerCanPlay == 1)
            {
                if (GameManager.gm.rollingDice == GameManager.gm.rollingDiceList[2])
                {
                    Invoke("role", 0.6f);
                }
            }
        }
        canDiceRoll = true;
        transferDice = false;
    }

    void role()
    {
        rollingDiceList[2].ComputerRoll();

    }
    void transferRollingDice()
    {
        if (GameManager.gm.totalPlayerCanPlay == 1)
        {
            if (rollingDice == rollingDiceList[0])
            {
                rollingDiceList[0].gameObject.SetActive(false);
                rollingDiceList[2].gameObject.SetActive(true);
                Invoke("role", 0.6f);
            }
            else
            {

                rollingDiceList[2].gameObject.SetActive(false);
                rollingDiceList[0].gameObject.SetActive(true);
            }

        }
       else if (GameManager.gm.totalPlayerCanPlay == 2)
        {
            if (rollingDice == rollingDiceList[0])
            {
                rollingDiceList[0].gameObject.SetActive(false);
                rollingDiceList[2].gameObject.SetActive(true);
            }
            else 
            {
        
                rollingDiceList[2].gameObject.SetActive(false);
                rollingDiceList[0].gameObject.SetActive(true);
            }
        }
       else if (GameManager.gm.totalPlayerCanPlay == 3)
        {
            int nextDice;
            for (int i = 0; i < 3; i++)
            {
                if (i == 2) { nextDice = 0; } else { nextDice = i + 1; }
                i = passout(i);
                if (rollingDice == rollingDiceList[i])
                {
                    rollingDiceList[i].gameObject.SetActive(false);
                    rollingDiceList[nextDice].gameObject.SetActive(true);
                }
            }

        }
       else if (GameManager.gm.totalPlayerCanPlay == 4)
        {
                int nextDice;
            for (int i = 0; i < rollingDiceList.Count; i++)
            {
                if (i == (rollingDiceList.Count - 1)) { nextDice = 0; } else { nextDice = i + 1; }
                i = passout(i);
                if (rollingDice == rollingDiceList[i])
                {
                    rollingDiceList[i].gameObject.SetActive(false);
                    rollingDiceList[nextDice].gameObject.SetActive(true);
                }
            }
        }
    }
    int passout(int i)
    {
        if (i == 0) { if (redOutPlayer == 4) { return i + 1; } }
        if (i == 1) { if (greenOutPlayer == 4) { return i + 1; } }
        if (i == 2) { if (yellowOutPlayer == 4) { return i + 1; } }
        if (i == 3) { if (blueOutPlayer == 4) { return i + 1; } }
        return i;
    }
}
