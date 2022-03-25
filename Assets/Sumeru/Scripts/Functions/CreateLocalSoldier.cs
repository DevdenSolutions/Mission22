using UnityEngine;

public class CreateLocalSoldier : MonoBehaviour
{
    [SerializeField] Texture2D _imagesToAdd;

    private void Start()
    {
        Invoke("CSL", 1f);
    }
    public void CSL()
    {
        SoldierDataManager.Instance.CreateSoldier("asdasda", "adasdasd", "asdasdad", "adadasd", _imagesToAdd);
    }
}
