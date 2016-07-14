﻿using UnityEngine;
using System.Collections;
using SocketIO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LNWPOR : MonoBehaviour {

	void Start () {
        SocketOn();
        StartCoroutine(WaitToEmitGetMainEditMoleculeJSON(1f));
    }


    private void SocketOn()
    {
        NetworkManager.Instance.Socket.On("GET_All_mainEditMoleculeJSON", AddMoleculeList);
    }

    private void AddMoleculeList(SocketIOEvent evt)
    {
        Debug.Log("gg");
    }

    private IEnumerator WaitToEmitGetMainEditMoleculeJSON(float time)
    {
        float count = 0;
        while (count < time)
        {
            count += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        NetworkManager.Instance.Socket.Emit("GET_All_mainEditMoleculeJSON");
    }

    public void OnClickRefreshButton()
    {
        SceneManager.LoadScene("LNWPOR");
    }
}
