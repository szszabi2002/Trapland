using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void QuitGame(){
        Debug.Log("QUIT!");
        Application.Quit();
    }
    public void Logout()
    {
        DBManager.Logout();
        SceneManager.LoadScene(0);
    }
}
