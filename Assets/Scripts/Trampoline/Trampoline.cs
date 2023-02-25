using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float jumpHeight;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player")
        {
            Rigidbody2D playerRigidBody = collision.GetComponent<Rigidbody2D>();
            anim.SetTrigger("jump");
            playerRigidBody.velocity = new Vector2(0f, jumpHeight);
        }
    }
}
