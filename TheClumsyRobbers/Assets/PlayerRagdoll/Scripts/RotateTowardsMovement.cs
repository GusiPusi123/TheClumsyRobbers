// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class RotateTowardsMovement : MonoBehaviour
// {
//     public float speed = 5.0f;
//     public Transform target;
//     public float rotationSpeed = 5.0f;

//     private Vector3 currentDirection;
//     private Quaternion targetRotation;

//     void Start()
//     {
//         // Получаем направление от объекта к целевой точке
//         currentDirection = (target.position - transform.position).normalized;
//     }

//     void Update()
//     {
//         // Получаем новое направление
//         currentDirection = (target.position - transform.position).normalized;

//         // Рассчитываем целевую ротацию
//         targetRotation = Quaternion.LookRotation(currentDirection, Vector3.up);

//         // Аанимировка объекта
//         transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
//         transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
//     }
// }


using UnityEngine;

public class RotateTowardsMovement : MonoBehaviour
{
    public float rotationSpeed = 10f; // скорость поворота
    private Vector3 moveDirection;

    void Update()
    {
        // Получаем ввод по горизонтали и вертикали
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Создаем вектор направления движения
        moveDirection = new Vector3(moveX, 0, moveZ);

        if (moveDirection.magnitude > 0.1f)
        {
            // Определяем целевую ориентацию
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            // Плавный поворот к нужной ориентации
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}


// using UnityEngine;

// public class RotateTowardsMovement : MonoBehaviour
// {
//     public Transform target; // Цель, на которую нужно смотреть
//     public float rotationSpeed = 5f; // Скорость поворота

//     void Update()
//     {
//         if (target != null)
//         {
//             // Определяем направление к цели
//             Vector3 direction = target.position - transform.position;
//             direction.y = 0; // Игнорируем вертикальную компоненту для вращения по горизонтали

//             if (direction.sqrMagnitude > 0.01f)
//             {
//                 // Создаем желаемое вращение
//                 Quaternion targetRotation = Quaternion.LookRotation(direction);
//                 // Плавно поворачиваем в сторону цели
//                 transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
//             }
//         }
//     }
// }