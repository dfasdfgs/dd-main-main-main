using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text uiText;

    void Awake()
    {

    }

    void Update()
    {
        if (PlayerMove.Getitem)
        {
            uiText.text = "������ ȹ�� : �Ϸ�!";
        }
    }
}
