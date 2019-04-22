using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{
    public GameObject playerPerfab;
    public AudioClip audio;
    public GameObject[] enemyPrefabs;
    public bool isCreatPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("bornTank", 1f);
        Destroy(gameObject,1f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void bornTank()
    {
        if (isCreatPlayer)
        {
            AudioSource.PlayClipAtPoint(audio, transform.position);
            Instantiate(playerPerfab, transform.position, Quaternion.identity);
        }
        else
        {
            int num = Random.Range(0,2);

            Instantiate(enemyPrefabs[num], transform.position, Quaternion.identity);
        }

    }
}
