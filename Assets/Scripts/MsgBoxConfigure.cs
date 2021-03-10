using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MsgBoxConfigure : MonoBehaviour
{
    //phone message miniObj

    public Text myText;
    public GameObject[] sides;
    public void SetTheObjects(bool meSend, string toWrite)
    {
        myText.text = toWrite;
        if (meSend)
        {
            myText.alignment = TextAnchor.MiddleRight;
            sides[1].SetActive(true);
        }
        else
        {
            myText.alignment = TextAnchor.MiddleLeft;
            sides[0].SetActive(true);
        }
    }
}
