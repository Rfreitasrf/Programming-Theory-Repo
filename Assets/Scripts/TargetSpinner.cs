using System.Collections;
using TMPro;
using UnityEngine;

public class TargetSpinner : Target // INHERITANCE
{
    [SerializeField] protected AudioClip pointSound;
    [SerializeField] protected TextMeshProUGUI encouragingText; 
    protected AudioSource targetAudio;
    private Color originalColor;


    protected override void Start() //POLYMORPHISM (override)
    {
        base.Start(); // Mantem a inicialização da classe base
        PointValue = 10; // Sobrescreve o valor da classe base
        targetAudio = GetComponent<AudioSource>();
        originalColor = encouragingText.color;
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
        encouragingText.text = messages[messageIndex];

        StopAllCoroutines();
        StartCoroutine(BlinkEffect());
      
    }

    private IEnumerator BlinkEffect()
    {
        
        encouragingText.color = Color.yellow;

        for (int i = 0; i < 3; i++)
        {
            encouragingText.enabled = false;
            yield return new WaitForSeconds(0.1f);
            encouragingText.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        encouragingText.color = originalColor;
    }

}



