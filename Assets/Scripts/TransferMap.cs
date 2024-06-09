using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public Transform ta;
    public Transform target; // 이동할 타겟 설정

    public GameObject thePlayer;
    public GameObject theCamera;


    // 박스 콜라이더에 닿는 순간 이벤트 발생
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "Player")
        {
            theCamera.transform.position = ta.transform.position;
            thePlayer.transform.position = target.transform.position;

        }
    }


}
