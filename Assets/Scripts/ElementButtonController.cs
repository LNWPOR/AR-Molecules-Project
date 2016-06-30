﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ElementButtonController : MonoBehaviour {

    ElementData elementForButton;
    GameObject periodicTableControllerGameObject;
    PeriodicTableController periodicTableControllerScript;
    // Use this for initialization
    void Start()
    {
        periodicTableControllerGameObject = GameObject.Find("PeriodicTableController");
        periodicTableControllerScript = periodicTableControllerGameObject.GetComponent<PeriodicTableController>();
        if (PeriodicTableManager.Instance.periodicTableList.Exists(x => x.name.Equals(gameObject.name)))
        {
            elementForButton = PeriodicTableManager.Instance.periodicTableList.Find(x => x.name.Equals(gameObject.name));
            Array.Find(gameObject.GetComponentsInChildren<Text>(), s => s.name.Equals("NameText")).text = elementForButton.name;
            Array.Find(gameObject.GetComponentsInChildren<Text>(), s => s.name.Equals("NumberText")).text = elementForButton.atomNumber.ToString();
        }
    }

    public void OnClickElementButton()
    {
        //gameObject.GetComponent<Button>().image.color = Color.red;

        GameObject elementPrefabInitiated = periodicTableControllerScript.getElementPrefabInitiated();
        GameObject elementPrefabSelected = Array.Find(periodicTableControllerScript.getElementPrefabs(), s => s.name.Equals(elementForButton.name));
        if (elementPrefabSelected != null)
        {
            if (elementPrefabInitiated == null)
            {
                InstantiatePrefabSelected(elementPrefabSelected, elementForButton);
                elementPrefabInitiated = periodicTableControllerScript.getElementPrefabInitiated();
            }
            if (!elementPrefabInitiated.name.Equals(elementForButton.name))
            {
                Destroy(elementPrefabInitiated);
                InstantiatePrefabSelected(elementPrefabSelected, elementForButton);
            }
        }
        else
        {
            //Debug.Log("cant find prefab");
        }
    }

    private void InstantiatePrefabSelected(GameObject elementPrefabSelected, ElementData elementForButton)
    {
        GameObject newElementPrefabInitiated = Instantiate(elementPrefabSelected,
                                                new Vector3(-2f, 3.5f, 0f),
                                                Quaternion.identity) as GameObject;
        newElementPrefabInitiated.name = elementForButton.name;
        periodicTableControllerScript.setElementPrefabInitiated(newElementPrefabInitiated);
    }
}