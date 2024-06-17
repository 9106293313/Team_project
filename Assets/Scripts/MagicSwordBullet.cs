using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class MagicSwordBullet : MonoBehaviour
{
    public Transform mainObj; 
    public float rotationSpeed = 1.0f; 

    private ParticleSystem m_particleSystem;
    MinMaxCurve rotationCurveX;
    MinMaxCurve rotationCurveY;

    float fixedRotation = 57.3f;

    void Start()
    {
        m_particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (mainObj != null && m_particleSystem != null)
        {
            Vector3 rotationEulerAngles = mainObj.rotation.eulerAngles;
            float angleRotateNum = 22.5f;

            if(rotationEulerAngles.z>=-22.5f && rotationEulerAngles.z < angleRotateNum)
            {
                rotationCurveX = new MinMaxCurve(0 / fixedRotation);
                rotationCurveY = new MinMaxCurve(60 / fixedRotation);
            }
            else if (rotationEulerAngles.z >= angleRotateNum && rotationEulerAngles.z < angleRotateNum*3)
            {
                rotationCurveX = new MinMaxCurve(-30 / fixedRotation);
                rotationCurveY = new MinMaxCurve(40 / fixedRotation);
            }
            else if(rotationEulerAngles.z>= angleRotateNum*3 && rotationEulerAngles.z < angleRotateNum*5)
            {
                rotationCurveX = new MinMaxCurve(-60 / fixedRotation);
                rotationCurveY = new MinMaxCurve(0 / fixedRotation);
            }
            else if (rotationEulerAngles.z >= angleRotateNum * 5 && rotationEulerAngles.z < angleRotateNum * 7)
            {
                rotationCurveX = new MinMaxCurve(30 / fixedRotation);
                rotationCurveY = new MinMaxCurve(40 / fixedRotation);
            }
            else if (rotationEulerAngles.z >= angleRotateNum * 7 && rotationEulerAngles.z < angleRotateNum * 9)
            {
                rotationCurveX = new MinMaxCurve(0 / fixedRotation);
                rotationCurveY = new MinMaxCurve(-60 / fixedRotation);
            }
            else if (rotationEulerAngles.z >= angleRotateNum * 9 && rotationEulerAngles.z < angleRotateNum * 11)
            {
                rotationCurveX = new MinMaxCurve(30 / fixedRotation);
                rotationCurveY = new MinMaxCurve(-40 / fixedRotation);
            }
            else if (rotationEulerAngles.z >= angleRotateNum * 11 && rotationEulerAngles.z < angleRotateNum * 13)
            {
                rotationCurveX = new MinMaxCurve(-60 / fixedRotation);
                rotationCurveY = new MinMaxCurve(0 / fixedRotation);
            }
            else if (rotationEulerAngles.z >= angleRotateNum * 13 && rotationEulerAngles.z < angleRotateNum * 15)
            {
                rotationCurveX = new MinMaxCurve(-30 / fixedRotation);
                rotationCurveY = new MinMaxCurve(-40 / fixedRotation);
            }


            var mainModule = m_particleSystem.main;
            mainModule.startRotationX = rotationCurveX;
            mainModule.startRotationY = rotationCurveY;
        }
    }
}
