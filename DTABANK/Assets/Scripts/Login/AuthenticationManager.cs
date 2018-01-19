using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

[System.Serializable]
public class User
{
    public bool success;
    public string error;
    public string email;
    public int id;
    // feel free to add userName....
}
public class AuthenticationManager : MonoBehaviour {
    public GameObject mainMenu;

    public GameObject fieldEmailAddress;
    public GameObject fieldPassword;
    public GameObject fieldReenterPassword;
    public GameObject LockedScreen;
    public Text textEmail;
    public Text textPassword;
    public Text ReenterPassword;
    public Text RegisterREG;
    public Text PasswordREG;
    public bool ShowRegister = false;
    public Text RegisterText;

    public Text Login_Feedback;

    WWWForm form;
    // Use this for initialization
    void Start () {
        Login_Feedback.text = "";
        Event_Manager.GameLock += Locked;
    }
    public void Locked()
    {
        LockedScreen.SetActive(true);
    }
    public void LoginButtonTapped() {
        Login_Feedback.text = "Logging in...";
        StartCoroutine("RequestLogin");
    }

    public void RegisterButtonTapped() {
        Debug.Log("Registering...");
        StartCoroutine("RequestRegister");
    }
    public IEnumerator RequestLogin()
    {
        string email = textEmail.text;
        string password = textPassword.GetComponentInParent<InputField>().text;
        form = new WWWForm();
        form.AddField("usernamePost", email);
        form.AddField("passwordPost", password);
        form.AddField("emailPost", email);

        WWW w = new WWW("http://81.169.177.181/UIB/action_login.php", form);
        yield return w;
        Login_Feedback.color = Color.black;
        Debug.Log(w.text);
        if (string.IsNullOrEmpty(w.error)) {
            User user = JsonUtility.FromJson<User>(w.text);
            Debug.Log("username" + user.id);
            if (user.success == true) {
                if (user.error != "") {
                    Login_Feedback.text = user.error;
                } else {
                    Login_Feedback.text = "login successful.";
                    App_Manager.instance.SetUser(user);
                    App_Manager.instance.SetUsername(user.email);
                    SceneManager.LoadScene("Home", LoadSceneMode.Single);
                }
            } else {
                Login_Feedback.text = "An error occured";
            }

            // todo: launch the game (player)
        } else {
            // error
            Login_Feedback.text = "An error occured.";
        }


    }

    public IEnumerator RequestRegister()
    {
        string email = RegisterREG.text;
        string password = PasswordREG.GetComponentInParent<InputField>().text;
        form = new WWWForm();
        form.AddField("usernamePost", email);
        form.AddField("passwordPost", password);
        form.AddField("emailPost", email);

        WWW w = new WWW("http://81.169.177.181/UIB/action_register.php", form);
        yield return w;
        Login_Feedback.color = Color.black;
        Debug.Log(w.text);
        if (string.IsNullOrEmpty(w.error)) {
            User user = JsonUtility.FromJson<User>(w.text);
            Debug.Log("username" + user.id);
            if (user.success == true) {
                if (user.error != "") {
                    Login_Feedback.text = user.error;
                } else {
                    Login_Feedback.text = "You can log in now";
                }
            } else {
                Login_Feedback.text = "An error occured";
            }

            // todo: launch the game (player)
        } else {
            // error
            Login_Feedback.text = "An error occured.";
        }


    }
}
