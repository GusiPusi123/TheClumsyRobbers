// using UnityEngine;

// public class PlayerController : MonoBehaviour
// {
//     public Rigidbody mainRigidbody; // Основная часть, которой управляем
//     public float moveSpeed = 5f;
//     public float jumpForce = 7f;
//     public LayerMask groundLayer;
//     public Transform groundCheck;
//     public float groundCheckRadius = 0.2f;
//     public float rotationSpeed = 10f; // скорость поворота

//     private bool isGrounded;

//     void Update()
//     {
//         // Проверка на землю
//         isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

//         // Ввод движения
//         float moveX = Input.GetAxis("Horizontal");
//         float moveZ = Input.GetAxis("Vertical");
//         Vector3 moveDir = new Vector3(moveX, 0, moveZ).normalized;

//         // Передача движения основной части
//         Vector3 targetVelocity = moveDir * moveSpeed;
//         Vector3 currentVelocity = mainRigidbody.velocity;

//         // Обновляем горизонтальные компоненты скорости
//         mainRigidbody.velocity = new Vector3(targetVelocity.x, currentVelocity.y, targetVelocity.z);

//         // Разворот в сторону движения
//         if (moveDir.sqrMagnitude > 0.1f)
//         {
//             // Цель - направление движения
//             Quaternion targetRotation = Quaternion.LookRotation(moveDir);
//             // Плавный разворот
//             transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
//         }

//         // Прыжок
//         if (Input.GetButtonDown("Jump") && isGrounded)
//         {
//             mainRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
//         }
//     }
// }


// using UnityEngine;

// public class PlayerController : MonoBehaviour
// {
//     public Rigidbody mainRigidbody; // Основная часть, которой управляем
//     public float moveSpeed = 5f;
//     public float jumpForce = 7f;
//     public LayerMask groundLayer;
//     public Transform groundCheck;
//     public float groundCheckRadius = 0.2f;
//     public float rotationSpeed = 10f; // скорость поворота

//     private bool isGrounded;

//     void Update()
//     {
//         // Проверка наличия mainCamera
//         Transform camTransform = Camera.main != null ? Camera.main.transform : null;
//         if (camTransform == null)
//         {
//             Debug.LogError("Main Camera не найдена. Убедитесь, что у камеры есть тег MainCamera.");
//             return;
//         }

//         // Проверка groundCheck
//         if (groundCheck == null)
//         {
//             Debug.LogError("groundCheck не назначен! Назначьте объект в инспекторе.");
//             return;
//         }

//         // Проверка на землю
//         isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

//         // Ввод движения
//         float moveX = Input.GetAxis("Horizontal");
//         float moveZ = Input.GetAxis("Vertical");

//         // Создаем направление движения, основанное на камере
//         Vector3 moveDir = (camTransform.forward * moveZ + camTransform.right * moveX).normalized;
//         moveDir.y = 0; // чтобы не было наклонов вверх/вниз

//         // Передача движения основной части
//         Vector3 targetVelocity = moveDir * moveSpeed;
//         Vector3 currentVelocity = mainRigidbody.velocity;

//         // Обновляем горизонтальные компоненты скорости
//         mainRigidbody.velocity = new Vector3(targetVelocity.x, currentVelocity.y, targetVelocity.z);

//         // Разворот в сторону движения
//         if (moveDir.sqrMagnitude > 0.1f)
//         {
//             // Цель - направление движения
//             Quaternion targetRotation = Quaternion.LookRotation(moveDir);
//             // Плавный разворот
//             transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
//         }

//         // Прыжок
//         if (Input.GetButtonDown("Jump") && isGrounded)
//         {
//             mainRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
//         }
//     }
// }

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody mainRigidbody; // Основная часть, которой управляем
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public float rotationSpeed = 10f; // скорость поворота
    public float jumpCooldown = 1f; // интервал между прыжками в секундах

    [Header("Animation Settings")]
    public Animator animator; // Ссылка на компонент Animator

    private float lastJumpTime = -Mathf.Infinity; // время последнего прыжка
    private bool isGrounded;

    void Update()
    {
        // Проверка наличия mainCamera
        Transform camTransform = Camera.main != null ? Camera.main.transform : null;
        if (camTransform == null)
        {
            Debug.LogError("Main Camera не найдена. Убедитесь, что у камеры есть тег MainCamera.");
            return;
        }

        // Проверка groundCheck
        if (groundCheck == null)
        {
            Debug.LogError("groundCheck не назначен! Назначьте объект в инспекторе.");
            return;
        }

        // Проверка на землю
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // Ввод движения
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Создаем направление движения, основанное на камере
        Vector3 moveDir = (camTransform.forward * moveZ + camTransform.right * moveX).normalized;
        moveDir.y = 0; // чтобы не было наклонов вверх/вниз

        // Передача движения основной части
        Vector3 targetVelocity = moveDir * moveSpeed;
        Vector3 currentVelocity = mainRigidbody.velocity;

        // Обновляем горизонтальные компоненты скорости
        mainRigidbody.velocity = new Vector3(targetVelocity.x, currentVelocity.y, targetVelocity.z);

        // --- ИСПРАВЛЕНИЕ: РАЗВОРOT В СТОРОНУ ДВИЖЕНИЯ ---
        // Если вектор движения не равен нулю (мы жмем на кнопки)
        if (moveDir.sqrMagnitude > 0.01f)
        {
            // Определяем, куда нужно смотреть
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            // Плавно разворачиваем игровой объект (весь скрипт) в эту сторону
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        // ------------------------------------------------

        // --- УПРАВЛЕНИЕ АНИМАЦИЕЙ ---
        if (animator != null)
        {
            // Проверяем, идет ли игрок прямо сейчас
            bool isMoving = moveDir.sqrMagnitude > 0.01f;
            
            animator.SetBool("Walk", isMoving);
            animator.SetBool("IsGrounded", isGrounded); // Передаем, на земле ли мы
        }
        // ----------------------------

        // Прыжок
        if (Input.GetButtonDown("Jump") && isGrounded && Time.time - lastJumpTime >= jumpCooldown)
        {
            // Физический толчок вверх
            mainRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            lastJumpTime = Time.time;

            // Активация анимации прыжка через ТРИГГЕР
            if (animator != null)
            {
                animator.SetTrigger("Jump");
            }
        }
    }
}