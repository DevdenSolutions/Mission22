using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AddImageToList : MonoBehaviour
{
    #region singleton

    private static AddImageToList _instance;

    public static AddImageToList Instance { get { return _instance; } }


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


    public void GetImagesFromURL()
    {
        int i = 0;
        foreach (var x in SoldierDataManager.Instance._soldierList)
        {
            StartCoroutine(GetImages(x.TrackingImageURL, i, (tex) =>
            {
                x.TargetImage = tex;
                CreateImageTarget.Instance.CreateTheImageTarget(x.TargetImage, x.Name); //Creating Image Targets from the texture we got from URL
            }));
            i++;
        }
    }

    IEnumerator GetImages(string URL, int index, Action<Texture2D> callback = null)
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
