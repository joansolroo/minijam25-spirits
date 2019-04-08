using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour {

    public enum PickableType
    {
        pistol, shotgun, rifle, booze, horse

    }
    [SerializeField] PickableType objectType;

    Vector3 position;
    float randomStartWiggling;
    // Use this for initialization
    void Start()
    {
        position = transform.localPosition;
        randomStartWiggling = Random.value * 10;
    }
    void LateUpdate()
    {
        this.transform.localPosition = position + new Vector3(0, Mathf.Sin(Time.time * 6+ randomStartWiggling) *0.01f, 0);
        triggered = false;
    }
    bool triggered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggered)
        {
            triggered = true;
            if (collision.gameObject.tag == "Player")
            {
                CharacterController2D controller = collision.gameObject.GetComponent<CharacterController2D>();
                if (controller != null)
                {
                    bool success = controller.OnPickObject(objectType);
                    if (success)
                    {
                        Destroy();
                    }
                }
            }
        }
    }
    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
