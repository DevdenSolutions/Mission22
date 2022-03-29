using System;
using TMPro;
using UnityEngine;
using Vuforia;

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
    [SerializeField] GameObject _soldierStatuePrefab;
    public void CreateTheImageTarget(Texture2D ImagesToDetect, string SoldierName, Action<GameObject> action = null)
    {
        ObserverBehaviour imageTarget = VuforiaBehaviour.Instance.ObserverFactory.CreateImageTarget(ImagesToDetect, 1f, SoldierName);
        imageTarget.transform.SetParent(_imageParent.transform);
        imageTarget.transform.localPosition = Vector3.zero;
        Instantiate(_soldierStatuePrefab, imageTarget.transform);

        DefaultAreaTargetEventHandler DOEH = imageTarget.gameObject.AddComponent<DefaultAreaTargetEventHandler>();
        DOEH.StatusFilter = DefaultAreaTargetEventHandler.TrackingStatusFilter.Tracked_ExtendedTracked;

        DOEH.OnTargetFound = new UnityEngine.Events.UnityEvent();
        DOEH.OnTargetFound.AddListener(() => OnTargetFound(SoldierName));

        DOEH.OnTargetLost = new UnityEngine.Events.UnityEvent();
        DOEH.OnTargetLost.AddListener(OnTargetLost);

        CreateUI.Instance.InstantiateUIFromJson(imageTarget.gameObject.transform, SoldierName);

        action?.Invoke(imageTarget.gameObject);
        Debug.LogError("Created Images");
    }

    void OnTargetFound(string name)
    {
        _testingText.text = name;
        ManageTargetBehaviour.Instance.OnTargetFound();
    }

    void OnTargetLost()
    {
        _testingText.text = "New Text";
        ManageTargetBehaviour.Instance.OnTargetLost();
    }
}



