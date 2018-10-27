using UnityEngine;
public class BasicMovement : MonoBehaviour
{
    public float speed = 6.0f;

    Vector3 movement;

    void Awake()
    {

    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Move(h, v);
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);

        transform.position += movement.normalized * speed * Time.deltaTime;
    }

}
