using TMPro;
using UnityEngine;

public class TargetSpinner : Target // INHERITANCE
{
    protected AudioSource targetAudio;
    [SerializeField] protected AudioClip pointSound;
    [SerializeField] protected TextMeshProUGUI EncouragingText;


    protected override void Start() //POLYMORPHISM (override)
    {
        base.Start(); // Mantem a inicialização da classe base
        PointValue = 10; // Sobrescreve o valor da classe base
        targetAudio = GetComponent<AudioSource>();
    }

    protected override void OnCollisionEnter(Collision collision) //POLYMORPHISM (override)
    {
        base.OnCollisionEnter(collision);
        targetAudio.PlayOneShot(pointSound, 2.0f);
        EncouragingMessages();
    }

    //Selecionar aleatoriamente uma menssagem de incentivo ao player
    public void EncouragingMessages()
    {
        string[] messages = { "Well Done!", "Nice Shot!", "Awesome!" };
        int messageIndex = Random.Range(0, 3);
        EncouragingText.text = messages[messageIndex];
    }

}



