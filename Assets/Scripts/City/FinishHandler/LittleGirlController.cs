using UnityEngine;

public class LittleGirlController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plane"))
        {
            GetComponentInParent<FinishHandler>().littleGirlGetLetter = true;
        }
    }
}