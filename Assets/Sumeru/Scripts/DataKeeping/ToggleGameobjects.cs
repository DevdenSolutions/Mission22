using UnityEngine;

public class ToggleGameobjects : MonoBehaviour
{
    [SerializeField] GameObject _View1, _View2, _soldier;
    void Start()
    {
        CreateUI.Instance._toggleTheSoldier += ToggleMesh;
        CreateUI.Instance._toggleTheView += ToggleView;
    }

    void ToggleView()
    {
        if (_View1.activeSelf)
        {
            _View1.SetActive(false);
            _View2.SetActive(true);
        }
        else
        {
            _View1.SetActive(true);
            _View2.SetActive(false);
        }
    }

    void ToggleMesh()
    {
        if (_soldier.activeSelf)
        {
            _soldier.SetActive(false);
        }
        else
        {
            _soldier.SetActive(true);
        }
    }



}
