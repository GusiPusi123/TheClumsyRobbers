// using UnityEngine;

// public class CameraController : MonoBehaviour
// {
//     public Transform target; // Игрок, вокруг которого вращается камера
//     public Transform head; // Объект головы, который нужно поворачивать
//     public float distance = 5f; // Расстояние до игрока
//     public float xSpeed = 200f; // Чувствительность по горизонтали
//     public float ySpeed = 200f; // Чувствительность по вертикали
//     public float yMinLimit = 0f; // Минимальный угол по вертикали
//     public float yMaxLimit = 80f;  // Максимальный угол по вертикали

//     private float currentX = 0f;
//     private float currentY = 0f;

//     void Start()
//     {
//         Vector3 angles = transform.eulerAngles;
//         currentX = angles.y;
//         currentY = angles.x;

//         // Сделать курсор невидимым и захватить его
//         Cursor.lockState = CursorLockMode.Locked;
//         Cursor.visible = false;
//     }

//     void LateUpdate()
//     {
//         if (target)
//         {
//             // Получаем ввод мыши
//             currentX += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
//             currentY -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

//             // Ограничиваем вертикальный угол
//             currentY = Mathf.Clamp(currentY, yMinLimit, yMaxLimit);

//             // Создаем вращение
//             Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

//             // Обновляем позицию камеры
//             Vector3 position = rotation * new Vector3(0, 0, -distance) + target.position;

//             transform.rotation = rotation;
//             transform.position = position;

//             // Поворот головы в сторону камеры
//             if (head != null)
//             {
//                 // Рассчитываем локальный угол головы по горизонтали
//                 float headYRotation = currentX;
//                 // Рассчитываем локальный угол головы по вертикали (можете ограничить, если нужно)
//                 float headXRotation = currentY;

//                 // Обновляем вращение головы
//                 head.localRotation = Quaternion.Euler(headXRotation, headYRotation, 0);
//             }
//         }
//     }
// }

using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Игрок, вокруг которого вращается камера
    public float distance = 5f; // Расстояние до игрока
    public float xSpeed = 120f; // Чувствительность по горизонтали
    public float ySpeed = 120f; // Чувствительность по вертикали
    public float yMinLimit = -20f; // Минимальный угол по вертикали
    public float yMaxLimit = 80f;  // Максимальный угол по вертикали

    private float currentX = 0f;
    private float currentY = 0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        currentX = angles.y;
        currentY = angles.x;

        // Сделать курсор невидимым и захватить его
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target)
        {
            // Получаем ввод мыши
            currentX += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
            currentY -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

            // Ограничиваем вертикальный угол
            currentY = Mathf.Clamp(currentY, yMinLimit, yMaxLimit);

            // Создаем вращение
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

            // Обновляем позицию камеры
            Vector3 position = rotation * new Vector3(0, 0, -distance) + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }
}