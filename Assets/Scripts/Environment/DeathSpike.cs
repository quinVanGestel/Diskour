using UnityEngine;

public class DeathSpike : ContactDeath
{
    private void OnCollisionEnter(Collision collision)
    {
        ISpikeAble mortal = collision.gameObject.GetComponent<ISpikeAble>();
        if (mortal == null)
        {
            return;
        }

        mortal.Kill();
    }
}
