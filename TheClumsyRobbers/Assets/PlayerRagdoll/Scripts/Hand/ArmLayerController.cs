using UnityEngine;

public class ArmLayerController : MonoBehaviour
{
    public Animator animator;
    public int LeftArm = 2; // индекс слоя для левой руки
    public int RightArm = 3; // индекс слоя для правой руки

    void Update()
    {
        // Управление левой рукой
        if (Input.GetKey(KeyCode.Q))
        {
            // Включить слой для левой руки
            animator.SetLayerWeight(LeftArm, 1);
        }
        else
        {
            // Выключить слой
            animator.SetLayerWeight(LeftArm, 0);
        }

        // Управление правой рукой
        if (Input.GetKey(KeyCode.Q))
        {
            animator.SetLayerWeight(RightArm, 1);
        }
        else
        {
            animator.SetLayerWeight(RightArm, 0);
        }
    }
}