using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [Header("Arrow Trap")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;

    [Header("Arrow")]
    [SerializeField] private float direction;
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    [SerializeField] private float damage;

    [Header("Sound")]
    [SerializeField] private AudioClip arrowSound;
    
    private float cooldownTimer;


    private void Attack() {
        cooldownTimer = 0;
        
        SoundManager.instance.PlaySound(arrowSound, SoundManager.instance.calculateVolume(transform.position, 1, 8, 0.8f));
        arrows[FindArrows()].transform.position = firePoint.position;
        arrows[FindArrows()].GetComponent<EnemyProjectile>().ActivateProjectile(direction, speed, resetTime, damage);
    }

    private int FindArrows() {
        for (int i = 0; i < arrows.Length; i++)
        {
            if(!arrows[i].activeInHierarchy) {
                return i;
            }
        }
        return 0;
    }

    private void Update() {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
            Attack();
    }
}
