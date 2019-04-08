using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

    [SerializeField] public GameObject player;
    [SerializeField] public Follow playerFollow;

    void Start()
    {
        playerFollow.target = player.transform;
    }
}
