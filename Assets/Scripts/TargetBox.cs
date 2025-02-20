using UnityEngine;

public class TargetBox : Target // INHERITANCE
{
    protected AudioSource targetAudio;
    [SerializeField] protected AudioClip pointSound;

    protected override void Start() //POLYMORPHISM (override)
    {
        base.Start(); // Mantem a inicialização da classe base
        PointValue = 20; // Sobrescreve o valor da classe base
        targetAudio = GetComponent<AudioSource>(); //Extende a classe base
    }

    protected override void OnCollisionEnter(Collision collision) //POLYMORPHISM (override)
    {
        base.OnCollisionEnter(collision);
        targetAudio.PlayOneShot(pointSound, 2.0f);
    }

}

