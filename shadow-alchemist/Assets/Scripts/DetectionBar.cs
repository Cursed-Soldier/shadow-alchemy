using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DetectionBar : MonoBehaviour
{
    public int currentDetection;
    public int detectionTimer;
    public int thresholdTimer;
    public Slider slider;
    public GameObject GameOverUI;
    public TMP_Text detectionAmountText;
    bool isDetectionLowered;
    bool isThresholdDetection;
    bool isCoroutineStarted;
    Coroutine thresholdCoroutine;
    
    // Start is called before the first frame update
    void Awake()
    {
        
        detectionAmountText.text = currentDetection.ToString();
       

        isDetectionLowered = false;
        isCoroutineStarted = false;
        Time.timeScale = 1;
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
        else if (currentDetection < 1)
        {
            currentDetection = 1;
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
            currentDetection+=1;
            detectionAmountText.text = currentDetection.ToString();
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
