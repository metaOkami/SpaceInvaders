using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObserverPattern : MonoBehaviour
{
    public static int score = 0;
    public class Subject
    {
        List<Observer> observers = new List<Observer>();

        public void Notify()
        {
            for (int i = 0; i < observers.Count; i++)
            {
                observers[i].OnNotify();
            }
        }

        //Add observer to the list
        public void AddObserver(Observer observer)
        {
            observers.Add(observer);
        }

    }
    
    public abstract class Observer
    {
        public abstract void OnNotify();
    }

    public class Score : Observer
    {
       
        GameObject ScoreText;
        ScoreEvents Events;
        public Score(GameObject scoretext, ScoreEvents events)
        {
            this.ScoreText = scoretext;
            this.Events = events;
        }

        public override void OnNotify()
        {
            SetScore(Events.AddPoints());
        }

        
        public void SetScore(int score)
        {
            ScoreText.GetComponent<Text>().text = "Tu puntuacion es: " + score;
        }
    }
    //Events
    public abstract class ScoreEvents
    {
        public abstract int AddPoints();
    }

    public class OneHit : ScoreEvents
    {
        public override int AddPoints()
        {
            return score+=10;
        }
    }


}
