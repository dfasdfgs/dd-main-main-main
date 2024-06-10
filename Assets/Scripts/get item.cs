using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class getitem: MonoBehaviour
{
    public SpriteRenderer Img_Renderer;
    public Sprite Sprite01;


    void Start()
    {
        Img_Renderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "item")
        {
            Img_Renderer.sprite = Sprite01;
            Destroy(collision.gameObject);
            gameObject.tag = "pickupitem";
        }
    }

    void Update()
    {

    }
}
