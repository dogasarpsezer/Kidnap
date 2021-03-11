using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
        soundSlider.value = PlayerPrefs.GetFloat("sound", 1);
        musicSlider.value = PlayerPrefs.GetFloat("music", 1);
        lang.value = PlayerPrefs.GetInt("lng", 0);
        for (int i = 0; i < 10; i++)
        {
            string name = "save" + i;
            int temps;
            temps = PlayerPrefs.GetInt(name + "scene", 1);
            if (temps != 1)
                emptySays[i].SetActive(false);
        }
        whichSaveStatic = 1;
    }
    public GameObject[] emptySays;

    #region settings

    public static int whichLanguage;
    public static float soundVol = 0.5f, musicVol = 0.5f;

    public Slider soundSlider, musicSlider;
    public Dropdown lang;

    public void SliderChange()
    {
        soundVol = soundSlider.value;
        musicVol = musicSlider.value;
        PlayerPrefs.SetFloat("music", musicVol);
        PlayerPrefs.SetFloat("sound", soundVol);
        MusicManager.current.VolumeSet();
        Debug.Log("set");
    }

    public void LanguageChange()
    {
        if (lang.value != whichLanguage)
        {
            whichLanguage = lang.value;
            PlayerPrefs.SetInt("lng", whichLanguage);
            SceneManager.LoadScene(0);
        }
    }

    #endregion

    #region caki

    public Animator caki; 
    public void CakiButton() 
    {
        caki.SetBool("selected", !caki.GetBool("selected"));
    }

    #endregion

    public void PickSave(int a) 
    {
        whichSaveStatic = a;
    }

    #region buttons

    public static int atWhichScene, pointsToWinHer,whichSaveStatic;

    public AudioClip phone;
    bool once = false;
    public void StartGame()
    {
        if (once) return;
        once = true;
        string name = "save" + whichSaveStatic;
        atWhichScene = PlayerPrefs.GetInt(name + "scene", 1);
        print(atWhichScene);
        pointsToWinHer = PlayerPrefs.GetInt(name + "scene", 0);
        MusicManager.SetSoundToPlay(phone);
        FadeCanvas.Fade(1,1);
        FadeCanvas.load = true;
        Invoke("ChangeScene", 4);
    }

    public static void Save()
    {
        string name = "save" + whichSaveStatic;
        PlayerPrefs.SetInt(name + "scene", atWhichScene);
        PlayerPrefs.SetInt(name + "scene", pointsToWinHer);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(atWhichScene);
    }
    #endregion

    #region pages;

    public GameObject[] pages;
    public int whichPageIsOpen = 0;
    public void OpenPage(int whichToOpen)
    {
        pages[whichToOpen].SetActive(true);
    }
    public void ClosePage(int whichToClose)
    {
        pages[whichToClose].SetActive(false);
    }

    #endregion

    #region exit
    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion
}

[System.Serializable]
public class DifferentLanguages
{
    //every language thing will use this class
    public string Eng, Tr;
    public string GetLang()
    {
        switch (MenuManager.whichLanguage)
        {
            case 0:
                return Eng;
            case 1:
                return Tr;
            default:
                return Eng;
        }
    }

}