using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject gamePanel;

    public void OnMouseDown()
    {
        mainPanel.SetActive(true);
        gamePanel.SetActive(false);
    }
}
