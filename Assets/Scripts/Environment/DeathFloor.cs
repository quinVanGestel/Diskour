using UnityEngine;

public class DeathFloor : ContactDeath
{
    private void OnCollisionEnter(Collision collision)
    {
        IStayInBounds mortal = collision.gameObject.GetComponent<IStayInBounds>();
        if (mortal == null)
        {
            return;
        }

        mortal.Kill();
    }
}
