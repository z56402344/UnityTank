using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private int lifeVal = 1;
    public int score = 0;
    public bool isDie;
    public bool isDefeat;

    public GameObject born;
    public GameObject isDefeatUI;

    public Text playerLife;
    public Text playerScore;

    private static PlayerManager instans;
    public static PlayerManager Instans
    {
        get
        {
            return instans;
        }
        set
        {
            instans = value;
        }
    }

    public void Awake()
    {
        instans = this;
    }


    // Update is called once per frame
    void Update()
    {
        if (isDefeat)
        {
            isDefeatUI.SetActive(true);
            return;
        }
        if (isDie)
        {
            Recover();
        }
        playerLife.text = lifeVal.ToString();
        playerScore.text = score.ToString();
    }

    public void Recover()
    {
        --lifeVal;
        if (lifeVal <= 0)
        {
            //生命用完，游戏结束, 回到主界面
            isDefeat = true;
            Invoke("RetrueMain",3);
        }
        else
        {
            //生命没有用完，重新出生

            GameObject tank = Instantiate(born,new Vector3(-2,-8,0),Quaternion.identity);
            tank.GetComponent<Born>().isCreatPlayer = true;
            isDie = false;
        }
    }

    public void RetrueMain()
    {
        SceneManager.LoadScene(0);
    }
}
