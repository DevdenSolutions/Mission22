using UnityEngine;

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


    [SerializeField] Texture2D[] _imagesToDetect;

    private void Start()
    {
    }


    public void AddImage()
    {
        int i = 0;
        foreach (var x in SoldierDataManager.Instance._soldierList)
        {
            x.TargetImage = _imagesToDetect[i];
            i++;
        }
    }

    public void MakeImageTargets()
    {
        Debug.LogError("Called MakeImageTargets");

        foreach (var x in SoldierDataManager.Instance._soldierList)
        {
            print("Image: " + x.TargetImage.name);
            print("Name: " + x.Name);
        }

        foreach (var x in SoldierDataManager.Instance._soldierList)
        {
            CreateImageTarget.Instance.CreateTheImageTarget(x.TargetImage, x.Name);
            print("Creating Image");
        }


    }
}
