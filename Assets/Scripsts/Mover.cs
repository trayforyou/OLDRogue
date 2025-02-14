using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private string _vertical = "Vertical";
    private string _horizontal = "Horizontal";

    private void Update()
    {
        float moveZ = Input.GetAxisRaw(_vertical) * _speed * Time.deltaTime;
        float moveX = Input.GetAxisRaw(_horizontal) * _speed * Time.deltaTime;

        transform.Translate(new Vector3(moveX,0,moveZ));
    }
}