using UnityEngine;

public class Target : MonoBehaviour
{
    private GameManager gameManager;

    private int pointMult;

    // ENCAPSULATION
    private int _pointValue; 
    public virtual int PointValue 
    {
        get { return _pointValue; }
        set 
        { 
            if(value >= 5)
                _pointValue = value;
            else
               Debug.LogError("You can't set a number smaller than 5 for PointValue!"); 
        }    
    }

    protected virtual void Start()
    {
        _pointValue = 5;
        pointMult = 2;
        gameManager = FindAnyObjectByType<GameManager>().GetComponent<GameManager>();
    }

    protected virtual void OnCollisionEnter(Collision collision) 
    {
        if (gameManager.hasPowerUp)
            gameManager.UpdateScore(PointValue, pointMult);
            
        else
            gameManager.UpdateScore(PointValue);
    }
}
