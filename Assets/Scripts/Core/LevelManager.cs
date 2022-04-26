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
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }  
    }
    public void Respawn()
    {
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
            Death.text = "Death Counter: " + DBManager.DeathCounter.ToString();
            yield return null;
        }
    }
}