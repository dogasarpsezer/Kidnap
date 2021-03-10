using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCanvas : MonoBehaviour
{
    public static FadeCanvas mySelf;
    public GameObject myCanvas;
    public CanvasGroup myImage;
    public GameObject pauseObj;
    public GameObject Loading;
    public static bool alreadyStarted, load;
    void Start()
    {
        if (alreadyStarted) Destroy(gameObject); else mySelf = this;
        
        Invoke("St",0.1f);
    }
    private void St()
    {
        DontDestroyOnLoad(myCanvas);
        StartCoroutine(FadeOut());
    }

    public static Color myColor;
    public static bool startedFade;
    public static int fade;
    public static bool isOnColor;
    public static float fadeDuration = 1, stayInColor = 1;
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            pauseObj.SetActive(true);
        }
        if (load && !Loading.activeSelf)
        {
            Loading.SetActive(true);
        }
        else if (!load && Loading.activeSelf)
        {
            Loading.SetActive(false);
        }
    }

    public void ClosePause()
    {
        pauseObj.SetActive(false);
    }
    public static void Fade(int inOrOut, float time)
    {
        fadeDuration = time;
        fade = inOrOut;
        startedFade = true;
        mySelf.FadeStart();
    }

    public void FadeStart() 
    {
        if (fade > 0)
        {
            mySelf.StartCoroutine(FadeIn());
        }
        else
        {
            mySelf.StartCoroutine(FadeOut());
        }
    }
    public IEnumerator FadeIn()
    {
        float a = 0;
        for (int i = 0; i < fadeDuration * 50; i++)
        {
            a += 1 / (fadeDuration * 50);
            myImage.alpha = a;
            yield return new WaitForFixedUpdate();
        }
        a = 1;
        myImage.alpha = a;
        yield return new WaitForSeconds(0.1f);
        startedFade = false;
    }
    public IEnumerator FadeOut()
    {
        float a = 1;
        for (int i = 0; i < fadeDuration * 50; i++)
        {
            a -= 1 / (fadeDuration * 50);
            myImage.alpha = a;
            yield return new WaitForFixedUpdate();
        }
        a = 0;
        myImage.alpha = a;
        yield return new WaitForSeconds(0.1f);
        startedFade = false;
    }

}
