using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikehead : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private Vector3[] directions = new Vector3[4];
    private Vector3 destination;
    private Vector3 startdestination;
    private float checkTimer;
    private bool attacking;
    [Header("Impact Sound")]
    [SerializeField] private AudioClip impactSound;
    private void Awake()
    {
        startdestination = this.transform.position;
    }
    private void OnEnable()
    {
        Stop();
    }
    private void Update()
    {
        if (attacking)
        {
            transform.Translate(destination * Time.deltaTime * speed);
        }
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
            {
                CheckForPlayer();
            }
        }
    }
    private void CheckForPlayer()
    {
        CalculateDirections();
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);
            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }
    private void CalculateDirections()
    {
        directions[3] = -transform.up * range;
    }
    private void Stop()
    {
        this.transform.position = startdestination;
        attacking = false;
    }
    private new void OnTriggerEnter2D(Collider2D collision)
    {
        SoundEffectsManager.Instance.PlaySound(impactSound);
        base.OnTriggerEnter2D(collision);
        StartCoroutine(StopAttack());
    }
    IEnumerator StopAttack()
    {
        attacking = false;
        yield return new WaitForSeconds(0.1f);
        Stop();
    }
}
