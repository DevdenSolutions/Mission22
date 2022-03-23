using UnityEngine;

public abstract class MainState : MonoBehaviour
{
    public abstract void ResetEverything();
    public abstract void InProgress();
    public abstract void Ended();


}
