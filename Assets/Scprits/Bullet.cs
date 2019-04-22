using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 10;
    public bool isPlayerBullet = false;//是否是敌人的子弹

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //子弹始终是处于坦克的Y轴，所以用 transform.up 属性 去定位方向
        transform.Translate(transform.up*speed*Time.deltaTime,Space.World);
    }

    //触发器---的三个回调
    //物体接触时，回调一次
    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Tank":
                if (!isPlayerBullet)
                {
                    collision.SendMessage("die");
                    Destroy(gameObject);
                }
                break;
            case "Wall":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                break;
            case "Heart":
                collision.SendMessage("die");
                Destroy(gameObject);
                break;
            case "Enemy":
                if (isPlayerBullet)
                {
                    collision.SendMessage("die");
                    Destroy(gameObject);
                }
                break;
            case "Barriar":
                Destroy(gameObject);
                break;
            default:
                break;
        }
        
    }

    //物体接触时，持续回调
    public void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    //物体接触离开时，回调一次
    public void OnTriggerExit2D(Collider2D collision)
    {
        
    }

}
