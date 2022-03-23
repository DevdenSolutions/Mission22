using TMPro;
using UnityEngine;
using Vuforia;

public class CheckTheThing : MonoBehaviour
{
    [SerializeField] Texture2D ImagesToDetect, ImagesToDetect2;
    [SerializeField] ImageTargetBehaviour imageTargetBehaviour;
    [SerializeField] GameObject prefab, prefab2;
    [SerializeField] GameObject parent;
    [SerializeField] TMP_Text UIText, UIText2;
    [SerializeField] UIDetails _uiDetails;
    [SerializeField] GameObject _uiData, _uiData2;

    private void Start()
    {
      //  VuforiaApplication.Instance.OnVuforiaStarted += CreateImageTargetFromSideLoadedTexture;
    }
    public void ChangeTheImage()
    {
        imageTargetBehaviour.ImageTargetType = ImageTargetType.INSTANT;
        print(imageTargetBehaviour.GetRuntimeTargetTexture());

    }

    void CreateImageTargetFromSideLoadedTexture()
    {
        ObserverBehaviour imageTarget = VuforiaBehaviour.Instance.ObserverFactory.CreateImageTarget(ImagesToDetect, 1f, ImagesToDetect.name);
        imageTarget.gameObject.AddComponent<MeshRenderer>();
        imageTarget.transform.SetParent(parent.transform);
        imageTarget.transform.localPosition = Vector3.zero;
        DefaultObserverEventHandler DOEH = imageTarget.gameObject.AddComponent<DefaultObserverEventHandler>();
        DOEH.StatusFilter = DefaultObserverEventHandler.TrackingStatusFilter.Tracked;

        DOEH.OnTargetFound = new UnityEngine.Events.UnityEvent();
        DOEH.OnTargetFound.AddListener(DoSomethingOnTargetFound);
        DOEH.OnTargetLost = new UnityEngine.Events.UnityEvent();
        DOEH.OnTargetLost.AddListener(DoSomethingOnTargetLost);
        DOEH.OnTargetFound = new UnityEngine.Events.UnityEvent();
        DOEH.OnTargetFound.AddListener(()=>CheckNameWithParameter(imageTarget.TargetName));
        prefab.transform.SetParent(imageTarget.gameObject.transform);
        prefab.transform.localPosition = Vector3.zero;




        ObserverBehaviour imageTarget2 = VuforiaBehaviour.Instance.ObserverFactory.CreateImageTarget(ImagesToDetect2, 1f, ImagesToDetect2.name);
        imageTarget2.gameObject.AddComponent<MeshRenderer>();
        imageTarget2.transform.SetParent(parent.transform);
        imageTarget2.transform.localPosition = Vector3.zero;
        DefaultObserverEventHandler DOEH2 = imageTarget2.gameObject.AddComponent<DefaultObserverEventHandler>();
        DOEH2.StatusFilter = DefaultObserverEventHandler.TrackingStatusFilter.Tracked;

        DOEH2.OnTargetFound = new UnityEngine.Events.UnityEvent();
        DOEH2.OnTargetFound.AddListener(DoSomethingOnTargetFound2);
        DOEH2.OnTargetLost = new UnityEngine.Events.UnityEvent();
        DOEH2.OnTargetLost.AddListener(DoSomethingOnTargetLost2);
        DOEH2.OnTargetFound = new UnityEngine.Events.UnityEvent();
        DOEH2.OnTargetFound.AddListener(() => CheckNameWithParameter(imageTarget2.TargetName));
        //prefab2.transform.SetParent(imageTarget2.gameObject.transform);
        //prefab2.transform.localPosition = Vector3.zero;


    }


    public void SetPrefab()
    {
        prefab.transform.SetParent(null);
    }
    void DoSomethingOnTargetFound()
    {
        //UIText.text = "Found";
        // Invoke("SetPrefab", 2f);
        // _uiData.SetActive(true);
    }

    void DoSomethingOnTargetLost()
    {
        //UIText.text = "Lost";
        //  _uiData.SetActive(false);
    }

    void DoSomethingOnTargetFound2()
    {
        //_uiData2.SetActive(true);
    }

    void DoSomethingOnTargetLost2()
    {
        //_uiData2.SetActive(false);
    }

    void CheckNameWithParameter(string s)
    {
        UIText.text = s;
        UIText2.text = s;
    }
}
