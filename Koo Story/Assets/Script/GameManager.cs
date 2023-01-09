using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; //시네마신
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour //파괴되면 안됨
{
    public static GameManager Instance;
    public SoundManager soundmanager;
    public bool[] Chest;//상자
    public int[] Key; //촌장집열쇠
    public int[] Quest;

    public int[] Swordhave;
    public int[] Armorhave;
    public int[] Skillhave;
    public int Swordnum;
    public int Armornum;
    public int Gold;

    public int StartPoint;

    public float Soundval1;
    public float Soundval2;

    public Sprite[] ArmorImage;
    public int BlenderDieCount;


    void Awake()
    {
        Chest = new bool[20];
        if (Instance != null) //중복파괴
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        //Load();
    }

    
    public void Save()
    {
        Soundval1 = soundmanager.val1;
        Soundval2 = soundmanager.val2;

        Debug.Log("Save");
        PlayerPrefs.SetInt("Gold", Gold);
        PlayerPrefs.SetInt("Swordnum", Swordnum);
        PlayerPrefs.SetInt("Armornum", Armornum);

        PlayerPrefs.SetFloat("Soundval1", Soundval1);
        PlayerPrefs.SetFloat("Soundval2", Soundval2);

        string strArr = ""; // 문자열 생성
        for (int i = 0; i < Chest.Length; i++) // 배열과 ','를 번갈아가며 tempStr에 저장
        {
            strArr = strArr + Chest[i];
            if (i < Chest.Length - 1) // 최대 길이의 -1까지만 ,를 저장
                strArr = strArr + ",";
        }
        PlayerPrefs.SetString("Chest", strArr);

        strArr = ""; // 문자열 생성
        for (int i = 0; i < Key.Length; i++) // 배열과 ','를 번갈아가며 tempStr에 저장
        {
            strArr = strArr + Key[i];
            if (i < Key.Length - 1) // 최대 길이의 -1까지만 ,를 저장
                strArr = strArr + ",";
        }
        PlayerPrefs.SetString("Key", strArr);

        strArr = ""; // 문자열 생성
        for (int i = 0; i < Quest.Length; i++) // 배열과 ','를 번갈아가며 tempStr에 저장
        {
            strArr = strArr + Quest[i];
            if (i < Quest.Length - 1) // 최대 길이의 -1까지만 ,를 저장
                strArr = strArr + ",";
        }
        PlayerPrefs.SetString("Quest", strArr);

        strArr = ""; // 문자열 생성
        for (int i = 0; i < Swordhave.Length; i++) // 배열과 ','를 번갈아가며 tempStr에 저장
        {
            strArr = strArr + Swordhave[i];
            if (i < Swordhave.Length - 1) // 최대 길이의 -1까지만 ,를 저장
                strArr = strArr + ",";
        }
        PlayerPrefs.SetString("Swordhave", strArr);

        strArr = ""; // 문자열 생성
        for (int i = 0; i < Armorhave.Length; i++) // 배열과 ','를 번갈아가며 tempStr에 저장
        {
            strArr = strArr + Armorhave[i];
            if (i < Armorhave.Length - 1) // 최대 길이의 -1까지만 ,를 저장
                strArr = strArr + ",";
        }
        PlayerPrefs.SetString("Armorhave", strArr);

        strArr = ""; // 문자열 생성
        for (int i = 0; i < Skillhave.Length; i++) // 배열과 ','를 번갈아가며 tempStr에 저장
        {
            strArr = strArr + Skillhave[i];
            if (i < Skillhave.Length - 1) // 최대 길이의 -1까지만 ,를 저장
                strArr = strArr + ",";
        }
        PlayerPrefs.SetString("Skillhave", strArr);




    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("Gold"))
        {
            Debug.Log("Load");
            Swordnum = PlayerPrefs.GetInt("Swordnum");
            Armornum = PlayerPrefs.GetInt("Armornum");
            if (GameObject.Find("Player") != null)
            {
                Player player = GameObject.Find("Player").GetComponent<Player>();
                player.ArmorInven(Armornum);
                player.Defense = GameObject.Find("Manager").GetComponent<Manager>().ArmorDefense[Armornum];
                player.SwordInven(Swordnum);
                player.Power = GameObject.Find("Manager").GetComponent<Manager>().SwordPower[Swordnum];
            }
            Gold = PlayerPrefs.GetInt("Gold");

            if (GameObject.Find("Manager") != null)
            {
                GameObject.Find("Manager").GetComponent<Manager>().PlayerGold = Gold;
            }

            Soundval1 = PlayerPrefs.GetFloat("Soundval1");
            Soundval2 = PlayerPrefs.GetFloat("Soundval2");

            soundmanager.val1 = Soundval1;
            soundmanager.val2 = Soundval2;


            string[] dataArr = PlayerPrefs.GetString("Chest").Split(','); // PlayerPrefs에서 불러온 값을 Split 함수를 통해 문자열의 ,로 구분하여 배열에 저장
            for (int i = 0; i < dataArr.Length; i++)
            {
                Chest[i] = System.Convert.ToBoolean(dataArr[i]); // 문자열 형태로 저장된 값을 정수형으로 변환후 저장
            }

            dataArr = PlayerPrefs.GetString("Key").Split(','); // PlayerPrefs에서 불러온 값을 Split 함수를 통해 문자열의 ,로 구분하여 배열에 저장
            for (int i = 0; i < dataArr.Length; i++)
            {
                Key[i] = System.Convert.ToInt32(dataArr[i]); // 문자열 형태로 저장된 값을 정수형으로 변환후 저장
            }

            dataArr = PlayerPrefs.GetString("Quest").Split(','); // PlayerPrefs에서 불러온 값을 Split 함수를 통해 문자열의 ,로 구분하여 배열에 저장
            for (int i = 0; i < dataArr.Length; i++)
            {
                Quest[i] = System.Convert.ToInt32(dataArr[i]); // 문자열 형태로 저장된 값을 정수형으로 변환후 저장
            }

            dataArr = PlayerPrefs.GetString("Swordhave").Split(','); // PlayerPrefs에서 불러온 값을 Split 함수를 통해 문자열의 ,로 구분하여 배열에 저장
            for (int i = 0; i < dataArr.Length; i++)
            {
                Swordhave[i] = System.Convert.ToInt32(dataArr[i]); // 문자열 형태로 저장된 값을 정수형으로 변환후 저장
            }

            dataArr = PlayerPrefs.GetString("Armorhave").Split(','); // PlayerPrefs에서 불러온 값을 Split 함수를 통해 문자열의 ,로 구분하여 배열에 저장
            for (int i = 0; i < dataArr.Length; i++)
            {
                Armorhave[i] = System.Convert.ToInt32(dataArr[i]); // 문자열 형태로 저장된 값을 정수형으로 변환후 저장
            }

            dataArr = PlayerPrefs.GetString("Skillhave").Split(','); // PlayerPrefs에서 불러온 값을 Split 함수를 통해 문자열의 ,로 구분하여 배열에 저장
            for (int i = 0; i < dataArr.Length; i++)
            {
                Skillhave[i] = System.Convert.ToInt32(dataArr[i]); // 문자열 형태로 저장된 값을 정수형으로 변환후 저장
            }
        }
    }

    public void GotoMenu()
    {
        SceneManager.LoadScene("Main");
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
