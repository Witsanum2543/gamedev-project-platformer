using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private bool canDead;

    [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header ("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private CapsuleCollider2D headBoxCollider;

    [Header ("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Dizzy Parameter")]
    [SerializeField] private float dizzyDuration;
    private float dizzyTimer = Mathf.Infinity;

    [Header("Sound")]
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip jumpOnHead;

    // Reference
    private Animator anim;
    private Health playerHealth;

    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        // ATTACK ONLY WHEN PLAYER GET IN TOO CLOSE
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown && playerHealth.currentHealth > 0)
            {
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttack");
                SoundManager.instance.PlaySound(attackSound);
            }
        }

        if(enemyPatrol != null)
            // enemy patrol will be disable if player in sight
            enemyPatrol.enabled = !PlayerInSight();

        
        // If enemy is dizzy, dizzy for ... sec until it recovered.
        if (anim.GetBool("isDizzy") == true) {
            enemyPatrol.enabled = false;
            dizzyTimer += Time.deltaTime;
        }

        // check if dizzy end ?
        if(dizzyTimer > dizzyDuration) {
            anim.SetBool("isDizzy", false);
            enemyPatrol.enabled = true;
            dizzyTimer = 0;
        } 

    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 
            0,  Vector2.left, 0, playerLayer);

        if(hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    // DRAWING HITBOX
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            // Damge player health
            playerHealth.TakeDamage(damage);
        }
    }

    private void DisableAfterDeath() {
        gameObject.SetActive(false);
    }

    private bool hasCollide = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player")
        {
            SoundManager.instance.PlaySound(jumpOnHead);

            if(hasCollide == false){
                Rigidbody2D playerRigidBody = collision.GetComponent<Rigidbody2D>();
                hasCollide = true;
                
                // Stun enemy if enemy is not being dizzy
                if (anim.GetBool("isDizzy") == false || !canDead) {
                    anim.SetBool("isDizzy", true);
                    playerRigidBody.velocity = new Vector2(0f, 7f);
                }

                // If enemy already dizzy kill it
                else if (anim.GetBool("isDizzy") == true && canDead) {
                    
                    dizzyTimer = 0;
                    anim.SetBool("isDizzy", false);
                    GetComponent<Health>().dead();
                    playerRigidBody.velocity = new Vector2(0f, 7f);
                }
                StartCoroutine(ResetCollide(0.2f));
            }
        }
    }

    private IEnumerator ResetCollide(float second)
    {
        yield return new WaitForSeconds(second);
        hasCollide = false;
    }
}