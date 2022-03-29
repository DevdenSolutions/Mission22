using UnityEngine;
using Vuforia;

public class CreateLocalSoldier : MonoBehaviour
{
    [SerializeField] Texture2D _imagesToAdd;

    private void Start()
    {
        VuforiaApplication.Instance.OnVuforiaStarted += CreateSoldierLocally;
    }
    public void CreateSoldierLocally() //Creating soldier image targets from locally stored images
    {
       // SoldierDataManager.Instance.CreateSoldier("asdasda", "adasdasd", "asdasdad", "adadasd","adsadad","asdsadad","adasdada","asdsadsad", _imagesToAdd);
    }
}
