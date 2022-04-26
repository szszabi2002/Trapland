using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class KillOnTouch : MonoBehaviour
{
    public GameObject player;
    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    private bool hasEntered;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("SureKill") && !hasEntered)
        {
            hasEntered = true;
            DBManager.DeathCounter += 1;
            SoundEffectsManager.Instance.PlaySound(deathSound);
            LevelManager.Instance.Respawn();
            Destroy(player);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("SureKill"))
        {
            hasEntered = false;
        }
    }
}