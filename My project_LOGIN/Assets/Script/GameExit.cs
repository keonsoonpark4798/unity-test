using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExit : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnClickExit()
    {
        Application.Quit();
        Debug.Log("게임을 종료합니다");
    }
}
