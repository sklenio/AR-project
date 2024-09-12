using UnityEngine;
using System.Collections;


public class DisableTimer : MonoBehaviour
{
    public float disableTime = 5f;

    void Start()
    {
        StartCoroutine(DisableObject());
    }

    private IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(disableTime);

        gameObject.SetActive(false);
    }
}
