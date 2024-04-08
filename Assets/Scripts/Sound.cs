using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public  GameObject play;
    public  GameObject mute;

    public void OnMouseDown()
    {
        if (GameManager.gm.ads)
        {
            GameManager.gm.ads.gameObject.SetActive(false);
            play.SetActive(false);
            mute.SetActive(true);
        }
        else
        {
            GameManager.gm.ads.gameObject.SetActive(true);
            play.SetActive(true);
            mute.SetActive(false);

        }
    }

}
