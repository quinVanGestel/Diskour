using Unity.VisualScripting;
using UnityEngine;

public abstract class ContactDeath : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        ICanDie mortality = collision.gameObject.GetComponent<ICanDie>();
        if (mortality == null)
        {
            return;
        }

        mortality.Kill();
    }
}
