using UnityEngine;

public class GangbeastsController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Transform modelTransform; // Для разворота модели
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody не найден на объекте " + gameObject.name);
        }

        if (modelTransform == null)
        {
            modelTransform = transform; // Если не указано, используем сам объект
        }
    }

    void Update()
    {
        if (rb == null) return; // Если Rigidbody отсутствует, не выполнять движение

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        // Ходьба
        if (movement.magnitude >= 0.1f)
        {
            // Перемещение
            Vector3 moveDirection = movement * moveSpeed;
            Vector3 newPosition = transform.position + moveDirection * Time.deltaTime;
            rb.MovePosition(newPosition);

            // Разворот модели
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            modelTransform.rotation = Quaternion.Slerp(modelTransform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // Прыжок
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}