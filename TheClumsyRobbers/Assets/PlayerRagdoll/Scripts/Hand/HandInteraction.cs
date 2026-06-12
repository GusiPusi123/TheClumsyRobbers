// using UnityEngine;

// public class HandInteraction : MonoBehaviour
// {
//     public Transform handTransform; // Трансформ руки персонажа
//     public float grabRange = 2f; // Дистанция для захвата
//     public LayerMask grabbableLayer; // Слой предметов, которые можно брать
//     public KeyCode grabKey = KeyCode.E; // Клавиша для захвата/отпускания

//     private Rigidbody heldObject;
//     private FixedJoint joint;

//     void Update()
//     {
//         if (Input.GetKeyDown(grabKey))
//         {
//             if (heldObject == null)
//             {
//                 TryGrab();
//             }
//             else
//             {
//                 Release();
//             }
//         }
//     }

//     void TryGrab()
//     {
//         Collider[] hitColliders = Physics.OverlapSphere(handTransform.position, grabRange, grabbableLayer);
//         if (hitColliders.Length > 0)
//         {
//             Rigidbody targetRb = hitColliders[0].attachedRigidbody;
//             if (targetRb != null)
//             {
//                 heldObject = targetRb;
//                 joint = handTransform.gameObject.AddComponent<FixedJoint>();
//                 joint.connectedBody = heldObject;
//                 joint.breakForce = 2000;
//             }
//         }
//     }

//     void Release()
//     {
//         if (joint != null)
//         {
//             Destroy(joint);
//             joint = null;
//         }
//         heldObject = null;
//     }
// }


using UnityEngine;

public class HandInteraction : MonoBehaviour
{
    public Transform handTransform; // Сустав руки
    public Transform cameraTransform; // Камера, за которой следим
    public float raiseAngle = 60f; // Угол подъема руки
    public float followSpeed = 5f; // Скорость следования/поднятия

    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private bool isRaising = false;

    void Start()
    {
        if (handTransform == null || cameraTransform == null)
        {
            Debug.LogError("Пожалуйста, привяжите рукав и камеру");
            return;
        }
        initialRotation = handTransform.localRotation;
        targetRotation = initialRotation;
    }

    void Update()
    {
        // Если зажата кнопка (например, левый клик мыши или другая)
        if (Input.GetButton("Fire1"))
        {
            isRaising = true;
        }
        else
        {
            isRaising = false;
        }

        // Определяем нужное вращение
        if (isRaising)
        {
            // Ориентируем руку в сторону камеры, поднимая вверх
            Vector3 direction = cameraTransform.forward;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            // Добавляем наклон вверх
            targetRotation = lookRotation * Quaternion.Euler(-raiseAngle, 0, 0);
        }
        else
        {
            targetRotation = initialRotation;
        }

        // Плавное вращение
        handTransform.localRotation = Quaternion.Slerp(handTransform.localRotation, targetRotation, followSpeed * Time.deltaTime);
    }
}