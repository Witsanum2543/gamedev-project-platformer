using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    // { get; private set; } mean can public get but set be private
    public float currentHealth { get; private set; }
    private Animator anim;

    [Header ("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header ("Components")]
    [SerializeField] private Behaviour[] components;

    [Header("Sound")]
    [SerializeField] private AudioClip diedSound;
    [SerializeField] private AudioClip hurtSound;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        
        if (currentHealth <= 0) {
            anim.SetTrigger("die");
                
            foreach (Behaviour component in components) {
                component.enabled = false;
            }

            SoundManager.instance.PlaySound(diedSound);

        } else {
            anim.SetTrigger("hurt");
            //iframes
            StartCoroutine(Invunerability());
            SoundManager.instance.PlaySound(hurtSound);
        }

    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invunerability()
    {
        // Player on layer 8 and enemy on layer 0
        Physics2D.IgnoreLayerCollision(8, 0, true);

        // invunerabity duration
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 0, false);
    }

    // ************************** GETTER AND SETTER ***********************

    public void setHealth(float health) {
        currentHealth = health;
    }

    public void dead() {
        TakeDamage(currentHealth);
    }

}
