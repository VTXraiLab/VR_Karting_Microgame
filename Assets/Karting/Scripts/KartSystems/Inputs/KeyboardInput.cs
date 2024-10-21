using UnityEngine;
using KartGame.KartSystems;

public class KeyboardInput : BaseInput
{
    //public string TurnInputName = "Horizontal";
    public float turnAngle;
    public bool accelerate;
    public bool brake;

    public override InputData GenerateInput() {
        return new InputData
        {
            Accelerate = accelerate,
            Brake = brake,
            TurnInput = turnAngle
        };
    }
}