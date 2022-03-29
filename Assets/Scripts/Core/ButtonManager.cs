using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class ButtonManager : MonoBehaviour
{
    
    Button[] LevelButton;
    void Start()
    {
        
    }
    private void Awake()
    {
        int ReachedLevel = PlayerPrefs.GetInt("ReachedLevel", 1);
        if (PlayerPrefs.GetInt("Level") >= 2)
        {
            ReachedLevel = PlayerPrefs.GetInt("Level");
        }
        LevelButton = new Button[transform.childCount];
        for (int i = 0; i < LevelButton.Length; i++)
        {
            LevelButton[i] = transform.GetChild(i).GetComponent<Button>();
            LevelButton[i].GetComponentInChildren<TextMeshProUGUI>().SetText((i + 1).ToString());
            if (i + 1 > ReachedLevel)
            {
                LevelButton[i].interactable = false;
            }
        }
    }
}