using UnityEngine;
using UnityEngine.UI;

public class TextsLanguageSet : MonoBehaviour
{
    //All Set
    //This changes the texts elements texts to fit to language

    public Text changeThis;
    public DifferentLanguages writeThis;

    private void Awake()
    {
        if (writeThis == null)
        {
            Debug.LogError("Text Language Not Set");
            changeThis.text = "Error";
            return;
        }
        changeThis.text = writeThis.GetLang();
    }
}
