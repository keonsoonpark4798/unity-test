using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExit : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnClickExit()
    {
        Application.Quit();
        Debug.Log("������ �����մϴ�");
    }
}