using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using TMPro;

public class CreateImageTarget : MonoBehaviour
{
    #region singleton

    private static CreateImageTarget _instance;

    public static CreateImageTarget Instance { get { return _instance; } }


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

    [SerializeField] GameObject _imageParent;
    [SerializeField] TMP_Text _testingText;
    public void CreateTheImageTarget(Texture2D ImagesToDetect, string SoldierName)
    {
        ObserverBehaviour imageTarget = VuforiaBehaviour.Instance.ObserverFactory.CreateImageTarget(ImagesToDetect, 1f, SoldierName);
        imageTarget.transform.SetParent(_imageParent.transform);
        imageTarget.transform.localPosition = Vector3.zero;
        DefaultAreaTargetEventHandler DOEH = imageTarget.gameObject.AddComponent<DefaultAreaTargetEventHandler>();
        DOEH.StatusFilter = DefaultAreaTargetEventHandler.TrackingStatusFilter.Tracked;

        DOEH.OnTargetFound = new UnityEngine.Events.UnityEvent();
        DOEH.OnTargetFound.AddListener(OnTargetFound);

        DOEH.OnTargetLost = new UnityEngine.Events.UnityEvent();
        DOEH.OnTargetLost.AddListener(OnTargetLost);

        Debug.LogError("Created Images");
    }

    void OnTargetFound()
    {
        _testingText.text = "Found The Image";
    }

    void OnTargetLost()
    {
        _testingText.text = "Lost the Image";
    }
}


