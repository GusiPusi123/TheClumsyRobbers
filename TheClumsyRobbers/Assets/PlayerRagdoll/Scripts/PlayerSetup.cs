using UnityEngine;

public class RagdollWalk : MonoBehaviour
{
    public Rigidbody[] limbs; // Все Rigidbody игрока
    public float walkForce = 200f; // сила для движения
    public float turnForce = 100f; // сила для поворота

    void Start()
    {
        // В начале — активен реггдолл, выключена анимация
        EnableRagdoll();
    }

    void Update()
    {
        // Управление движением
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(h, 0, v);

        if (moveDirection.magnitude > 0.1f)
        {
            // Применяем силу к центральной части тела (например, к туловищу)
            Rigidbody torso = limbs[0]; // предполагается, что первая — туловище
            torso.AddForce(moveDirection.normalized * walkForce);
        }
    }

    public void EnableRagdoll()
    {
        foreach (Rigidbody rb in limbs)
        {
            rb.isKinematic = false; // включить физику
        }
    }

    public void DisableRagdoll()
    {
        foreach (Rigidbody rb in limbs)
        {
            rb.isKinematic = true; // выключить физику
        }
    }
}