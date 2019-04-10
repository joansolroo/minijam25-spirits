using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

    [SerializeField] public CharacterController2D player;
    [SerializeField] public Follow playerFollow;

    void Start()
    {
        playerFollow.target = player;
    }
}
