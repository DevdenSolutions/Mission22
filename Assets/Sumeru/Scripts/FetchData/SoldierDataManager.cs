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

    public List<iSoldier> _soldierList = new List<iSoldier>();


    public void CreateSoldier(string Name, string url, string designation, string type,Texture2D imageToDetect=null)
    {
        iSoldier LocalSoldier = SoldierFactory.GetSoldierInstance(Name, url, designation, type,imageToDetect);
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
            print("Type: " + x.Type);
            print("GameObject: " + x.ImageTargetGameObject);
        }
    }

    public void FindSoldier(string soldierName, Action<iSoldier> action = null)
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
    public static iSoldier GetSoldierInstance(string name, string URL, string designation, string type, Texture2D imageToDetect=null)
    {
        iSoldier remoteSoldier;

        remoteSoldier = new Soldier();
        remoteSoldier.Name = name;
        remoteSoldier.TrackingImageURL = URL;
        remoteSoldier.Designation = designation;
        remoteSoldier.Type = type;
        if (imageToDetect != null)
        {
            remoteSoldier.TargetImage = imageToDetect;
        }

        return remoteSoldier;
    }
}
