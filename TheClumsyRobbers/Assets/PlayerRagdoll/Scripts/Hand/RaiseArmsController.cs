using UnityEngine;

public class RaiseArmsController : MonoBehaviour
{
    public Transform leftArm; // ссылка на левую руку
    public Transform rightArm; // ссылка на правую руку
    public float raiseHeight = 1.0f; // насколько поднять руки
    public float smoothSpeed = 5f; // скорость поднятия

    private Vector3 leftArmInitialPos;
    private Vector3 rightArmInitialPos;
    private Vector3 leftArmRaisedPos;
    private Vector3 rightArmRaisedPos;

    private bool isRaising = false;

    void Start()
    {
        if (leftArm != null)
        {
            leftArmInitialPos = leftArm.localPosition;
            leftArmRaisedPos = leftArmInitialPos + Vector3.up * raiseHeight;
        }

        if (rightArm != null)
        {
            rightArmInitialPos = rightArm.localPosition;
            rightArmRaisedPos = rightArmInitialPos + Vector3.up * raiseHeight;
        }
    }

    void Update()
    {
        if (isRaising)
        {
            if (leftArm != null)
                leftArm.localPosition = Vector3.Lerp(leftArm.localPosition, leftArmRaisedPos, Time.deltaTime * smoothSpeed);
            if (rightArm != null)
                rightArm.localPosition = Vector3.Lerp(rightArm.localPosition, rightArmRaisedPos, Time.deltaTime * smoothSpeed);
        }
        else
        {
            if (leftArm != null)
                leftArm.localPosition = Vector3.Lerp(leftArm.localPosition, leftArmInitialPos, Time.deltaTime * smoothSpeed);
            if (rightArm != null)
                rightArm.localPosition = Vector3.Lerp(rightArm.localPosition, rightArmInitialPos, Time.deltaTime * smoothSpeed);
        }
    }

    public void RaiseArms()
    {
        isRaising = true;
    }

    public void LowerArms()
    {
        isRaising = false;
    }
}