using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//地图生成器
public class MapController : MonoBehaviour
{
    //0.老家；1=墙；2=障碍；3=出生效果；4=河流；5=草；6=空气墙；
    public GameObject[] items;
    private List<Vector3> list = new List<Vector3>();

    public void Awake()
    {
        //创建老家
        CrateItem(0,new Vector3(0,-8,0));
        //创建老家周围的墙
        CrateItem(1, new Vector3(1, -8, 0));
        CrateItem(1, new Vector3(-1, -8, 0));
        for (int i = -1; i <= 1; i++)
        {
            CrateItem(1, new Vector3(i, -7, 0));
        }
        for (int i = -11; i< 12;i++)
        {
            CrateItem(6, new Vector3(i, 9, 0));
            CrateItem(6, new Vector3(i, -9, 0));
        }
        for (int i = -9; i < 9; i++)
        {
            CrateItem(6, new Vector3(-11, i, 0));
            CrateItem(6, new Vector3(11, i, 0));
        }

        //创建地图中的其他元素
        for (int i = 0; i < 20; i++)
        {
            CrateItem(1, CreateRandomItem());
            if (i<=20)
            {
                CrateItem(2, CreateRandomItem());
                CrateItem(4, CreateRandomItem());
                CrateItem(5, CreateRandomItem());
            }
        }

        //创建玩家
        GameObject player = Instantiate(items[3],new Vector3(2,-8,0),Quaternion.identity);
        player.GetComponent<Born>().isCreatPlayer = true;
        //初始化创建敌人
        CrateItem(3, new Vector3(0, 8, 0));
        CrateItem(3, new Vector3(-10, 8, 0));
        CrateItem(3, new Vector3(10, 8, 0));

        //循环创建敌人
        InvokeRepeating("CreatEnemy",4,5);
    }

    //创建地图中元素的方法
    private void CrateItem(int index,Vector3 v3)
    {
        GameObject obj = Instantiate(items[index], v3, Quaternion.identity);
        obj.transform.SetParent(gameObject.transform);
    }

    //随机获取一个某范围内的Vector3 坐标
    private Vector3 CreateRandomItem()
    {
        while (true)
        {
            Vector3 v = new Vector3(Random.Range(-9,10), Random.Range(-7,8),0);
            if (!IsAdd(v))
            {
                list.Add(v);
                return v;
            }
        }
    }

    private bool IsAdd(Vector3 v)
    {
        for (int i = 0; i<list.Count;i++)
        {
            if (v == list[i])
            {
                return true;
            }
        }
        return false;
    }

    //随机创建敌人
    private void CreatEnemy()
    {
        int i = Random.Range(0,3);
        if (i == 0)
        {
            CrateItem(3, new Vector3(0, 8, 0));
        }
        else if( i == 1)
        {
            CrateItem(3, new Vector3(-10, 8, 0));
        }
        else
        {
            CrateItem(3, new Vector3(10, 8, 0));
        }
    }
}
