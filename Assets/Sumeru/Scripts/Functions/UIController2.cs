using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController2 : MonoBehaviour
{
    [SerializeField] Slider _positionSlider;
    [SerializeField] Slider _rotationSlider;
    [SerializeField] TMP_Text _positionText;
    [SerializeField] TMP_Text _rotationText;
    private void Start()
    {
        Debug.LogError("The angle of UI: " + transform.localRotation);
    }


    public void MoveFrontAndBack()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, _positionSlider.value, transform.localPosition.z);
        _positionText.text = _positionSlider.value.ToString();
    }

    public void Rotate()
    {
        transform.localRotation = new Quaternion(_rotationSlider.value, transform.localRotation.y,transform.localRotation.z, transform.localRotation.w);
        _rotationText.text = _rotationSlider.value.ToString();
    }
}
