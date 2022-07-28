using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Logininfo   // 로그인 정보 class
{
    public string ID;    // 아이디
    public string PW;    // 비밀번호
    public string Ty = "Login";  // 타입
}

[System.Serializable]
public class Signupinfo  // 회원가입 정보 class
{
    public string ID;    // 아이디
    public string PW;    // 비밀번호
    public string NA;    // 이름
    public int AGE;      // 나이
    public string PHONE; // 핸드폰
    public string Ty = "Signup";  // 타입
}
[System.Serializable]
public class Updateinfo  // 수정할 정보 class
{
    public string ID;    // 아이디
    public string PW;    // 비밀번호
    public string NA;    // 이름
    public int AGE;      // 나이
    public string PHONE; // 핸드폰
    public string Ty = "Update";  // 타입
}
public class Textmess   // 송신받은 메세지
{
    public string message;    // 메세지
    public string ID;         // 아이디
    public string PW;         // 비밀번호
    public string NA;         // 이름
    public int AGE;           // 나이
    public string PHONE;      // 핸드폰
}

public class LoginManager : MonoBehaviour
{

    public GameObject LoginView;          // 로그인 패널
    public GameObject SignupView;         // 회원가입 패널
    public GameObject UpdateView;         // 회원정보 수정 패널

    public InputField inputField_ID;      // 로그인 정보
    public InputField inputField_PW;     

    public InputField inputField_SignID;  // 회원가입 정보
    public InputField inputField_SignPW;
    public InputField inputField_SignNA;
    public InputField inputField_SignAG;
    public InputField inputField_SignPH;

    public InputField inputField_UpID;    // 회원수정 정보
    public InputField inputField_UpPW;
    public InputField inputField_UpNA;
    public InputField inputField_UpAG;
    public InputField inputField_UpPH;

    string LoginURL = "http://192.168.2.115/AuthDB.php";  //웹서버 주소

    IEnumerator SendToDB(string json)  // 서버 전송 코루틴
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
            Debug.Log(Text.ID + "님 안녕하세요.");
            UpdateView.SetActive(true);
            inputField_UpID.text = Text.ID;
            inputField_UpPW.text = Text.PW;
            inputField_UpNA.text = Text.NA;
            inputField_UpAG.text = (Text.AGE).ToString();
            inputField_UpPH.text = Text.PHONE;
        }
    }

    public void LoginButtonClick()   // 로그인 버튼 클릭
    {
        Logininfo logininfo = new Logininfo();
        logininfo.ID = inputField_ID.text;
        logininfo.PW = inputField_PW.text;
        string json = JsonUtility.ToJson(logininfo);
        StartCoroutine(SendToDB(json));
    }

    public void SignUpButtonClick()   // 회원가입 버튼 클릭 
    {
        inputField_SignID.text = "";
        inputField_SignPW.text = "";
        inputField_SignNA.text = "";
        inputField_SignAG.text = "";
        inputField_SignPH.text = "";
        SignupView.SetActive(true);
    }
    public void CancelButtonClick()    // 취소 버튼 클릭
    {
        SignupView.SetActive(false);
        UpdateView.SetActive(false);
        LoginView.SetActive(true);
    }
    public void ConSignUpButtonClick()  // 회원가입 확인 버튼 클릭
    {
        SignupView.SetActive(false);
        if(string.IsNullOrWhiteSpace(inputField_SignID.text) || string.IsNullOrWhiteSpace(inputField_SignPW.text))
        {
            Debug.Log("ID나 PW가 입력되지 않았습니다.");
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
    public void EditButtonClick()  // 회원정보 수정 버튼 클릭
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
