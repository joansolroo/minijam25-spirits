using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(200)]
public class HPPanel : MonoBehaviour {

    [SerializeField] CharacterController2D player;
    [SerializeField] GameObject[] hpRenderers;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip hpUp;
    [SerializeField] AudioClip hpDown;

    int hp;
    // Use this for initialization
	void Start () {
        hp = (int)player.hp;
        UpdateBar();

    }
	
	// Update is called once per frame
	void LateUpdate () {
		if(hp!= (int)player.hp)
        {
            if(hp> (int)player.hp)
            {
                audioSource.PlayOneShot(hpDown);
            }
            if (hp < (int)player.hp)
            {
                audioSource.PlayOneShot(hpUp);
            }

            hp = (int)player.hp;
            UpdateBar();
        }
	}

    void UpdateBar()
    {
        for (int h = 0; h < hpRenderers.Length; ++h)
        {
            hpRenderers[h].SetActive((h == hp));
        }
    }
}
