﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MainManager : MonoBehaviour
{
    public MoleculeData moleculeSelected;
    public List<MoleculeData> moleculeList;
    public JSONObject[] AllMoleculesJSON;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);

        moleculeList = new List<MoleculeData>();
        AddMoleculeToList("BeCl2");
        AddMoleculeToList("SO3");
        AddMoleculeToList("CH4");
        AddMoleculeToList("PCl5");
        AddMoleculeToList("SF6");
        AddMoleculeToList("Cl2");
        AddMoleculeToList("SF4");
        AddMoleculeToList("XeF4");
        AddMoleculeToList("ClF5");
        AddMoleculeToList("IF7");
    }

    private void AddMoleculeToList(string newName)
    {
        MoleculeData molecule = new MoleculeData(newName);
        moleculeList.Add(molecule);
    }
}
