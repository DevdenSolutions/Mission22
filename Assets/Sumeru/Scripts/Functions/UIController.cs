using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Button Up, Down, Left, Right;
    [SerializeField]
    TMP_Text tempText;
    void Start()
    {
        Up = References.Instance.Up;
        Down = References.Instance.Down;
        Left = References.Instance.Left;
        Right = References.Instance.Right;
        tempText = References.Instance.tempText;

        Up.onClick.AddListener(GoUp);
        Down.onClick.AddListener(GoDown);
        Left.onClick.AddListener(GoLeft);
        Right.onClick.AddListener(GoRight);
    }

    // Update is called once per frame
    void Update()
    {
        tempText.text = transform.localPosition.ToString();
    }

    void GoUp()
    {
        transform.localPosition = new Vector3(transform.localPosition.x,transform.localPosition.y+.1f,transform.localPosition.z);
    }
    void GoDown()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - .1f, transform.localPosition.z);
    }

    void GoLeft()
    {
        transform.localPosition = new Vector3(transform.localPosition.x - .1f, transform.localPosition.y, transform.localPosition.z);
    }

    void GoRight()
    {
        transform.localPosition = new Vector3(transform.localPosition.x + .1f, transform.localPosition.y, transform.localPosition.z);
    }
}
