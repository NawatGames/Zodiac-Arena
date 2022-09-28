using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capricorn : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject beam;
    private Transform beamSpawn;

    // Start is called before the first frame update
    void Start()
    {
        beamSpawn = transform.GetChild(0);
        ShootLaserBeam();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
            ShootLaserBeam();
    }

    void ShootLaserBeam()
    {
        beamSpawn.right = (player.position - beamSpawn.position).normalized;
        Instantiate(beam, beamSpawn.position, beamSpawn.rotation);
    }
}
