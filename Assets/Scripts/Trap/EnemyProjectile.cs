using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour // Will damage player every time they touch
{
    private float speed;
    private float resetTime;
    private float lifetime;
    private float direction;
    private float damage;

    private Vector3 arrowInitScale;

    public void ActivateProjectile(float _direction, float _speed, float _resetTime, float _damage){
        lifetime = 0;
        gameObject.SetActive(true);
        direction = _direction;
        speed = _speed;
        resetTime = _resetTime;
        damage = _damage;

        arrowInitScale = GetComponent<Transform>().localScale;
        GetComponent<Transform>().localScale = new Vector3(Mathf.Abs(arrowInitScale.x) * _direction,
            arrowInitScale.y, arrowInitScale.z);
    }

    private void Update() {
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime) {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            collision.GetComponent<Health>().TakeDamage(damage);
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(false);
        }
    }

}
