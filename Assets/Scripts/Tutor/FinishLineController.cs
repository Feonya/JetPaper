using UnityEngine;

public class FinishLineController : MonoBehaviour
{
    public GameObject startLine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            startLine.GetComponent<Collider2D>().enabled = true;
            GetComponent<Collider2D>().enabled = false;
        }
    }
}