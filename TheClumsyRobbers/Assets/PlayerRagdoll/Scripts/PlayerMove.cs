using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbody3D;
    [SerializeField] ConfigurableJoint mainJoint;
    Vector2 moveInputVector = Vector2.zero;
    bool JumpButtonPressed = false;
    float maxspeed = 3f;
    bool isGrounded = false;
    RaycastHit[] raycastHits = new RaycastHit[10];

    void Start()
    {
        // Инициализация, если нужно
    }

    void Update()
    {
        moveInputVector.x = Input.GetAxis("Horizontal");
        moveInputVector.y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpButtonPressed = true;
        }
    }

    void FixedUpdate()
    {
        // Проверка на землю
        isGrounded = false;
        int numberOfHits = Physics.SphereCastNonAlloc(
            rigidbody3D.position,
            0.1f,
            Vector3.down,
            raycastHits,
            0.5f
        );

        for (int i = 0; i < numberOfHits; i++)
        {
            if (raycastHits[i].transform.root == transform)
                continue;
            isGrounded = true;
            break;
        }

        if (!isGrounded)
        {
            rigidbody3D.AddForce(Vector3.down * 10f);
        }

        float inputMagnitude = moveInputVector.magnitude;

        if (inputMagnitude > 0)
        {
            // Создаем направление из входных данных
            Vector3 inputDirection = new Vector3(moveInputVector.x, 0, moveInputVector.y);
            // Нормализуем, чтобы движение было равномерным
            Vector3 moveDir = inputDirection.normalized;

            // Применяем силу в направлении движения
            rigidbody3D.AddForce(moveDir * 10f);

            // Ограничиваем максимальную скорость
            if (rigidbody3D.velocity.magnitude > maxspeed)
            {
                rigidbody3D.velocity = rigidbody3D.velocity.normalized * maxspeed;
            }
        }

        // Вращение цели (если нужно)
        if (inputMagnitude > 0)
        {
            // Создаем желаемое направление вращения
            Vector3 inputDirection = new Vector3(moveInputVector.x, 0, moveInputVector.y);
            Quaternion desiredDirection = Quaternion.LookRotation(inputDirection, transform.up);
            mainJoint.targetRotation = Quaternion.RotateTowards(mainJoint.targetRotation, desiredDirection, Time.fixedDeltaTime * 300);
        }

        // Прыжок
        if (isGrounded && JumpButtonPressed)
        {
            rigidbody3D.AddForce(transform.up * 20f, ForceMode.Impulse);
            JumpButtonPressed = false;
        }
    }
}