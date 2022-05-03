using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class ButtonManager : MonoBehaviour
{
    Button[] LevelButton;
    private void Awake()
    {
        LevelButton = new Button[transform.childCount];
        for (int i = 0; i < LevelButton.Length; i++)
        {
            LevelButton[i] = transform.GetChild(i).GetComponent<Button>();
            LevelButton[i].GetComponentInChildren<TextMeshProUGUI>().SetText((i + 1).ToString());
            if (i + 1 > DBManager.ReachedLevel)
            {
                LevelButton[i].interactable = false;
            }
        }
    }
}