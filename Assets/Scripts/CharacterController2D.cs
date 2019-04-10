using UnityEngine;
using System.Collections;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] public float hp = 4;
    [SerializeField] public int MAXHP = 6;
    [SerializeField] public float speed;             //Floating point variable to store the player's movement speed.
    int direction = 1;
    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    [SerializeField] Transform body;
    [SerializeField] Transform handLeft;
    [SerializeField] Transform handRight;

    [SerializeField] MouseAim[] aim;
    [SerializeField] Weapon[] weapon;
    [SerializeField] Weapon[] weapon1;
    [SerializeField] Weapon[] weapon2;
    [SerializeField] Weapon[] weapon3;
    [SerializeField] Weapon[] weapon4;
    [SerializeField] public int currentWeapon = -1;

    [SerializeField] SpriteRenderer[] sprites;
    [SerializeField] GameObject deadBody;
    float randomStartWiggling;
    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
        ChangeWeapon(currentWeapon);
        randomStartWiggling = Random.value * 10;
        SetColor(color);
    }

    public void Move(Vector2 _movement)
    {
        movement = _movement;
    }
    public void Push(Vector2 force)
    {
        //rb2d.velocity+=force;
    }
    Vector2 movement;

    public Vector2 aimAt;
    public void AimAt(Vector2 target)
    {
        Vector2 newAimAt = target - new Vector2(this.transform.position.x, this.transform.position.y);
        newAimAt = newAimAt.normalized * Mathf.Min(5,Mathf.Max(newAimAt.magnitude, 0.25f));
        if(currentWeapon == -1) { newAimAt *= 0.1f; }
        else if (currentWeapon < 2) { newAimAt *= 0.25f; }
        else if (currentWeapon == 2) { newAimAt *= 0.15f; }
        else if (currentWeapon == 3) { newAimAt *= 0.4f; }
        aimAt = Vector2.MoveTowards(aimAt, newAimAt, 10*Time.deltaTime);
        CheckWeapon();
        direction = target.x < transform.position.x ? -1 : 1;
        if (weapon != null && weapon.Length > 0)
        {
            aim[0].weapon = weapon[0];
            aim[0].AimAt(target);
            if (weapon.Length > 1)
            {
                aim[1].weapon = weapon[1];
                aim[1].AimAt(target);
            }
        }


    }
    public void Fire1()
    {
        CheckWeapon();
        if (weapon != null && weapon.Length > 0)
        {
            weapon[0].Fire();
        }
    }
    public void Fire2()
    {
        CheckWeapon();
        if (weapon != null && weapon.Length > 1)
        {
            weapon[1].Fire();
        }
    }
    void CheckWeapon()
    {
        if (currentWeapon == 0)
        {
            weapon = weapon1;
        }
        else if (currentWeapon == 1)
        {
            weapon = weapon2;
        }
        if (currentWeapon == 2)
        {
            weapon = weapon3;
        }
        if (currentWeapon == 3)
        {
            weapon = weapon4;
        }
        if (weapon != null)
        {
            if (weapon.Length == 1 && !weapon[0].IsActive()) weapon[0].SetActive(true);
            if (weapon.Length == 2 && !weapon[1].IsActive()) weapon[1].SetActive(true);
        }
    }


    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    public float d = 1;
    void FixedUpdate()
    {
        /*if (movement.x != 0)
        {
            direction = (int)Mathf.Sign(movement.x);
        }*/
        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.velocity =  movement*speed;
        
        //rb2d.AddForce(delta.normalized * Mathf.Min(delta.magnitude, speed) * d);
    }

    private void Update()
    {
        //wiggle and look at direction
        body.transform.localPosition = new Vector3(0, Mathf.Sin(Time.time * 5 + randomStartWiggling) * 0.025f, 0);

        body.transform.localEulerAngles = new Vector3(0, direction > 0 ? 0 : 180, -direction*movement.x * 10);
    }

    void ChangeWeapon(int newWeapon)
    {
        if (weapon != null)
        {
            if(weapon.Length==1)weapon[0].SetActive(false);
            if (weapon.Length == 2) weapon[1].SetActive(false);
        }
        currentWeapon = newWeapon;
        if (newWeapon == -1)
        {
            foreach (Weapon w in weapon2)
            {
                w.gameObject.SetActive(false);
            }
            foreach (Weapon w in weapon3)
            {
                w.gameObject.SetActive(false);
            }
            foreach (Weapon w in weapon4)
            {
                w.gameObject.SetActive(false);
            }
            foreach (Weapon w in weapon1)
            {
                w.gameObject.SetActive(false);
            }
            weapon = null;
        }
        else
        {
            foreach (Weapon w in weapon2)
            {
                w.gameObject.SetActive(currentWeapon == 1);
            }
            foreach (Weapon w in weapon3)
            {
                w.gameObject.SetActive(currentWeapon == 2);
            }
            foreach (Weapon w in weapon4)
            {
                w.gameObject.SetActive(currentWeapon == 3);
            }
            foreach (Weapon w in weapon1)
            {
                w.gameObject.SetActive(currentWeapon == 0 || currentWeapon == 1);
            }

        }
        CheckWeapon();
    }

    public void Damage(int damage)
    {
        if (!blinking)
        {
            hp -= damage * 0.5f;
            if (hp < 0)
            {
                hp = 0;
                Die();
            }
            else
            {
                StartCoroutine(Blink());
            }
        }


    }
    [SerializeField] public Color color;
    bool blinking = false;
    IEnumerator Blink()
    {
        if (!blinking)
        {
            blinking = true;
            float t = 0;
            while (t < 1)
            {

                SetColor(Color.Lerp(color, Color.red, t));
                yield return new WaitForEndOfFrame();
                t += Time.deltaTime * 30;
            }
            t = 0;
            while (t < 1)
            {
                SetColor(Color.Lerp(Color.red, color, t * 4));
                yield return new WaitForEndOfFrame();
                t += Time.deltaTime * 10;
            }
            SetColor(color);

            blinking = false;
        }
    }
    bool dead = false;
    void Die()
    {
        if (!dead)
        {
            dead = true;
            if (gameObject.tag == "Enemy")
            {
                PickableObject.PickableType dropType;
                if (currentWeapon == 0 || currentWeapon == 1)
                {
                    dropType = PickableObject.PickableType.pistol;
                }
                else if (currentWeapon == 2)
                {
                    dropType = PickableObject.PickableType.shotgun;
                }
                else if (currentWeapon == 3)
                {
                    dropType = PickableObject.PickableType.rifle;
                }
                else
                {
                    dropType = PickableObject.PickableType.booze;
                }
                DropManager.DropAt(dropType, this.transform.position);
            }
            if (gameObject.tag == "Player")
            {
                GameManager.Restart();
            }
            if (deadBody != null)
            {
                deadBody.SetActive(true);
                deadBody.transform.parent = this.transform.parent;
            }
             this.gameObject.SetActive(false);
        }
    }
    void SetColor(Color c)
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.color = c;
        }
    }
    public bool OnPickObject(PickableObject.PickableType type)
    {
        if (type == PickableObject.PickableType.pistol
            && currentWeapon!=1)
        {
            if (currentWeapon == 0)
            {
                ChangeWeapon(1);
                return true;
            }
            else
            {
                ChangeWeapon(0);
                return true;
            }
        }
        else if (type == PickableObject.PickableType.shotgun
             && currentWeapon != 2)
        {
            ChangeWeapon(2);
            return true;
        }
        else if (type == PickableObject.PickableType.rifle
             && currentWeapon != 3)
        {
            ChangeWeapon(3);
            return true;
        }
        else if (type == PickableObject.PickableType.booze)
        {
            if (hp < MAXHP)
            {
                hp = Mathf.Min(MAXHP, hp + 2);

                return true;
            }

        }
        else if (type == PickableObject.PickableType.horse)
        {
            GameManager.End();
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Vector3 crossAir = this.transform.position;
        crossAir.x += aimAt.x;
        crossAir.y += aimAt.y;
        Gizmos.DrawSphere(crossAir, 0.05f);
    }
}