using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Particle Hit");
    }
}
