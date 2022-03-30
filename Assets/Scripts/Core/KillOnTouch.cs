using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class KillOnTouch : MonoBehaviour
{
    public GameObject player;

    private bool hasEntered;
	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == ("SureKill") && !hasEntered) {
			hasEntered = true;
            DBManager.DeathCounter += 1;
			Destroy(player);
			LevelManager.Instance.Respawn();
		}
	}
}

