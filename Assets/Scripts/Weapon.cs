using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] public int capacity;
    [SerializeField] public int load;
    [SerializeField] public float loadTime;
    [SerializeField] public bool reloadInterruptSupported = false;
    [Header("Shooting")]
    [SerializeField] public int bulletsPerShot = 1;
    [SerializeField] public float spread = 0;
    [SerializeField] public float cooldown = 0;

    [Header("Links")]
    [SerializeField] Bullet bulletPefab;
    [SerializeField] Transform nossle;
    [SerializeField] CharacterController2D controller;
    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clipFire;
    [SerializeField] AudioClip clipEmpty;
    [SerializeField] AudioClip clipReload;

    [SerializeField] int hand;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time-lastFire > 2)
        {
            if(load < capacity)
            {
                Reload();
            }
        }
    }

    public void SetActive(bool active)
    {
        if (active == false)
        {
            firing = false;
            reloading = false;
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
    bool failShot = false;
    float lastFire = 0;
    public void Fire()
    {
        if (load > 0)
        {
            StartCoroutine(DoFire());
            lastFire = Time.time;
        }
        else
        {
            // if (failShot)
            {
                Reload();
                //   failShot = false;
            }/*
            else
            {
                failShot = true;
                audioSource.PlayOneShot(clipEmpty);
            }*/
        }
    }
    public bool firing = false;
    IEnumerator DoFire()
    {
        if (!firing)
        {

            firing = true;
            if (reloading && reloadInterruptSupported)
            {
                CancelReload();
            }
            if (!reloading)
            {
                for (int c = 0; c < bulletsPerShot; ++c)
                {
                    Bullet b = GameObject.Instantiate<Bullet>(bulletPefab);
                    b.tag = controller.tag;
                    b.gameObject.SetActive(true);
                    b.transform.position = nossle.position;
                    b.transform.rotation = nossle.rotation;
                    b.transform.RotateAround(nossle.position, Vector3.forward, Random.Range(-spread, spread));
                    b.rb2D.velocity = b.transform.right * b.velocity * Random.Range(0.8f, 1.2f);
                }
                --load;
                audioSource.PlayOneShot(clipFire);
                float t = 0;
                float r = 0;
                while (t < cooldown / 2)
                {
                    r = Mathf.MoveTowards(r, 30, 30 * cooldown / 2);
                    this.transform.parent.localEulerAngles = new Vector3(0, 0, r);
                    yield return new WaitForEndOfFrame();
                    t += Time.deltaTime;

                } while (t < cooldown)
                {
                    r = Mathf.MoveTowards(r, 0, 30 * cooldown / 2);
                    this.transform.parent.localEulerAngles = new Vector3(0, 0, r);
                    yield return new WaitForEndOfFrame();
                    t += Time.deltaTime;

                }
                this.transform.parent.localEulerAngles = new Vector3(0, 0, 0);
            }
            firing = false;
        }
    }


    public void Reload()
    {
        StartCoroutine(DoReload());
    }
    void CancelReload()
    {
        StopCoroutine(DoReload());
        reloading = false;
    }
    public bool reloading = false;

    IEnumerator DoReload()
    {
        if (!reloading)
        {
            reloading = true;
            while (load < capacity && reloading)
            {
                //yield return new WaitForSeconds(loadTime/2);
                if (reloading)
                {
                    audioSource.PlayOneShot(clipReload);
                    load++;
                    yield return new WaitForSeconds(loadTime );
                }
            }
            reloading = false;
        }
    }
}
