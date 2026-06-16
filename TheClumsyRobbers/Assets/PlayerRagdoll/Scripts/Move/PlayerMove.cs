// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerMove : MonoBehaviour
// {
//     public float Speed;
//     public float strafeSpeed;
//     public float JumpForce;

//     public Rigidbody Hips;
//     public bool isGrounded;
//     // Start is called before the first frame update
//     void Start()
//     {
//         Hips = GetComponent<RigidBody>();    
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }


using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody rb; // Rigidbody персонажа
    public float moveForce = 50f; // сила для передвижения
    public float maxSpeed = 5f; // максимальная скорость

    void FixedUpdate()
    {
        // Получение ввода с клавиатуры
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D или стрелки влево/вправо
        float moveVertical = Input.GetAxis("Vertical"); // W/S или стрелки вверх/вниз

        // Создаем вектор направления
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Ограничение скорости
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(movement * moveForce);
        }
    }
}