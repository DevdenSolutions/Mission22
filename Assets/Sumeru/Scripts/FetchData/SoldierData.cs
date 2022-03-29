
public interface ISoldier
{
    string Name { get; set; }

    string TrackingImageURL { get; set; }

    string Hometown { get; set; }

    string DOB { get; set; }

    string DOD { get; set; }

    string Designation { get; set; }

    string Branch { get; set; }

    string ShortBio { get; set; }

    UnityEngine.Texture2D TargetImage { get; set; }

    UnityEngine.GameObject ImageTargetGameObject { get; set; }
}
