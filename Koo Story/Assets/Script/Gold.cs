using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    public AudioClip[] clip;
    public int MonsterGold1;
    Rigidbody2D rigid;
    public int isGold;
    public Sprite[] GoldSpirte; //0:µ¿Àü1:µ·´Ù¹ß

    void OnEnable()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.AddForce(new Vector2(0, 1) * 2, ForceMode2D.Impulse);
    }
    void FixedUpdate()
    {

    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="Player" && isGold ==1)
        {
            SoundManager.instance.SFXPlay("Gold", clip[0]);
            Player player = collision.GetComponent<Player>();
            player.PlayerGold += MonsterGold1;
            isGold = -1;
            gameObject.SetActive(false);
        }
    }
    public void Gold2(int num)
    {
        MonsterGold1 = num;
        isGold = 1;
    }
}
