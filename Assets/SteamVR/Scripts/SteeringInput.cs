using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR.InteractionSystem;

public struct InputData
{
    public bool Accelerate;
    public bool Brake;
    public float TurnInput;
}

public interface IInput
{
    InputData GenerateInput();
}

public abstract class BaseInput : MonoBehaviour, IInput
{
    /// <summary>
    /// Override this function to generate an XY input that can be used to steer and control the car.
    /// </summary>
    public abstract InputData GenerateInput();
}

public class SteeringInput : BaseInput
{
    public CircularDrive drive;

    //public string TurnInputName = "Horizontal";
    public string AccelerateButtonName = "Accelerate";
    public string BrakeButtonName = "Brake";

    public override InputData GenerateInput()
    {
        return new InputData
        {
            Accelerate = Input.GetButton(AccelerateButtonName),
            Brake = Input.GetButton(BrakeButtonName),
            TurnInput = Input.GetAxis("Horizontal")//drive.outAngle / 360f
        };
    }
}