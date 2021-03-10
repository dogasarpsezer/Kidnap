using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Transform toTeleport;

    public void Teleport()
    {
        StartCoroutine(Tp());
    }
    IEnumerator Tp()
    {
        FadeCanvas.Fade(1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        #region CamAndCharReset
        GameObject character = GameObject.FindGameObjectWithTag("PlayerObj");
        if (character != null)
            GameObject.FindGameObjectWithTag("PlayerObj").transform.position = toTeleport.position;
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        cam.GetComponent<CameraFollowObj>().TP();
        #endregion
        yield return new WaitForSeconds(0.5f);
        FadeCanvas.Fade(-1, 0.5f);
        yield return new WaitForSeconds(0.7f);
    }
}
