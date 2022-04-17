using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;
public class Finish : MonoBehaviour
{
    public GameObject FinishMenu;
    public TMP_Text TimeText, DeathText;
    [SerializeField]public int reachedlevel = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            TimeText.text = "Time: " + DBManager.Time;
            DeathText.text = "Deaths: " + DBManager.DeathCounter;
            DBManager.ReachedLevel = reachedlevel;
            DBManager.Level = SceneManager.GetActiveScene().name;
            Time.timeScale = 0f;
            FinishMenu.SetActive(true);
            StartCoroutine(SavePlayerData());
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        DBManager.DeathCounter = 0;
    }
    public void NextLevel()
    {
        Debug.Log(DBManager.ReachedLevel);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void Awake()
    {
        if (!DBManager.LoggedIn)
        {
            SceneManager.LoadScene(0);
        }
    }
    public void CallSaveData()
    {
        
    }
    IEnumerator SavePlayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", DBManager.username);
        form.AddField("LevelPost", DBManager.Level);
        form.AddField("TimePost", DBManager.Time);
        form.AddField("DeathCounterPost", DBManager.DeathCounter);
        form.AddField("ReachedLevelPost", DBManager.ReachedLevel);
        UnityWebRequest request = UnityWebRequest.Post("https://trapland.000webhostapp.com/Savedata.php", form);
        yield return request.SendWebRequest();
        if (request.downloadHandler.text[0] == '0')
        {
            Debug.Log("Data Saved");
        }
        else
        {
            Debug.Log("Data not Saved, Error: #" + request.downloadHandler.text);
        }
        //DBManager.Logout();
    }
}
