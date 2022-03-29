using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class References : MonoBehaviour
{
    #region singleton

    private static References _instance;

    public static References Instance { get { return _instance; } }


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


    [SerializeField]
    public Button Up, Down, Left, Right;
    [SerializeField]
    public TMP_Text tempText;
}
