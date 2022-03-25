
using UnityEngine;
[System.Serializable]
public class Soldier : iSoldier
{
    public string Name { get; set; }
    public string TrackingImageURL { get; set; }
    public string Designation { get; set; }
    public string Type { get; set; }
    public Texture2D TargetImage { get; set; }
    public GameObject ImageTargetGameObject { get; set; }
}
