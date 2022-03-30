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
    public GameObject InfoPrompt;
    public void CallLogin()
    {
        StartCoroutine(login());
    }
    IEnumerator login()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
        UnityWebRequest request = UnityWebRequest.Post("https://trapland.000webhostapp.com/Login.php", form);
        yield return request.SendWebRequest();
        if (request.downloadHandler.text[0] == '0')
        {
            DBManager.username = nameField.text;
            DBManager.DeathCounter = int.Parse(request.downloadHandler.text.Split('\t')[1]);
        }
        else
        {
            InfoPrompt.SetActive(true);
            Info.SetText("User login failed! \nError: #" + request.downloadHandler.text, true);
        }
    }
    public void VerifyInputs()
    {
        submitButton.interactable = (nameField.text.Length >= 3 && passwordField.text.Length >= 8);
    }
}
