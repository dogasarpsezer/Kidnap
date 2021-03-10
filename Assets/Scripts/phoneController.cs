using UnityEngine;
using UnityEngine.UI;
using VIDE_Data;
using UnityEngine.SceneManagement;
using System.Collections;

public class phoneController : MonoBehaviour
{
    public Transform parentForMessages;
    public GameObject msgBoxPrefab;

    int atWhichMessage = 0, whichKey = 0;

    public Text type;
    public string tmp = "", msg = "";
    public Text type2;
    public Button sendButton;

    private void Start()
    {
        FadeCanvas.load = false;
        FadeCanvas.Fade(-1, 1);
        VD.OnEnd += End;
        VD.BeginDialogue(GetComponent<VIDE_Assign>());
        msg = VD.nodeData.comments[0];
    }

    public string NextScene;
    public GameObject nextSceneGoButton;
    bool ended = false;
    void End(VD.NodeData data)
    {
        MenuManager.atWhichScene++;
        string name = "save" + MenuManager.whichSaveStatic;
        PlayerPrefs.SetInt(name + "scene", MenuManager.atWhichScene);
        PlayerPrefs.SetInt(name + "scene", MenuManager.pointsToWinHer);
        sendButton.interactable = false;
        Debug.Log("here");
        ended = true;
        VD.OnEnd -= End;
        VD.EndDialogue();
        Debug.Log("also here");
        GoNextScene();
    }
    public void GoNextScene()
    {
        FadeCanvas.fade = 1;
        Invoke("ChangeScene", FadeCanvas.fadeDuration + 2f);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(NextScene);
    }
    void OnDisable()
    {
        VD.OnEnd -= End;
    }

    public void TypeButton() 
    {
        
        if (isBudTyping == true || ended || startedTyping)
        {
            return;
        }
        type.text = "";
        StartCoroutine(Type());
    }
    bool startedTyping = false;
    public IEnumerator Type()
    {
        startedTyping = true;
        for (int i = 0; i < msg.Length; i++)
        {
            yield return new WaitForSeconds(0.05f);
            TypeYourself();
        }
        yield return new WaitForSeconds(0.2f);
        ButtonSendMsg();
        startedTyping = false;
    }

    bool isBudTyping;
    public void TypeYourself()
    {
        if (isBudTyping == true)
        {
            return;
        }
        if (whichKey == msg.Length)
        {
            sendButton.interactable = false;
            return;
        }
        type2.text += msg[whichKey++].ToString();
        //MusicManager.SetSoundToPlay(messageSound);
    }
    public AudioClip messageSound;

    public void ButtonSendMsg()
    {
        sendButton.interactable = false;
        VD.Next();
        StartCoroutine(Timed());
        type.text = "Enter text...";
    }
    IEnumerator Timed()
    {
        isBudTyping = true;
        GameObject msgTp = Instantiate(msgBoxPrefab, parentForMessages);
        msgTp.GetComponent<MsgBoxConfigure>().SetTheObjects(true, type2.text);
        type.text = "";
        type2.text = "";
        whichKey = 0;
        if (!ended)
        {
            while (!VD.nodeData.isPlayer)
            {
                yield return new WaitForSeconds(Random.Range(0.5f, 2f));
                msgTp = Instantiate(msgBoxPrefab, parentForMessages);
                msgTp.GetComponent<MsgBoxConfigure>().SetTheObjects(false, VD.nodeData.comments[0]);
                VD.Next();
                //play sound
                yield return new WaitForSeconds(0.01f);
                if (ended) break;
            }
            msg = VD.nodeData.comments[0];
        }
        isBudTyping = false;
    }
}