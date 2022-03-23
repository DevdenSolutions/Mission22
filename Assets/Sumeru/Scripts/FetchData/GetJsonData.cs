using Proyecto26;
using UnityEngine;

public class GetJsonData : MonoBehaviour
{
    #region singleton

    private static GetJsonData _instance;

    public static GetJsonData Instance { get { return _instance; } }


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

    private readonly string baseURL = "https://raex1311.github.io/VMA/VMA.json";
    public MyRoot[] JsonData;


    private void Start()
    {
        Get();
    }
    public void Get()
    {
        RestClient.DefaultRequestHeaders["Authorization"] = "Bearer";
        RequestHelper requestOptions = null;

        requestOptions = new RequestHelper
        {
            Uri = baseURL,
            EnableDebug = true,
        };

        RestClient.GetArray<MyRoot>(requestOptions).Then(res =>
        {
            JsonData = res;
            print(res);

            foreach (var x in JsonData)
            {
                print(x.Name);
                SoldierDataManager.Instance.CreateSoldier(x.Name, x.TrackingImageURL, x.Designation, x.Type);
            }
        }
        ).Catch(exp =>
          {
              Debug.Log(exp.Message + "" + exp.StackTrace);
          }
        ).Finally(() => { AddImageToList.Instance.AddImage(); AddImageToList.Instance.MakeImageTargets();  });
    }

}

[System.Serializable]
public class MyRoot
{
    public string Name;
    public string TrackingImageURL;
    public string Designation;
    public string Type;
}
