using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    private const int NegativeSign = -1;

    [SerializeField] private PlayerInput _input;
    [SerializeField] private List<HingeJoint> _hingeJoints;
    [SerializeField, Range(-100, 100)] private float _targetVelocity;
    [SerializeField, Range(0, 1000)] private float _force;

    private void Awake() => UseLimits(true);

    private void OnEnable()
    {
        _input.PositiveSwinging += OnPositiveSwinging;
        _input.NegativeSwinging += OnNegativeSwinging;
        _input.MotorTurnedOff += OnMotorTurnedOff;
        _input.LimitsTurnedOff += OnLimitsTurnedOff;
        _input.LimitsTurnedOn += OnLimitsTurnedOn;
    }

    private void OnDisable()
    {
        _input.PositiveSwinging -= OnPositiveSwinging;
        _input.NegativeSwinging -= OnNegativeSwinging;
        _input.MotorTurnedOff -= OnMotorTurnedOff;
        _input.LimitsTurnedOff -= OnLimitsTurnedOff;
        _input.LimitsTurnedOn -= OnLimitsTurnedOn;
    }

    private void OnPositiveSwinging()
    {
        UseMotor(true);
        _targetVelocity = Mathf.Abs(_targetVelocity);
        SetTargetVelocity(_targetVelocity);
    }

    private void OnNegativeSwinging()
    {
        UseMotor(true);
        _targetVelocity *= NegativeSign;
        SetTargetVelocity(_targetVelocity);
    }

    private void OnMotorTurnedOff() => UseMotor(false);
    private void OnLimitsTurnedOff() => UseLimits(false);
    private void OnLimitsTurnedOn() => UseLimits(true);

    private void UseLimits(bool isUseLimits)
    {
        foreach (HingeJoint hingeJoint in _hingeJoints)
            hingeJoint.useLimits = isUseLimits;
    }

    private void UseMotor(bool isUseMotor)
    {
        foreach (HingeJoint hingeJoint in _hingeJoints)
            hingeJoint.useMotor = isUseMotor;
    }

    private void SetTargetVelocity(float targetVelocity)
    {
        foreach (HingeJoint hingeJoint in _hingeJoints)
        {
            JointMotor jointMotor = hingeJoint.motor;
            jointMotor.targetVelocity = targetVelocity;
            jointMotor.force = _force;
            hingeJoint.motor = jointMotor;
        }
    }
}