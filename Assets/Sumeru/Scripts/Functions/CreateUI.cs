using UnityEngine;

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
    public void InstantiateUIFromJson(Transform imageTarget, string Name)
    {
        GameObject go = Instantiate(_uiPrefab, imageTarget);
        AddDataToUI(go.transform.GetChild(0).gameObject.GetComponent<UIDetails>(), Name);
    }

    void AddDataToUI(UIDetails uIDetails, string Name)
    {
        uIDetails.title.text = Name;
        SoldierDataManager.Instance.FindSoldier(Name, (e) =>
         {
             Debug.LogError("Printing from CreateUI: " + e.TrackingImageURL + e.Designation + e.Type);
         });
    }
}
