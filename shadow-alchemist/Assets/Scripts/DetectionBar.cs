using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class DetectionBar : MonoBehaviour
{
    public int currentDetection;
    public int detectionTimer;
    public int thresholdTimer;
    public Slider slider;
    public GameObject GameOverUI;
    bool isDetectionLowered;
    bool isThresholdDetection;
    bool isCoroutineStarted;
    Coroutine thresholdCoroutine;

    // Start is called before the first frame update
    void Awake()
    {
        currentDetection = 150;
        detectionTimer = 5;
        thresholdTimer = 10;
        isDetectionLowered = false;
        isCoroutineStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentDetection >= 100 && isCoroutineStarted == false)
        {
            isCoroutineStarted = true;
            thresholdCoroutine = StartCoroutine(AboveThresholdDetection());
        }
        else if(currentDetection <= 100 && isCoroutineStarted == true)
        {
            StopCoroutine(thresholdCoroutine);
            isCoroutineStarted = false;
        }

        if(!isDetectionLowered)
        {
            Debug.Log("start");
            StartCoroutine(LowerDetection());
        }
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        slider.value = currentDetection;
    }

    public void UpdateDetection(int value)
    {
        currentDetection+=value;
    }

    public IEnumerator LowerDetection()
    {
        isDetectionLowered = true;
        yield return new WaitForSeconds(detectionTimer);
        Debug.Log("detection");
        if(currentDetection != 0)
        {
            currentDetection-=1;
        }
        isDetectionLowered = false;
    }

    public IEnumerator AboveThresholdDetection()
    {
        isThresholdDetection = true;
        yield return new WaitForSeconds(thresholdTimer);
        Debug.Log("threshold");
        if(currentDetection >= 100)
        {
            //EndGame
            Time.timeScale = 0;
            GameOverUI.SetActive(true);
            StopCoroutine(thresholdCoroutine);
        }
        isThresholdDetection = false;
    }
}