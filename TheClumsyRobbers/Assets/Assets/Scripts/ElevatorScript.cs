using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public Transform[] points; // Массив точек, между которыми перемещается лифт
    public float speed = 2f; // Скорость перемещения

    private int currentTargetIndex = 0; // Индекс текущей точки назначения
    private Transform currentTarget;

    void Start()
    {
        if (points.Length == 0)
        {
            Debug.LogError("Не заданы точки для лифта!");
            enabled = false;
            return;
        }

        currentTarget = points[currentTargetIndex];
    }

    void Update()
    {
        if (points.Length == 0) return;

        // Перемещаемся к текущей точке
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

        // Проверяем, достигли ли точки
        if (Vector3.Distance(transform.position, currentTarget.position) < 0.01f)
        {
            // Переходим к следующей точке
            currentTargetIndex = (currentTargetIndex + 1) % points.Length;
            currentTarget = points[currentTargetIndex];
        }
    }
}