using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VIDE_Data;
using UnityEngine.SceneManagement;
public class DialogueUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        VD.OnEnd += End;
        VD.OnNodeChange += ChangeUI;
        VD.BeginDialogue(GetComponent<VIDE_Assign>());
    }

    bool ended;
    public string nextSceneName;
    void End(VD.NodeData data)
    {
        ended = true;
        VD.OnEnd -= End;
        VD.EndDialogue();
        SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator TypeEffect(Text a, string b)
    {
        a.text = "";
        if (!ended)
        {
            foreach (char t in VD.nodeData.comments[0])
            {
                yield return new WaitForSeconds(0.05f);
                a.text += t;
            }
        }
    }

    public void ButtonPress(int whichButton)
    {
        buttonsMain.SetActive(false);
        VD.nodeData.commentIndex = whichButton;
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
            buttonsText[i].text = "";
        }
        VD.Next();
    }
    public void MainPress()
    {
        VD.Next();
    }

    public GameObject[] buttons;
    public Text[] buttonsText;
    public Text mainText;
    public GameObject buttonsMain;
    public GameObject nametag;
    public GameObject ImagePrefab;
    public Transform parent;
    

    void ChangeUI(VD.NodeData data)
    {
        //?
        ImagePrefab.GetComponent<Image>().sprite = data.sprite;
        if (data.isPlayer)
        {
            nametag.SetActive(false);
            if (data.comments.Length == 1)
            {
                StartCoroutine(TypeEffect(mainText, data.comments[0]));
                //sound implement
            }
            else
            {
                buttonsMain.SetActive(true);
                for (int i = 0; i < data.comments.Length; i++)
                {
                    buttons[i].SetActive(true);
                    StartCoroutine(TypeEffect(buttonsText[i], data.comments[i]));
                }
            }
        }
        else
        {
            //sound implement
            nametag.SetActive(true);
            StartCoroutine(TypeEffect(mainText, data.comments[0]));
        }
    }

    void OnDisable()
    {
        VD.OnEnd -= End;
    }
}
