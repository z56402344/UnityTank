using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    //属性
    public float moveSpeed = 3;
    public Vector3 bullectEuAngle;
    public float timeVal = 0;
    public float defendTimeVal = 3;
    private bool isDefended = true;

    //引用
    private SpriteRenderer sr;//精灵管理器
    public Sprite[] tanksSprite;//上 右 下 左 精灵数组
    public GameObject bullect;
    public GameObject explodePrefab;
    public GameObject defendEffectPrefab;

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
        //无敌状态更新
        if (isDefended)
        {

            defendEffectPrefab.SetActive(true);
            defendTimeVal -= Time.deltaTime;
            print("000 <<< defendTimeVal=" + defendTimeVal);
            if (defendTimeVal <= 0)
            {
                print("111 <<< isDefended=" + isDefended);
                isDefended = false;
                defendEffectPrefab.SetActive(false);
            }
        }



    }

    //固定每秒多少帧，防止触碰边缘是发生的抖动
    public void FixedUpdate()
    {
        if(PlayerManager.Instans.isDefeat)
        {
            return;
        }
        move();
        //子弹的CD时间
        if (timeVal > 0.2)
        {
            attack();
        }
        else
        {
            timeVal += Time.fixedDeltaTime;
        }

    }

    //坦克移动
    public void move()
    {
        float v = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0)
        {
            sr.sprite = tanksSprite[2];
            bullectEuAngle = new Vector3(0,0,-180);
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

        float h = Input.GetAxisRaw("Horizontal");
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject.Instantiate(bullect,transform.position,Quaternion.Euler(transform.eulerAngles+ bullectEuAngle));
            timeVal = 0;
        }
    }


    //收到攻击后死亡逻辑
    public void die()
    {

        //被保护时，不受伤害直接return
        if (isDefended)
        {
            return;
        }
       
            PlayerManager.Instans.isDie = true;
        //展示爆炸效果
        Instantiate(explodePrefab,transform.position,transform.rotation);

        //销毁当前对象
        Destroy(gameObject);
    }

}
