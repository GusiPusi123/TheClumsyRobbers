using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public float StarafeSpeed;
    public float jumpForce;

    public Rigidbody hips;
    public bool isGrounded;

    void Start()
    {
        hips = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.W))
        {
            hips.AddForce(hips.transform.forward * speed);
        }
    }
}
