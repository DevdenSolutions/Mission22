using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    void Start()
    {
      //  CreateSoldier("Soldier1", "This is the URL man", "DGP", "US Army");
    }

    public void CreateSoldier(string Name, string url, string designation, string type)
    {
        iSoldier LocalSoldier = SoldierFactory.GetSoldierInstance(Name, url, designation, type);
        _soldierList.Add(LocalSoldier);
    }

    public void PrintList()
    {
        foreach(var x in _soldierList)
        {
            print("Name: " + x.Name);
            print("URL: " + x.TrackingImageURL);
            print("Designation: " + x.Designation);
            print("Type: " + x.Type);
        }
    }

    public void FindSoldier(string soldierName)
    {
        var URL = _soldierList.Where(e => e.Name == soldierName).Select(e => e.TrackingImageURL);

        if (URL.GetEnumerator().MoveNext())
        {
            foreach (var x in URL)
            {
                print("The URL is: " + x);
            }
        }
        else
        {
            print("Soldier Not Found");
        }

    }

}


public class SoldierFactory
{
    public static iSoldier GetSoldierInstance(string name, string URL, string designation, string type)
    {
        iSoldier remoteSoldier;

        remoteSoldier = new Soldier();
        remoteSoldier.Name = name;
        remoteSoldier.TrackingImageURL = URL;
        remoteSoldier.Designation = designation;
        remoteSoldier.Type = type;

        return remoteSoldier;
    }
}