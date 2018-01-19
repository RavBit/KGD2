using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class App_Manager : MonoBehaviour {
    public static App_Manager instance;
    public User User;
    [Header("Username of logged in User")]
    [SerializeField]
    private string username;

    // Makes sure the App_Manager does not get destroyed & Singleton 
    void Awake()
    {
        if (instance != null)
            Debug.LogError("More than one App Manager in the scene");
        else
            instance = this;
        DontDestroyOnLoad(transform.gameObject);
    }
    void Update()
    {
        if (PlayerPrefs.GetInt("Finished") == 1)
        {
            Event_Manager.Game_Lock();
        }
    }
    // Set username from the Authentication Mgr
    public void SetUsername (string _username) {
        username = _username;
	}
    // Get username
    public string GetUsername
    {
        get {
            return username;
        }
    }
    
    public void SetUser(User u)
    {
        User = u;
    }

    // Log out the user
    public void LogOutUser()
    {
       SetUsername("");
       SceneManager.LoadScene("Log_In", LoadSceneMode.Single);
    }
}
