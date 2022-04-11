using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Finish : MonoBehaviour
{
    public GameObject FinishMenu;
    public TMP_Text TimeText, DeathText;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            TimeText.text = "Time: " + DBManager.Time;
            DeathText.text = "Deaths: " + DBManager.DeathCounter;
            Time.timeScale = 0f;
            FinishMenu.SetActive(true);
        }
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
        DBManager.DeathCounter = 0;
    }
    public void NextLevel()
    {
        DBManager.ReachedLevel += 1;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
