using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VIDE_Data;
using UnityEngine.SceneManagement;

public class VideStoryBoard : MonoBehaviour
{
    public Text storyTeller;

    [Header("Sahne 1 Gerekli")]
    public GameObject disableWhenEmpty;
    public Image storyShower;

    [Header("Sahne 3 Gerekli")]
    public bool isFloat;//is monolog or any floating chat
    
    void Start()
    {
        FadeCanvas.Fade(-1,1);
        FadeCanvas.load = false;
        VD.OnEnd += End;
        VD.BeginDialogue(GetComponent<VIDE_Assign>());
        if (!isFloat)
            ChangeThings();
    }

    //this is the button of the door
    //also the end method of the storyboard
    public void GoToNextScene()
    {
        FadeCanvas.Fade(1, 1);
        MenuManager.atWhichScene++;
        MenuManager.Save();
        FadeCanvas.load = true;
        Invoke("SceneOpener", FadeCanvas.fadeDuration + 2f);
    }

    void SceneOpener() 
    {
        SceneManager.LoadScene(NextSceneName);
    }

    [HideInInspector]
    public bool coolDown = false;
    public void ChangeButton()
    {
        if (coolDown || ended || FadeCanvas.startedFade == true)
        {
            Debug.Log("Returned cooldown: " + coolDown + " Fade: " + FadeCanvas.startedFade);
            return;
        }
        else
            coolDown = true;

        VD.Next();
        if (isFloat)
        {
            TextBlockTransition();
        }
        else
        {
            FadeCanvas.Fade(1, 1);
            Invoke("ChangeThings", 1f);
        }
    }
    public void TextBlockTransition() 
    {
        StartCoroutine(Fade(true));
        StartCoroutine(TypeEffect());
    }
    public void ChangeThings()
    {
        if (ended)
            return;
        string tp = VD.nodeData.comments[0];
        if (tp[0] == '!')
        {
            disableWhenEmpty.SetActive(false);
            Invoke("Cooldown",1.2f);
        }
        else
        {
            disableWhenEmpty.SetActive(true);
            StartCoroutine(TypeEffect());
        }
        if (VD.nodeData.audios[0] != null)
        {
            MusicManager.current.OpenSound(VD.nodeData.audios[0]);
        }
        storyShower.sprite = VD.nodeData.sprite;
        FadeCanvas.Fade(-1, 1);
    }
    void Cooldown() 
    {
        coolDown = false;
    }
    IEnumerator TypeEffect()
    {
        if (!ended)
        {
            if (isFloat)
            {
                StartCoroutine(Fade(true));
                yield return new WaitForSeconds(0.7f);
            }
            else 
            {
                while (FadeCanvas.startedFade == false)
                {
                    yield return new WaitForSeconds(0.02f);
                }
            }
            storyTeller.text = "";
            if (VD.nodeData.audios[0] != null)
                MusicManager.SetSoundToPlay(VD.nodeData.audios[0]);
            foreach (char a in VD.nodeData.comments[0])
            {
                if (a == '!')
                {
                    break;
                }
                yield return new WaitForSeconds(0.05f);
                storyTeller.text += a;
            }
            if (isFloat) 
            {
                yield return new WaitForSeconds(0.7f);
                StartCoroutine(Fade(false));
            }
            else
            {
                coolDown = false;
            }
        }
    }
    [HideInInspector]
    public bool ended = false;
    public string NextSceneName;
    void End(VD.NodeData data)
    {
        ended = true;
        VD.OnEnd -= End;
        VD.EndDialogue();
        StartCoroutine(Fade(false));
        if (!isFloat)
        {
            FadeCanvas.Fade(-1, 1);
            GoToNextScene();
        }
    }

    public CanvasGroup scene3Window;
    public IEnumerator Fade(bool isIn) 
    {
        if (isIn)
        {
            scene3Window.alpha = 0;
            for (float i = 0; i < 1; i += 0.04f)
            {
                scene3Window.alpha = i;
                yield return new WaitForSeconds(0.03f);
            }
            scene3Window.alpha = 1;
            yield return new WaitForSeconds(0.05f);
        }
        else
        {
            scene3Window.alpha = 1;
            for (float i = 1; i >= 0; i -= 0.04f)
            {
                scene3Window.alpha = i;
                yield return new WaitForSeconds(0.03f);
            }
            scene3Window.alpha = 0;
            yield return new WaitForSeconds(0.05f);
            //in order to use the button again
            coolDown = false;
        }
    }

    void OnDisable()
    {
        VD.OnEnd -= End;
    }
}
