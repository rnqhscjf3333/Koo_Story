using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Ending : MonoBehaviour
{
    public GameObject[] MainObject;
    public GameObject Boss;
    public GameObject Light;
    public Manager manager;

    public float LightColor;
    public string sentence;
    public Sprite sprite;

    public float position1;
    public float position2;
    public float position3;
    public bool isEnd;
    public bool isLight;

    void Awake()
    {
        if (GameManager.Instance.BlenderDieCount == 2)
        {
            MainObject[1].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

        Light.GetComponent<Light2D>().color = new Color(1, LightColor, LightColor);
        LightColor = 1-(-1/(Boss.transform.position.y));
        if(LightColor <= 0 && !isLight)
        {
            isLight = true;
            if (GameManager.Instance.BlenderDieCount == 3)
            {
                MainObject[0].GetComponent<Animator>().SetBool("2", true);
                Invoke("isEnding", 16.7f);
            }
            else if (GameManager.Instance.BlenderDieCount == 2)
            {
                MainObject[0].GetComponent<Animator>().SetBool("3", true);
                Invoke("isEnding", 16.7f);
            }
        }

        if (Input.GetButtonDown("Fire1") && isEnd)
        {
            SoundManager.instance.Click();
            Invoke("Go", 1);
            GameObject.Find("Manager").GetComponent<Manager>().Fade = 1;
            isEnd = false;
        }
    }
    void isEnding()
    {
        isEnd = true;
    }

    void Go()
    {
        GameManager.Instance.GotoMenu();
    }

}
