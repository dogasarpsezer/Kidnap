using System.Collections;
using UnityEngine;

public class CameraFollowObj : MonoBehaviour
{
    public Rigidbody2D myrigidb;
    public GameObject toFollow;
    public Vector3 camOffset;
    void Start()
    {
        StartCoroutine(Follow());
    }

    IEnumerator Follow()
    {
        //this way instead of transform making it chubby it makes everything smoother 
        while (toFollow == null)
        {
            yield return new WaitForSeconds(0.02f);
        }
        Vector3 placeToGo = toFollow.transform.position;
        placeToGo = placeToGo - transform.position;
        myrigidb.velocity = placeToGo * 5 + camOffset;
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(Follow());
    }
    public void TP()
    {
        transform.position = toFollow.transform.position;
    }
}
