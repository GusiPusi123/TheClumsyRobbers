// using UnityEngine;

// public class PlayerMove : MonoBehaviour
// {
//     public Rigidbody rb; // Rigidbody персонажа
//     public float moveForce = 50f; // сила для передвижения
//     public float maxSpeed = 5f; // максимальная скорость
//     public float jumpForce = 4000f; // сила прыжка
//     public Transform groundCheck; // точка для проверки земли
//     public LayerMask groundLayer; // слой, считающийся землей

//     private bool isGrounded;

//     void FixedUpdate()
//     {
//         // Получение ввода с клавиатуры
//         float moveHorizontal = Input.GetAxis("Horizontal");
//         float moveVertical = Input.GetAxis("Vertical");

//         // Создаем вектор направления
//         Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

//         // Ограничение скорости
//         if (rb.velocity.magnitude < maxSpeed)
//         {
//             rb.AddForce(movement * moveForce);
//         }

//         // Проверка, на земле ли персонаж
//         isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);

//         // Прыжок
//         if (Input.GetButtonDown("Jump") && isGrounded)
//         {
//             rb.AddForce(Vector3.up * jumpForce);
//         }
//     }
// }


using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody rb; // Rigidbody персонажа
    public float moveForce = 50f; // сила для передвижения
    public float maxSpeed = 5f; // максимальная скорость
    public float jumpForce = 4000f; // сила прыжка
    public Transform groundCheck; // точка для проверки земли
    public LayerMask groundLayer; // слой, считающийся землей

    public float rotationSpeed = 10f; // скорость поворота

    private bool isGrounded;

    void FixedUpdate()
    {
        // Получение ввода с клавиатуры
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Создаем вектор направления
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Проверка, есть ли движение
        if (movement.magnitude > 0.1f)
        {
            // Вращение персонажа в сторону направления движения
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        // Ограничение скорости
        if (rb.velocity.magnitude < maxSpeed && movement.magnitude > 0.1f)
        {
            rb.AddForce(movement * moveForce);
        }

        // Проверка, на земле ли персонаж
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundLayer);

        // Прыжок
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }
}