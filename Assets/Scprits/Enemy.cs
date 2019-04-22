using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //属性
    public float moveSpeed = 3;
    public Vector3 bullectEuAngle;
    public float timeVal = 0;
    public float defendTimeVal = 3;
    private float v = -1;//垂平方向
    private float h;//水平方向
    private float timeValChangeMove = 0;

    //引用
    private SpriteRenderer sr;//精灵管理器
    public Sprite[] tanksSprite;//上 右 下 左 精灵数组
    public GameObject bullect;
    public GameObject explodePrefab;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //子弹的间隔时间
        if (timeVal > 2)
        {
            attack();
        }
        else
        {
            timeVal += Time.deltaTime;
            //print("子弹间隔 >>> timeVal=" + timeVal);
        }
    }

    //固定每秒多少帧，防止触碰边缘是发生的抖动
    public void FixedUpdate()
    {
        move();

    }

    //坦克移动
    public void move()
    {
        //每timeValChangeMove秒转向一次，
        //通过随机数实现AI电脑自动移动逻辑
        if (timeValChangeMove > 3)
        {
            int num = Random.Range(0, 8);
            if (num >= 5)
            {
                v = -1;
                h = 0;
            }
            else if (num == 0)
            {
                v = 1;
                h = 0;
            }
            else if (num > 0 && num <= 2)
            {
                v = 0;
                h = -1;
            }
            else if (num > 2 && num <= 4)
            {
                v = 0;
                h = 1;
            }
            timeValChangeMove = 0;
        }
        else
        {
            timeValChangeMove += Time.fixedDeltaTime;
        }


        //v = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0)
        {
            sr.sprite = tanksSprite[2];
            bullectEuAngle = new Vector3(0, 0, -180);
        }
        else if (v > 0)
        {
            sr.sprite = tanksSprite[0];
            bullectEuAngle = new Vector3(0, 0, 0);
        }


        if (v != 0)
        {
            //防止用户上下左右一起按的bug
            return;
        }

        //h = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0)
        {
            sr.sprite = tanksSprite[3];
            bullectEuAngle = new Vector3(0, 0, 90);
        }
        else if (h > 0)
        {
            sr.sprite = tanksSprite[1];
            bullectEuAngle = new Vector3(0, 0, -90);
        }

    }

    //子弹攻击
    public void attack()
    {
        //print("发射子弹 >>>");
        GameObject.Instantiate(bullect, transform.position, Quaternion.Euler(transform.eulerAngles + bullectEuAngle));
        timeVal = 0;
      
    }


    //收到攻击后死亡逻辑
    public void die()
    {
        //展示爆炸效果
        Instantiate(explodePrefab, transform.position, transform.rotation);
        PlayerManager.Instans.score++;
        print("PlayerManager.Instans.score="+ PlayerManager.Instans.score);
        //销毁当前对象
        Destroy(gameObject);
    }

    //电脑AI优化逻辑，当电脑触碰到电脑或者不可销毁的障碍时，直接调用转向
    public void OnCollisionEnter2D(Collision2D collision)
    {
        print("OnCollisionEnter2D >>>");
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Barriar" || collision.gameObject.tag == "Barriar")
        {
            timeValChangeMove = 4;
        }
    }

}
