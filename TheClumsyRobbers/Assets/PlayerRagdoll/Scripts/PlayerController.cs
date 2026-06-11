using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody mainRigidbody; // Основная часть, которой управляем
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private bool isGrounded;

    void Update()
    {
        // Проверка на землю
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // Ввод движения
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(moveX, 0, moveZ).normalized;

        // Передача движения основной части
        Vector3 targetVelocity = moveDir * moveSpeed;
        Vector3 currentVelocity = mainRigidbody.velocity;

        // Обновляем горизонтальные компоненты скорости
        mainRigidbody.velocity = new Vector3(targetVelocity.x, currentVelocity.y, targetVelocity.z);

        // Прыжок
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            mainRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
