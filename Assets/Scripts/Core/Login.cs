using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class Login : MonoBehaviour
{
    public TMP_InputField nameField, passwordField;
    public Button submitButton;
    public TMP_Text Info;
    public GameObject InfoPrompt, OkButton, PlayButton;
    public int InputSelected;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            InputSelected--;
            if (InputSelected < 0) InputSelected = 1;
            SelectInputField();
        }

        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            InputSelected++;
            if (InputSelected > 1) InputSelected = 0;
            SelectInputField();
        }
        void SelectInputField()
        {
            switch (InputSelected)
            {
                case 0:
                    nameField.Select();
                    break;
                case 1:
                    passwordField.Select();
                    break;
            }
        }
    }
    public void nameFieldSelected() => InputSelected = 0;
    public void passFieldSelected() => InputSelected = 1;
    public void CallLogin()
    {
        StartCoroutine(login());
    }
    IEnumerator login()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", nameField.text);
        form.AddField("password", passwordField.text);
        UnityWebRequest request = UnityWebRequest.Post("https://trapland.000webhostapp.com/Login.php", form);
        yield return request.SendWebRequest();
        if (request.downloadHandler.text[0] == '0')
        {
            DBManager.username = nameField.text;
            print("Username: " + DBManager.username);
            DBManager.ReachedLevel = int.Parse(request.downloadHandler.text.Split('\t')[1]);
            InfoPrompt.SetActive(true);
            PlayButton.SetActive(true);
            Info.SetText("Logged in as " + DBManager.username, true);
        }
        else
        {
            InfoPrompt.SetActive(true);
            OkButton.SetActive(true);
            Info.SetText("User login failed! \nError: #" + request.downloadHandler.text, true);
        }
    }
    public void VerifyInputs()
    {
        submitButton.interactable = (nameField.text.Length >= 3 && passwordField.text.Length >= 8);
    }
}
