using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
public class Register : MonoBehaviour
{
    public TMP_InputField nameField, passwordField;
    public Button submitButton;
    public TMP_Text Info;
    public GameObject InfoPrompt;
    public void CallRegister()
    {
        StartCoroutine(Registration());
    }
    IEnumerator Registration()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
        UnityWebRequest request = UnityWebRequest.Post("https://trapland.000webhostapp.com/Register.php", form);
        //UnityWebRequest request = UnityWebRequest.Post("http://localhost/Trapland/Register.php", form);
        yield return request.SendWebRequest();
        if (request.downloadHandler.text == "0")
        {
            InfoPrompt.SetActive(true);
            Info.SetText("User created successfully!", true);
        }
        else
        {
            InfoPrompt.SetActive(true);
            Info.SetText("User creation failed! \nError: #" + request.downloadHandler.text, true);
        }
    }
    public void VerifyInputs()
    {
        submitButton.interactable = (nameField.text.Length >= 3 && passwordField.text.Length >= 8);
    }
}
