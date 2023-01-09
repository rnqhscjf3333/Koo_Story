using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    GameManager gamemanager;
    public SoundManager soundmanager;
    public AudioClip[] clip;
    int num;
    public bool isMenu;
    public Slider[] slider;

    public GameObject[] GB;
    public Text[] TT;

    void Awake()
    {
        //soundmanager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        isMenu = true;
    }
    void Update()
    {
        if(!isMenu)
            Time.timeScale = 1;
        if (isMenu && GameObject.Find("DungeonManager") != null)
            Time.timeScale = 1;
    }
    void OnEnable()
    {
        slider[0].value = soundmanager.val1;
        slider[1].value = soundmanager.val2;
        SoundManager.instance.Click();
    }
    void OnDisable()
    {
        SoundManager.instance.Click();
    }
    public void Load()
    {
        if (isMenu && GameObject.Find("DungeonManager") == null)
        {
            SoundManager.instance.Click2();
            gamemanager.Load();

            slider[0].value = soundmanager.val1;
            slider[1].value = soundmanager.val2;
            Naration("불러오기 완료!");
        }
        else
            Naration1("던전에서는 불러오기 할 수 없습니다.");
    }

    public void Save()
    {
        if (isMenu && GameObject.Find("DungeonManager") == null)
        {
            SoundManager.instance.Click2();
            gamemanager.Save();
            Naration("저장 완료!");
        }
        else
            Naration1("던전에서는 저장할 수 없습니다.");
    }
    public void Exit()
    {
        if (isMenu)
        {
            SoundManager.instance.Click();
            num = 0;
            Time.timeScale = 1;
            Invoke("Go", 1);
            GameObject.Find("Manager").GetComponent<Manager>().Fade = 1;
            isMenu = false;
            Application.Quit();
        }
    }

    public void Main()
    {
        if (isMenu)
        {
            SoundManager.instance.Click();
            num = 1;
            Time.timeScale = 1;
            Invoke("Go", 1);
            GameObject.Find("Manager").GetComponent<Manager>().Fade = 1;
            isMenu = false;
        }
    }

    public void Replay()
    {
        if (isMenu)
        {
            gamemanager.StartPoint = 0;
            SoundManager.instance.Click();
            num = 2;
            Time.timeScale = 1;
            Invoke("Go", 1);
            GameObject.Find("Manager").GetComponent<Manager>().Fade = 1;
            isMenu = false;
        }
    }
    public void MAP()
    {
        if (isMenu)
        {
            SoundManager.instance.Click();
            num = 3;
            Time.timeScale = 1;
            Invoke("Go", 1);
            GameObject.Find("Manager").GetComponent<Manager>().Fade = 1;
            isMenu = false;
        }
    }

    void Go()
    {
        if (num == 0)
            gamemanager.GameExit();
        else if (num == 1)
            gamemanager.GotoMenu();
        else if (num == 2)
            GameObject.Find("Manager").GetComponent<Manager>().PlayerDie1();
        else if (num == 3)
            GameObject.Find("Manager").GetComponent<Manager>().GOTOMAP();
    }

    public void BGSoundVolume(float val)
    {
        soundmanager.val1 = val;
        soundmanager.BGSoundVolume(val);
    }

    public void SFXVolume(float val)
    {
        soundmanager.val2 = val;
        soundmanager.SFXVolume(val);
    }
    public void Help()
    {
        SoundManager.instance.Click();
        GB[0].SetActive(true);
    }
    public void NotHelp()
    {
        SoundManager.instance.Click();
        GB[0].SetActive(false);
    }

    public void Naration(string sentence)
    {
        GB[1].SetActive(true);
        TT[0].text = sentence;
        Invoke("NarationEnd", 1f);
    }
    public void Naration1(string sentence)
    {
        GB[2].SetActive(true);
        TT[1].text = sentence;
        Invoke("NarationEnd1", 1f);
    }
    public void NarationEnd()
    {
        GB[1].SetActive(false);
    }
    public void NarationEnd1()
    {
        GB[2].SetActive(false);
    }
}
