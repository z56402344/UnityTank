using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private SpriteRenderer sr;
    public GameObject explodePerfab;
    public AudioClip dieAudio;

    public Sprite breakSprite;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
     }

    public void die()
    {
        sr.sprite = breakSprite;
        Instantiate(explodePerfab,transform.position,transform.rotation);
        AudioSource.PlayClipAtPoint(dieAudio, transform.position);
    }


}
