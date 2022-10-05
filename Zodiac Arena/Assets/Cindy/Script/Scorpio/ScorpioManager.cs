using UnityEngine;

public class ScorpioManager : MonoBehaviour
{
    [Header("Atributos do Veneno Lançado pela Scorpio")]
    public Transform poisonPoint;
    public ScorpioPoison scorpioPoison;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            scorpioPoison.Shoot(poisonPoint,scorpioPoison.gameObject);
        }
        
    }
}
