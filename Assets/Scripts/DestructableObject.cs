﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour {

    [SerializeField] Color color;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] int hp;
    [SerializeField] PickableObject drop;

    void Start()
    {
        sprite.color = color;
    }
    void LateUpdate()
    {
        triggered = false;
    }
    bool triggered = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Debug.Log("hitted by " + collision.collider.name+"::"+ collision.gameObject.layer);
        if (collision.gameObject.layer == 10 && collision.gameObject.tag != this.tag)
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                if (!triggered)
                {
                    triggered = true;
                    Hurt(bullet.damage);
                }
            }
        }

    }
    void Hurt(int damage)
    {
        hp -= damage;
        if (hp > 0)
        {
            StartCoroutine(Blink());
        }
        else
        {
            Destroy();
        }
    }
    void Destroy()
    {
        if (drop != null)
        {

            PickableObject newDrop = GameObject.Instantiate<PickableObject>(drop);
            newDrop.transform.position = this.transform.position;
            newDrop.gameObject.SetActive(true);
            newDrop.transform.parent = drop.transform.parent;
            newDrop.transform.localScale = drop.transform.localScale;
        }
        Destroy(this.gameObject);
    }
    IEnumerator Blink()
    {
        float t = 0;
        while (t < 1)
        {
            sprite.color = Color.Lerp(color, Color.red, t );
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime*10;
        }
        t = 0;
        while (t < 1)
        {
            sprite.color = Color.Lerp(Color.red, color, t * 4);
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime*5;
        }
        sprite.color = color;
    }
}
