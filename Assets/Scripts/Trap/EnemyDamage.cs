using System.Collections;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    private bool hasCollide = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            if(hasCollide == false){
              hasCollide = true;
              collision.GetComponent<Health>().TakeDamage(damage);
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
