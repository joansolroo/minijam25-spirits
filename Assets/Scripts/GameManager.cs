using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] Animation introduction;
    [SerializeField] float introductionDuration;

    [SerializeField] GameObject level;
    [SerializeField] GameObject currentLevel;
    [SerializeField] GameObject outro;
    [SerializeField] bool playing;

    static GameManager instance;

    [SerializeField] bool debug = false;
    void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start()
    {
        StartCoroutine(Introduction());
    }

    private void Update()
    {
        if(outro.activeSelf && Input.GetMouseButtonDown(0))
        {
            restart();
        }
    }

    int state = 0;
    // Update is called once per frame

    public static void End()
    {

        instance.outro.SetActive(true);
        instance.currentLevel.SetActive(false);
    }
    public static void Restart()
    {
        instance.StartCoroutine(instance.DoRestart());
    }

    void restart()
    {
        if (outro.activeSelf)
        {
            outro.SetActive(false);
        }
        if (currentLevel != null)
        {
            currentLevel.SetActive(false);
            GameObject.Destroy(currentLevel);
        }
        currentLevel = GameObject.Instantiate<GameObject>(level);
        currentLevel.SetActive(true);
        state = 1;
    }
    IEnumerator DoRestart()
    {
        yield return new WaitForSeconds(3);
        instance.restart();
    }
    IEnumerator Introduction()
    {
        if (!debug)
        {
            introduction.gameObject.SetActive(true);
            level.SetActive(false);
            yield return new WaitForSeconds(introductionDuration);

        }
        else
        {
            introduction.gameObject.SetActive(false);
            level.SetActive(false);
        }
        restart();
    }
}
