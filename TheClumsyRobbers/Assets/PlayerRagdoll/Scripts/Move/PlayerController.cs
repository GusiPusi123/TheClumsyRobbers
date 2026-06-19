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

    [Header("Rotation Correction")]
    public float rotationOffsetY = 0f; // Смещение по Y для корректировки ориентации модели

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
        mainRigidbody.velocity = new Vector3(targetVelocity.x, currentVelocity.y, targetVelocity.z);

        // --- Поворот в сторону движения ---
        if (moveDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            // Добавляем смещение по Y, чтобы учесть ориентацию модели
            targetRotation *= Quaternion.Euler(0, rotationOffsetY, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // --- Анимации ---
        if (animator != null)
        {
            bool isMoving = moveDir.sqrMagnitude > 0.01f;
            animator.SetBool("Walk", isMoving);
            animator.SetBool("IsGrounded", isGrounded);
        }

        // --- Прыжок ---
        if (Input.GetButtonDown("Jump") && isGrounded && Time.time - lastJumpTime >= jumpCooldown)
        // if (Input.GetButtonDown("Jump") && isGrounded)
        {
            mainRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            lastJumpTime = Time.time;

            if (animator != null)
            {
                animator.SetTrigger("Jump");
            }
        }
    }
}


// using UnityEngine;

// public class Controllmaksim : MonoBehaviour
// {
//     public Rigidbody mainRigidbody; // Основная часть, которой управляем
//     public float moveSpeed = 5f;
//     public float jumpForce = 7f;
//     public LayerMask groundLayer;
//     public Transform groundCheck;
//     public float groundCheckRadius = 0.2f;
//     public float rotationSpeed = 10f; // скорость поворота

//     [Header("Animation Settings")]
//     public Animator animator; // Ссылка на компонент Animator

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

//         // --- ИСПРАВЛЕНИЕ: РАЗВОРOT В СТОРОНУ ДВИЖЕНИЯ ---
//         // Если вектор движения не равен нулю (мы жмем на кнопки)
//         if (moveDir.sqrMagnitude > 0.01f)
//         {
//             // Определяем, куда нужно смотреть
//             Quaternion targetRotation = Quaternion.LookRotation(moveDir);
//             // Плавно разворачиваем игровой объект (весь скрипт) в эту сторону
//             transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
//         }
//         // ------------------------------------------------

//         // --- УПРАВЛЕНИЕ АНИМАЦИЕЙ ---
//         if (animator != null)
//         {
//             // Проверяем, идет ли игрок прямо сейчас
//             bool isMoving = moveDir.sqrMagnitude > 0.01f;
            
//             animator.SetBool("Walk", isMoving);
//             animator.SetBool("IsGrounded", isGrounded); // Передаем, на земле ли мы
//         }
//         // ----------------------------

//         // Прыжок
//         if (Input.GetButtonDown("Jump") && isGrounded)
//         {
//             // Физический толчок вверх
//             mainRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

//             // Активация анимации прыжка через ТРИГГЕР
//             if (animator != null)
//             {
//                 animator.SetTrigger("Jump");
//             }
//         }
//     }
// }