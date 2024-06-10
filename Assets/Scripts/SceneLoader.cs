using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string stage;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "pickupitem")
        {
            SceneManager.LoadScene("stage");
        }
    }
}
