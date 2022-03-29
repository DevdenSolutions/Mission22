using Proyecto26;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Vuforia;

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

    //private readonly string baseURL = "https://raex1311.github.io/VMA/VMA.json";
    private readonly string baseURL = "https://mission22-99754-default-rtdb.firebaseio.com/Soldiers.json";
    public MyRoot[] JsonData;


    private void Start()
    {
        VuforiaApplication.Instance.OnVuforiaStarted += CreateSoldierFromServer;
    }
    public void CreateSoldierFromServer()
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
                StartCoroutine(GetImages(x.TrackingImageURL,(tex)=> {

                    SoldierDataManager.Instance.CreateSoldier(x.Name, x.TrackingImageURL,x.Hometown,x.DOB,x.DOD, x.Designation, x.Branch,x.ShortBio, tex);  //Creating the soldier image targets here from the server data
                }));
               
            }
        }
        ).Catch(exp =>
          {
              Debug.Log(exp.Message + "" + exp.StackTrace);
          }
        );
    }

    IEnumerator GetImages(string URL, Action<Texture2D> callback = null)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(URL);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            callback?.Invoke(((DownloadHandlerTexture)www.downloadHandler).texture);
        }

    }

}

[System.Serializable]
public class MyRoot
{
    public string Name;
    public string TrackingImageURL;
    public string Hometown;
    public string DOB;
    public string DOD;
    public string Designation;
    public string Branch;
    public string ShortBio;
}
