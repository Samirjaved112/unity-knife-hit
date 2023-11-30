using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LogRotation : MonoBehaviour
{
   
    
    [SerializeField] 
    private RotationElement[] rotationPattern;
    private WheelJoint2D wheelJoint;
    private JointMotor2D motor;
   

    private void Start()
    {
       
        wheelJoint = GetComponent<WheelJoint2D>();
        motor = new JointMotor2D();
        StartCoroutine(PlayRotationPattern());
    }

    private IEnumerator PlayRotationPattern()
    {
        int rotationIndex = 0;
        while (true)
        {
            float startTime = Time.time;
            float startSpeed = motor.motorSpeed;
            float targetSpeed = rotationPattern[rotationIndex].Speed;
            float duration = rotationPattern[rotationIndex].Duration;

            while (Time.time - startTime < duration)
            {
                float t = (Time.time - startTime) / duration;
                motor.motorSpeed = Mathf.Lerp(startSpeed, targetSpeed, t);
                motor.maxMotorTorque = 10000;
                wheelJoint.motor = motor;
                yield return null;
            }

            rotationIndex++;
            rotationIndex = rotationIndex < rotationPattern.Length ? rotationIndex : 0;
        }
    }
   
}
[System.Serializable]
public struct RotationElement
{
    public float Speed;
    public float Duration;
}
