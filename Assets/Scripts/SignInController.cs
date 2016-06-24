﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SocketIO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class SignInController : MonoBehaviour
{

    
    public InputField usernameInputField;
    public InputField passwordInputField;
    public Button signIpBtn;
    public Button signUpBtn;

    void Awake()
    {
        signIpBtn.onClick.AddListener(() => OnClickSignIn());
        signUpBtn.onClick.AddListener(() => OnClickSingUp());
    }

    void Start()
    {
        SocketOn();
    }

    private void SocketOn()
    {
        NetworkManager.Instance.Socket.On("NET_AVARIABLE", (SocketIOEvent evt) => {
            Debug.Log("Net Avariable");
        });
        NetworkManager.Instance.Socket.On("CONNECTED", OnUserSignIn);
    }

    private void OnUserSignIn(SocketIOEvent evt)
    {
        //Debug.Log("ID = "+evt.data.GetField("id").ToString());
        //Debug.Log("USERNAME = "+evt.data.GetField("username").ToString());
        UserData usrData = new UserData();
        usrData.id = Converter.JsonToString(evt.data.GetField("id").ToString());
        usrData.username = Converter.JsonToString(evt.data.GetField("username").ToString());
        UserManager.Instance.userData = usrData;
        SceneManager.LoadScene("menu");
    }

    private void OnClickSignIn()
    {
        // Debug.Log(usernameInputField.text + " Login");
        // string username = usernameInputField.text;
        // Dictionary<string, string> data = new Dictionary<string, string>();
        // data["username"] = username;
        // data["name"] = username;
        // NetworkManager.Instance.Socket.Emit("LOGIN",new JSONObject(data));

        JSONObject data = new JSONObject();
        data.AddField("username", usernameInputField.text);
        data.AddField("password", passwordInputField.text);
        NetworkManager.Instance.Socket.Emit("SIGNIN", data);
    }

    private void OnClickSingUp()
    {
        SceneManager.LoadScene("signup");
    }
}
