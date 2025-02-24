using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField] UnityEvent onTriggerEnter2D;
    [SerializeField] UnityEvent onTriggerExit2D;
    [SerializeField] bool destroyOnTriggerEnter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        onTriggerEnter2D.Invoke();
        if (destroyOnTriggerEnter)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        onTriggerExit2D.Invoke();

    }

}
