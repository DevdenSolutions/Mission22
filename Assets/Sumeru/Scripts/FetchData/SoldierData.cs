
public interface iSoldier
{
    string Name { get; set; }

    string TrackingImageURL { get; set; }

    string Designation { get; set; }

    string Type { get; set; }

    UnityEngine.Texture2D TargetImage { get; set; }

    UnityEngine.GameObject ImageTargetGameObject { get; set; }
}
