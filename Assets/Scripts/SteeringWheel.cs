using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using KartGame.KartSystems;
using Valve.VR;

public class SteeringWheel : CircularDrive
{
    public KeyboardInput wheelController;
    public SteamVR_Action_Boolean actionAccelerate;
    public SteamVR_Action_Boolean actionBrake;
    //public SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.Any;
    
    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        bool accelerate = false;
        bool brake = false;

        if (actionAccelerate != null)
        {
            accelerate = actionAccelerate.state;
        }

        if (actionBrake != null)
        {
            brake = actionBrake.state;
        }

        if (wheelController != null)
        {
            wheelController.turnAngle = -outAngle / 360f;
            wheelController.accelerate = accelerate;
            wheelController.brake = brake;
        }
    }
}
