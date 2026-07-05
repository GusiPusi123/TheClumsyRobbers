// using UnityEngine;

// public class UpLeftArm : MonoBehaviour
// {
//     public Animator animator; // Объект Animator

//     void Update()
//     {
//         // Проверка на нажатие левой кнопки мыши
//         if (Input.GetKeyDown(KeyCode.E))
//         {
//             // Запуск анимации по триггеру или состоянию
//             animator.SetTrigger("UpArmMiddle");
//         }
//     }
// }

using UnityEngine;

public class UpRightArm : MonoBehaviour
{
    public Animator animator; // Объект Animator

    void Update()
    {
        // Проверка, удерживается ли кнопка E
        if (Input.GetKey(KeyCode.E))
        {
            // Запускаем анимацию поднятия руки
            animator.SetBool("UpRightArmMiddle", true);
        }
        else
        {
            // Запускаем анимацию опускания руки
            animator.SetBool("UpRightArmMiddle", false);
        }
    }
}
