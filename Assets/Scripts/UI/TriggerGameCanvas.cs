using System.Collections;
using UnityEngine;

public class TriggerGameCanvas : MonoBehaviour
{
    public static bool triggerCanvas = false;
    //public bool isDelayOver = false;

    public GameObject gameCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        triggerCanvas = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(triggerCanvas == true)
        {
            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3);
        //isDelayOver = true;
        gameCanvas.SetActive(true);
    }
}
