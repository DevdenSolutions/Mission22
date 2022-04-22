using UnityEngine;
using UnityEngine.UI;

public class CreateUI : MonoBehaviour
{
    #region singleton

    private static CreateUI _instance;

    public static CreateUI Instance { get { return _instance; } }


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


    [SerializeField] GameObject _uiPrefab;
    [SerializeField] UiView _currentUiView;

    public  delegate void ToggleTheView();
    public ToggleTheView _toggleTheView;

    public delegate void ToggleTheSoldier();
    public ToggleTheSoldier _toggleTheSoldier;
    public void InstantiateUIFromJson(Transform imageTarget, string Name)
    {
        GameObject go = Instantiate(_uiPrefab, imageTarget);

        //switch (_currentUiView)
        //{
        //    case UiView.View1:
        //        go.transform.GetChild(0).gameObject.SetActive(true);
        //        AddDataToUI(go.transform.GetChild(0).gameObject.GetComponent<UIDetails>(), Name);
        //        break;

        //    case UiView.View2:
        //        go.transform.GetChild(1).gameObject.SetActive(true);
        //        AddDataToUI(go.transform.GetChild(1).gameObject.GetComponent<UIDetails>(), Name);
        //        break;
        //}

        AddDataToUI(go.transform.GetChild(0).gameObject.GetComponent<UIDetails>(), Name);
        AddDataToUI(go.transform.GetChild(1).gameObject.GetComponent<UIDetails>(), Name);
        go.transform.GetChild(0).gameObject.SetActive(true);
        go.transform.GetChild(1).gameObject.SetActive(false);


    }

    void AddDataToUI(UIDetails uIDetails, string Name)
    {
        SoldierDataManager.Instance.FindSoldier(Name, (e) =>
         {
            
             uIDetails.Hometown.text = "Hometown :" + e.Hometown;
             uIDetails.Name.text = "Name: " + e.Name;
             uIDetails.DOB.text = "DOB: " + e.DOB;
             uIDetails.DOD.text = "DOD: " + e.DOD;
             uIDetails.Designation.text = "Rank: " + e.Designation;
             uIDetails.Branch.text = "Branch: " + e.Branch;
             uIDetails.shortBio.text = e.ShortBio;
         });
    }

    public void ToggleView()
    {
        _toggleTheView?.Invoke();
    }


    public void ToggleSoldier()
    {
        _toggleTheSoldier?.Invoke();
    }
}

public enum UiView
{
    View1,
    View2
}
