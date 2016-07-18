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

    public GameObject socketIOPrefab;

    void Awake()
    {
        GetSocketIO();
        signIpBtn.onClick.AddListener(() => OnClickSignIn());
        signUpBtn.onClick.AddListener(() => OnClickSingUp());
    }

    private void GetSocketIO()
    {
        if (GameObject.Find("SocketIO") == null)
        {
            GameObject socketIOGenerated = Instantiate(socketIOPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
            socketIOGenerated.name = "SocketIO";
        }
    }

    void Start()
    {
        SocketOn();
    }

    private void SocketOn()
    {
        if (!NetworkManager.Instance.signInSceneSocketIsOn)
        {
            NetworkManager.Instance.Socket.On("NET_AVARIABLE", (SocketIOEvent evt) => {
                Debug.Log("Net Avariable");
            });
            NetworkManager.Instance.Socket.On("CONNECTED", OnUserSignIn);
            NetworkManager.Instance.signInSceneSocketIsOn = true;
        }
    }

    private void OnUserSignIn(SocketIOEvent evt)
    {
        //Debug.Log("ID = " + evt.data.GetField("id").ToString());
        //Debug.Log("USERNAME = " + evt.data.GetField("username").ToString());
        if (Convert.ToInt32(evt.data.GetField("status").ToString()).Equals(0))
        {
            Debug.Log(Converter.JsonToString(evt.data.GetField("log").ToString()));
        }else if(Convert.ToInt32(evt.data.GetField("status").ToString()).Equals(1))
        {
            UserData usrData = new UserData();
            usrData.id = Converter.JsonToString(evt.data.GetField("id").ToString());
            usrData.username = Converter.JsonToString(evt.data.GetField("username").ToString());
            UserManager.Instance.userData = usrData;
            SceneManager.LoadScene("menu");
        }  
    }

    private void OnClickSignIn()
    {
        if (usernameInputField.text.Equals("") || passwordInputField.text.Equals(""))
        {
            Debug.Log("Please fill username & password");
        }else
        {
            JSONObject data = new JSONObject();
            data.AddField("username", usernameInputField.text);
            data.AddField("password", passwordInputField.text);
            NetworkManager.Instance.Socket.Emit("SIGNIN", data);
        }
    }

    private void OnClickSingUp()
    {
        SceneManager.LoadScene("signup");
    }
}
