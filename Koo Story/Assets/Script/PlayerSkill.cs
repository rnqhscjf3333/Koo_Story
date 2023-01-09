using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public float speed;
    public float damage;
    public bool ishit; //Å¸°ÝÃ¼Å©
    public int HitCount; //¸î¹øÅ¸°Ý
    public int HitCount1;
    public int time; //Á¦ÇÑ½Ã°£
    public int Time1;
    public Sprite[] skillsprite;


    void OnEnable()
    {
        Time1 = time;
        HitCount1 = HitCount;
        damage = GameObject.Find("Player").GetComponent<Player>().Power;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        GetComponent<SpriteRenderer>().sprite = skillsprite[GameManager.Instance.Swordnum];
        
    }



    // Update is called once per frame
    void Update()
    {
        Time1 -= 1;

        if (Time1 <= 0)
        {
            gameObject.SetActive(false);
        }

        transform.Translate(Vector3.right * Time.deltaTime * speed * transform.localScale.x);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7 && ishit == false && HitCount >0) //ÇÃ·¹ÀÌ¾îÇÑÅ× ¸Â­ŸÀ»¶§
        {
            if (collision.GetComponent<Enemy>() != null)
                collision.GetComponent<Enemy>().onDamaged(damage);
            else if (collision.GetComponent<Boss1>() != null)
                collision.GetComponent<Boss1>().onDamaged(damage);
            HitCount1 -= 1;
            //ishit = true;
            Invoke("Ishit", 0.0001f);
            if(HitCount1 ==0)
                gameObject.SetActive(false);
        }
    }

    void Ishit()
    {
        ishit = false;
    }
}
