using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour 
{
    public event Action PositiveSwinging; 
    public event Action NegativeSwinging; 
    public event Action MotorTurnedOff; 
    public event Action LimitsTurnedOff; 
    public event Action LimitsTurnedOn; 
    public event Action ProjectileLaunched; 
    public event Action LeverDroppedDown; 

    public bool IsPositiveSwing => Input.GetKeyDown(KeyCode.Q);
    public bool IsNegativeSwing => Input.GetKeyDown(KeyCode.A);
    public bool IsMotorTurnedOff => Input.GetKeyDown(KeyCode.Z);
    public bool IsLimitsTurnedOff => Input.GetKeyDown(KeyCode.W);
    public bool IsLimitsTurnedOn => Input.GetKeyDown(KeyCode.S);
    public bool IsLaunchProjectile => Input.GetKeyDown(KeyCode.R);
    public bool IsDropDownLever => Input.GetKeyDown(KeyCode.F);

    private void Update()
    {
        if(IsPositiveSwing)
            PositiveSwinging?.Invoke();

        if(IsNegativeSwing)
            NegativeSwinging?.Invoke();

        if(IsMotorTurnedOff)
            MotorTurnedOff?.Invoke();

        if(IsLimitsTurnedOff)
            LimitsTurnedOff?.Invoke();

        if(IsLimitsTurnedOn)
            LimitsTurnedOn?.Invoke();

        if(IsLaunchProjectile)
            ProjectileLaunched?.Invoke();

        if(IsDropDownLever)
            LeverDroppedDown?.Invoke();
    }
}