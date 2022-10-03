using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundState : MonoBehaviour
{
    public void PoisonGround(GameObject poisongroundPrefab, Transform transformPoison)
    {
        Instantiate(poisongroundPrefab, new Vector3(transformPoison.position.x, this.transform.position.y, this.transform.position.z), this.transform.rotation);
    }
}
