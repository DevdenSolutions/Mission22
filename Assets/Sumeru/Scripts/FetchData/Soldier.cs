
using UnityEngine;
[System.Serializable]
public class Soldier : ISoldier
{
    public string Name { get; set; }
    public string TrackingImageURL { get; set; }
    public string Hometown { get; set; }
    public string DOB { get; set; }
    public string DOD { get; set; }
    public string Designation { get; set; }
    public string Branch { get; set; }
    public string ShortBio { get; set; }
    public Texture2D TargetImage { get; set; }
    public GameObject ImageTargetGameObject { get; set; }
}
