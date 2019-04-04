using UnityEngine;

public class FontsController : MonoBehaviour
{
    private Transform fontTransform;
    private Vector3 originalPosition;

    private void Start()
    {
        fontTransform = transform;
        originalPosition = fontTransform.position;
    }

    private void FixedUpdate()
    {
        fontTransform.position = originalPosition + new Vector3(Random.Range(-0.03f, 0.03f), Random.Range(-0.03f, 0.03f), 0.0f);
    }
}