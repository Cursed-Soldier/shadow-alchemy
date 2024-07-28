using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    
    public void BackToMain()
    {
        //Change Scene to Menu scene
        Debug.Log("Back To Menu");
        SceneManager.LoadScene("MainMenu");
    }
}
