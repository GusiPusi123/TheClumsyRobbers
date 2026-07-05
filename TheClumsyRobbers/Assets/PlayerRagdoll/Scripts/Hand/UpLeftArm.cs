using UnityEngine;

public class UpLeftArm : MonoBehaviour
{
    public Animator animator; // Объект Animator

    void Update()
    {
        // Проверка, удерживается ли кнопка E
        if (Input.GetKey(KeyCode.Q))
        {
            // Запускаем анимацию поднятия руки
            animator.SetBool("UpLeftArmMiddle", true);
        }
        else
        {
            // Запускаем анимацию опускания руки
            animator.SetBool("UpLeftArmMiddle", false);
        }
    }
}
