using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageTargetBehaviour : MonoBehaviour
{
    #region singleton

    private static ManageTargetBehaviour _instance;

    public static ManageTargetBehaviour Instance { get { return _instance; } }


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


    public void OnTargetFound()
    {

    }

    public void OnTargetLost()
    {

    }
}
