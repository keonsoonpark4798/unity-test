using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Logininfo   // �α��� ���� class
{
    public string ID;    // ���̵�
    public string PW;    // ��й�ȣ
    public string Ty = "Login";  // Ÿ��
}

[System.Serializable]
public class Signupinfo  // ȸ������ ���� class
{
    public string ID;    // ���̵�
    public string PW;    // ��й�ȣ
    public string NA;    // �̸�
    public int AGE;      // ����
    public string PHONE; // �ڵ���
    public string Ty = "Signup";  // Ÿ��
}
[System.Serializable]
public class Updateinfo  // ������ ���� class
{
    public string ID;    // ���̵�
    public string PW;    // ��й�ȣ
    public string NA;    // �̸�
    public int AGE;      // ����
    public string PHONE; // �ڵ���
    public string Ty = "Update";  // Ÿ��
}
public class Textmess   // �۽Ź��� �޼���
{
    public string message;    // �޼���
    public string ID;         // ���̵�
    public string PW;         // ��й�ȣ
    public string NA;         // �̸�
    public int AGE;           // ����
    public string PHONE;      // �ڵ���
}

public class LoginManager : MonoBehaviour
{

    public GameObject LoginView;          // �α��� �г�
    public GameObject SignupView;         // ȸ������ �г�
    public GameObject UpdateView;         // ȸ������ ���� �г�

    public InputField inputField_ID;      // �α��� ����
    public InputField inputField_PW;     

    public InputField inputField_SignID;  // ȸ������ ����
    public InputField inputField_SignPW;
    public InputField inputField_SignNA;
    public InputField inputField_SignAG;
    public InputField inputField_SignPH;

    public InputField inputField_UpID;    // ȸ������ ����
    public InputField inputField_UpPW;
    public InputField inputField_UpNA;
    public InputField inputField_UpAG;
    public InputField inputField_UpPH;

    string LoginURL = "http://192.168.2.115/AuthDB.php";  //������ �ּ�

    IEnumerator SendToDB(string json)  // ���� ���� �ڷ�ƾ
    {
        Debug.Log(json);
        WWWForm form = new WWWForm();
        form.AddField("jsonPost", json);
        WWW www = new WWW(LoginURL, form);

        yield return www;

       Textmess Text = JsonUtility.FromJson<Textmess>(www.text);
        Debug.Log(Text.message);
        if(Text.message == "login success")
        {
            LoginView.SetActive(false);
            Debug.Log(Text.ID + "�� �ȳ��ϼ���.");
            UpdateView.SetActive(true);
            inputField_UpID.text = Text.ID;
            inputField_UpPW.text = Text.PW;
            inputField_UpNA.text = Text.NA;
            inputField_UpAG.text = (Text.AGE).ToString();
            inputField_UpPH.text = Text.PHONE;
        }
    }

    public void LoginButtonClick()   // �α��� ��ư Ŭ��
    {
        Logininfo logininfo = new Logininfo();
        logininfo.ID = inputField_ID.text;
        logininfo.PW = inputField_PW.text;
        string json = JsonUtility.ToJson(logininfo);
        StartCoroutine(SendToDB(json));
    }

    public void SignUpButtonClick()   // ȸ������ ��ư Ŭ�� 
    {
        inputField_SignID.text = "";
        inputField_SignPW.text = "";
        inputField_SignNA.text = "";
        inputField_SignAG.text = "";
        inputField_SignPH.text = "";
        SignupView.SetActive(true);
    }
    public void CancelButtonClick()    // ��� ��ư Ŭ��
    {
        SignupView.SetActive(false);
        UpdateView.SetActive(false);
        LoginView.SetActive(true);
    }
    public void ConSignUpButtonClick()  // ȸ������ Ȯ�� ��ư Ŭ��
    {
        SignupView.SetActive(false);
        if(string.IsNullOrWhiteSpace(inputField_SignID.text) || string.IsNullOrWhiteSpace(inputField_SignPW.text))
        {
            Debug.Log("ID�� PW�� �Էµ��� �ʾҽ��ϴ�.");
        }else{
            Signupinfo signupinfo = new Signupinfo();
            signupinfo.ID = inputField_SignID.text;
            signupinfo.PW = inputField_SignPW.text;
            signupinfo.NA = inputField_SignNA.text;
            signupinfo.AGE = int.Parse(inputField_SignAG.text);
            signupinfo.PHONE = inputField_SignPH.text;
            string json = JsonUtility.ToJson(signupinfo);
            StartCoroutine(SendToDB(json));
        }
    }
    public void EditButtonClick()  // ȸ������ ���� ��ư Ŭ��
    {
        UpdateView.SetActive(false);

        Updateinfo updateinfo = new Updateinfo();
        updateinfo.ID = inputField_UpID.text;
        updateinfo.PW = inputField_UpPW.text;
        updateinfo.NA = inputField_UpNA.text;
        updateinfo.AGE = int.Parse(inputField_UpAG.text);
        updateinfo.PHONE = inputField_UpPH.text;
        string json = JsonUtility.ToJson(updateinfo);
        StartCoroutine(SendToDB(json));
    }
}
