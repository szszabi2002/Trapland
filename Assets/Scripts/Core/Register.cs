using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;
public class Register : MonoBehaviour
{
    public TMP_InputField nameField, passwordField;
    public Button submitButton;
    public TMP_Text Info;
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
            Info.SetText("User created successfully!");
            Info.SetText("");
        }
        else
        {
            Info.SetText("User creation failed! Error: #" + request.downloadHandler.text);
        }
    }
    public void VerifyInputs()
    {
        submitButton.interactable = (nameField.text.Length >= 8 && passwordField.text.Length >= 8);
    }
}
