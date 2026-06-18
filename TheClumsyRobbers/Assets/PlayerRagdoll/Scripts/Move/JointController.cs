using UnityEngine;

public class JointController : MonoBehaviour
{
    public ConfigurableJoint[] joints; // Массив суставов
    public Rigidbody[] rigidbodies; // Массив rigidbodies

    public float SpringStrength = 5000f;
    public float Damping = 100f;
    public float MaxForce = 600f;

    public bool Ragdoll = false;

    void Start()
    {
        // Инициализация суставов и rigidbodies, если нужно
        if (joints == null || joints.Length == 0)
        {
            joints = GetComponentsInChildren<ConfigurableJoint>();
        }

        if (rigidbodies == null || rigidbodies.Length == 0)
        {
            rigidbodies = GetComponentsInChildren<Rigidbody>();
        }

        // Настройка суставов
        foreach (var joint in joints)
        {
            // Включаем Spring в Linear Limit
            var linearLimit = joint.linearLimit;
            linearLimit.limit = 0.2f; // пример значения, можно подстроить
            joint.linearLimit = linearLimit;

            // Включение Spring через приводы
            JointDrive drive = new JointDrive();
            drive.positionSpring = SpringStrength;
            drive.positionDamper = Damping;
            drive.maximumForce = MaxForce;

            joint.xDrive = drive;
            joint.yDrive = drive;
            joint.zDrive = drive;
        }

        // Настройка Ragdoll
        SetRagdoll(Ragdoll);
    }

    public void SetRagdoll(bool state)
    {
        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = !state;
        }
    }

    void Update()
    {
        // Можно добавлять сюда управление или динамическое изменение параметров
    }
}