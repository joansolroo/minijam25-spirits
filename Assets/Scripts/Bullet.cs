using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {

    public Rigidbody2D rb2D;
    public int velocity = 1;
    public int damage = 1;
    public float ttl = 10;

    public void Start()
    {
        ttl *= Random.Range(0.95f, 1.05f);
    }
    private void LateUpdate()
    {
        ttl -= Time.deltaTime;
        if(ttl <= 0)
        {
            DestroyBullet();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != this.gameObject.tag)
        {
            CharacterController2D controller =  collision.gameObject.GetComponent<CharacterController2D>();
            if(controller != null)
            {
                controller.Damage(damage);
            }
            DestroyBullet();
        }
        
    }

    void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}
