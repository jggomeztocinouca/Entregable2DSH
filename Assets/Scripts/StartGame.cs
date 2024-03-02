using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMatch()
    {
        SceneManager.LoadScene("PreLevel1");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToLevel1()
    {
        SceneManager.LoadScene("PreLevel1");
    }
    public void GoToLevel2()
    {
        SceneManager.LoadScene("PreLevel2");
    }
    public void GoToLevel3()
    {
        SceneManager.LoadScene("PreLevel3");
    }
}
