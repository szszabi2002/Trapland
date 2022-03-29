using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class LevelManager : MonoBehaviour
{
    public Text Death;
    public static LevelManager Instance;
    public Transform respawnPoint;
    public GameObject playerPrefab;
    public CinemachineVirtualCameraBase cam;
    private void Awake() {
        Instance = this;
    }
    public void Respawn(){
        GameObject Player = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
        cam.Follow = Player.transform;
    }
    private void Start()
    {
        StartCoroutine("DeathCount");
    }
    IEnumerator DeathCount()
    {
        while (true)
        {
            Death.text = "Death Counter: " + GlobalVariable.DeathCounter.ToString();
            yield return null;
        }
    }

}
