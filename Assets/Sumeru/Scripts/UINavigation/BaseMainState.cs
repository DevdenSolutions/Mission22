
using UnityEngine;
using Lean.Gui;
public class BaseMainState : MainState
{
    [SerializeField] string StateText;
    [SerializeField] LeanToggle _leanToggle;
    public override void Ended()
    {
        print(StateText + " Ended");
    }

    public override void InProgress()
    {
        
    }

    public override void ResetEverything()
    {
        print(StateText + " Started");
        _leanToggle.TurnOn();
    }
}
