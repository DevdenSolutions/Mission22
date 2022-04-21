using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
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
    private readonly string baseURL_R = "https://stackby.com/api/betav1/rowlist/stAyOUTV5MUFGmUhJo/Table%201";
    private readonly string ApiKey_R = "etqPMHwxBFem2wcD";

    private readonly string baseURL_Mission22 = "https://stackby.com/api/betav1/rowlist/stfVS31L3x0jC5YLc5/Table%201";
    private readonly string ApiKey_Mission22 = "f9XnglmZKe4cF2Oq";

    public MyRoot[] JsonData;
    public Root[] JsonData2;

    [SerializeField] String Etag;

    private void Start()
    {
        //Using Strategy 1
       // VuforiaApplication.Instance.OnVuforiaStarted += CreateSoldierFromServer;
        VuforiaApplication.Instance.OnVuforiaStarted += CheckForCache;

        //Using Strategy 2
        //VuforiaApplication.Instance.OnVuforiaStarted += CreateSoldierFromServer2;
    }

    void CheckForCache()
    {
        RestClient.Request(new RequestHelper
        {
            Uri = baseURL,
            Method = "GET",
            Headers = new Dictionary<string, string>
            {
                { "X-Firebase-ETag","true" }
            }
        }).Then(response =>
        {
            Etag = response.GetHeader("ETag");
            Debug.LogError("The ETag is:---> " + Etag);

        });
    }


    #region Strategy3
    void PutDataIntoCache()
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
                StartCoroutine(GetImages(x.TrackingImageURL, (tex) => {

                    SoldierDataManager.Instance.CreateSoldier(x.Name, x.TrackingImageURL, x.Hometown, x.DOB, x.DOD, x.Designation, x.Branch, x.ShortBio, tex);  //Creating the soldier image targets here from the server data
                }));

            }
        }
        ).Catch(exp =>
        {
            Debug.Log(exp.Message + "" + exp.StackTrace);
        }
        );
    }

    void CreateSoldier()
    {

    }
    #endregion

    #region Strategy1
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

    #endregion

    #region Strategy2

    public void CreateSoldierFromServer2()
    {
        RestClient.DefaultRequestHeaders["Authorization"] = "Bearer";
        RequestHelper requestOptions = null;

        requestOptions = new RequestHelper
        {
            Uri = baseURL_Mission22,
            EnableDebug = true,
            Headers = new Dictionary<string, string>
            {
                {"api-key", ApiKey_Mission22 }
            }
        };

        RestClient.GetArray<Root>(requestOptions).Then(res =>
        {
            JsonData2 = res;
            print(res);

            foreach (var x in JsonData2)
            {
                AddNotAvailableText(x);
                print(x);
                
                StartCoroutine(GetImages(x.field.TrackingURL, (tex) =>
                {
                    
                    SoldierDataManager.Instance.CreateSoldier(x.field.Name, x.field.TrackingURL, x.field.Hometown, x.field.DOB, x.field.DOD, x.field.Designation, x.field.Branch, x.field.ShortBio, tex);  //Creating the soldier image targets here from the server data
                }));

            }
        }
        ).Catch(exp =>
        {
            Debug.Log(exp.Message + "" + exp.StackTrace);
        }
        );
    }


    void AddNotAvailableText(Root myRoot)
    {
        if (string.IsNullOrEmpty(myRoot.field.Name))
        {
            myRoot.field.Name = "NA";
        }

        if (string.IsNullOrEmpty(myRoot.field.Hometown))
        {
            myRoot.field.Hometown = "NA";
        }
        if (string.IsNullOrEmpty(myRoot.field.DOB))
        {
            myRoot.field.DOB = "NA";
        }

        if (string.IsNullOrEmpty(myRoot.field.DOD))
        {
            myRoot.field.DOD = "NA";
        }

        if (string.IsNullOrEmpty(myRoot.field.Designation))
        {
            myRoot.field.Designation = "NA";
        }

        if (string.IsNullOrEmpty(myRoot.field.Branch))
        {
            myRoot.field.Branch = "NA";
        }

        if (string.IsNullOrEmpty(myRoot.field.ShortBio))
        {
            myRoot.field.ShortBio = "NA";
        }
    }
    #endregion

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
