using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;

public class SoldierDataManager : MonoBehaviour
{
    #region singleton

    private static SoldierDataManager _instance;

    public static SoldierDataManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion

    public List<ISoldier> _soldierList = new List<ISoldier>();


    public void CreateSoldier(string Name, string url, string Hometown, string DOB, string DOD, string designation, string branch,string shortBio, Texture2D imageToDetect=null)
    {
        ISoldier LocalSoldier = SoldierFactory.GetSoldierInstance(Name,Hometown,DOB,DOD, url, designation, branch,shortBio, imageToDetect);
        _soldierList.Add(LocalSoldier);
        CreateImageTarget.Instance.CreateTheImageTarget(imageToDetect,Name, (go)=>
        {
            LocalSoldier.ImageTargetGameObject = go;
        });
    }

    public void PrintList()
    {
        foreach(var x in _soldierList)
        {
            print("Name: " + x.Name);
            print("URL: " + x.TrackingImageURL);
            print("Designation: " + x.Designation);
            print("Type: " + x.Branch);
            print("GameObject: " + x.ImageTargetGameObject);
        }
    }

    public void FindSoldier(string soldierName, Action<ISoldier> action = null)
    {
        var URL = _soldierList.Where(e => e.Name == soldierName).Select(e => e).FirstOrDefault();

        if (URL != null)
        {
            action?.Invoke(URL);
        }
        else
        {
            print("Soldier Not Found");
        }
    }

}


public class SoldierFactory
{
    public static ISoldier GetSoldierInstance(string name, string Hometown, string DOB, string DOD, string URL, string designation, string branch, string shortBio, Texture2D imageToDetect=null)
    {
        ISoldier remoteSoldier;

        remoteSoldier = new Soldier();
        remoteSoldier.Name = name;
        remoteSoldier.Hometown = Hometown;
        remoteSoldier.DOB = DOB;
        remoteSoldier.DOD = DOD;
        remoteSoldier.TrackingImageURL = URL;
        remoteSoldier.Designation = designation;
        remoteSoldier.Branch = branch;
        remoteSoldier.ShortBio = shortBio;
        if (imageToDetect != null)
        {
            remoteSoldier.TargetImage = imageToDetect;
        }

        return remoteSoldier;
    }
}
