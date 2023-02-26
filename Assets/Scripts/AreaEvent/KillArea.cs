using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            Health playerHealth = collision.GetComponent<Health>();
            playerHealth.TakeDamage(playerHealth.currentHealth);
        }
    }
}
