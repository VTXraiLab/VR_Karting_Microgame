using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using KartGame.KartSystems;
using Valve.VR;

public class SteeringWheel : MonoBehaviour
{
    public KeyboardInput wheelController;
    public SteamVR_Action_Boolean actionAccelerate;
    public SteamVR_Action_Boolean actionBrake;

    public SteamVR_Action_Boolean grabAction; // SteamVR grab action
    public Transform leftHand;
    public Transform rightHand;

    private bool isLeftHandGrabbing;
    private bool isRightHandGrabbing;
    private Vector3 leftHandGrabOffset;
    private Vector3 rightHandGrabOffset;
    private float leftHandInitialAngle;
    private float rightHandInitialAngle;

    // Variable to track the current angle of the wheel with respect to its start position
    public float currentWheelAngle = 0f;

    private void Update()
    {
        HandleInput();

        if (isLeftHandGrabbing || isRightHandGrabbing)
        {
            UpdateSteeringWheelRotation();
        }

        UpdateWheelController();
    }

    private void UpdateWheelController()
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
            wheelController.turnAngle = -currentWheelAngle / 360f;
            wheelController.accelerate = accelerate;
            wheelController.brake = brake;
        }
    }

    private void HandleInput()
    {

        // Left hand grab and release
        if (grabAction.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            Grab(leftHand, true);
        }
        if (grabAction.GetStateUp(SteamVR_Input_Sources.LeftHand))
        {
            Release(true);
        }

        // Right hand grab and release
        if (grabAction.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            Grab(rightHand, false);
        }
        if (grabAction.GetStateUp(SteamVR_Input_Sources.RightHand))
        {
            Release(false);
        }
    }

    private void Grab(Transform hand, bool isLeftHand)
    {
        Vector3 wheelToHand = hand.position - transform.position;
        Vector3 grabOffset = wheelToHand.normalized;
        float initialAngle = Vector3.SignedAngle(transform.up, grabOffset, transform.forward);

        if (isLeftHand)
        {
            isLeftHandGrabbing = true;
            leftHandGrabOffset = grabOffset;
            leftHandInitialAngle = initialAngle;
        }
        else
        {
            isRightHandGrabbing = true;
            rightHandGrabOffset = grabOffset;
            rightHandInitialAngle = initialAngle;
        }
    }

    private void Release(bool isLeftHand)
    {
        if (isLeftHand)
        {
            isLeftHandGrabbing = false;
        }
        else
        {
            isRightHandGrabbing = false;
        }
    }

    private void UpdateSteeringWheelRotation()
    {
        float deltaAngle = 0f;

        if (isLeftHandGrabbing && isRightHandGrabbing)
        {
            // Two-handed grab: calculate rotation based on both hands
            deltaAngle = (CalculateDeltaAngle(leftHand, leftHandInitialAngle) + CalculateDeltaAngle(rightHand, rightHandInitialAngle)) / 2f;
        }
        else if (isLeftHandGrabbing)
        {
            // Single left hand rotation
            deltaAngle = CalculateDeltaAngle(leftHand, leftHandInitialAngle);
        }
        else if (isRightHandGrabbing)
        {
            // Single right hand rotation
            deltaAngle = CalculateDeltaAngle(rightHand, rightHandInitialAngle);
        }

        // Apply the rotation around the forward axis (usually Z-axis)
        transform.Rotate(Vector3.forward, deltaAngle, Space.Self);

        // Update the cumulative wheel angle
        currentWheelAngle += deltaAngle;

        // Optional: Wrap or clamp currentWheelAngle (e.g., -180 to 180 degrees)
        currentWheelAngle = Mathf.Clamp(currentWheelAngle, -360f, 360f);

        // Update the initial angles for next frame
        if (isLeftHandGrabbing)
        {
            leftHandInitialAngle = Vector3.SignedAngle(transform.up, (leftHand.position - transform.position).normalized, transform.forward);
        }
        if (isRightHandGrabbing)
        {
            rightHandInitialAngle = Vector3.SignedAngle(transform.up, (rightHand.position - transform.position).normalized, transform.forward);
        }
    }

    private float CalculateDeltaAngle(Transform hand, float initialAngle)
    {
        Vector3 currentDirection = (hand.position - transform.position).normalized;
        float currentAngle = Vector3.SignedAngle(transform.up, currentDirection, transform.forward);
        return currentAngle - initialAngle;
    }
}