// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class ArmController : MonoBehaviour
// {
//     public Animator animator; // Объект Animator

//     // Update is called once per frame
//     void Update()
//     {
//         //левая рука
//         if (Input.GetKey(KeyCode.Q))
//         {
//             // Запускаем анимацию поднятия руки
//             animator.SetBool("UpLeftArmMiddle", true);
//         }
//         else
//         {
//             // Запускаем анимацию опускания руки
//             animator.SetBool("UpLeftArmMiddle", false);
//         }
//         //правая рука
//         if (Input.GetKey(KeyCode.E))
//         {
//             // Запускаем анимацию поднятия руки
//             animator.SetBool("UpRightArmMiddle", true);
//         }
//         else
//         {
//             // Запускаем анимацию опускания руки
//             animator.SetBool("UpRightArmMiddle", false);
//         }
//     }
// }

using UnityEngine;

public class ArmController : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator компонент не найден!");
        }
    }

    void Update()
    {
        if (animator == null) return;

        SetArmState("UpLeftArmMiddle", Input.GetKey(KeyCode.Q));
        SetArmState("UpRightArmMiddle", Input.GetKey(KeyCode.E));
    }

    private void SetArmState(string parameterName, bool state)
    {
        animator.SetBool(parameterName, state);
    }
}