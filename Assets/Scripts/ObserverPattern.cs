using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObserverPattern : MonoBehaviour
{
    public class Subject
    {
        //A list with observers that are waiting for something to happen
        List<Observer> observers = new List<Observer>();

        //Send notifications if something has happened
        public void Notify()
        {
            for (int i = 0; i < observers.Count; i++)
            {
                //Notify all observers even though some may not be interested in what has happened
                //Each observer should check if it is interested in this event
                observers[i].OnNotify();
            }
        }

        //Add observer to the list
        public void AddObserver(Observer observer)
        {
            observers.Add(observer);
        }

        //Remove observer from the list
        public void RemoveObserver(Observer observer)
        {
        }
    }
    //Wants to know when another object does something interesting 
    public abstract class Observer
    {
        public abstract void OnNotify();
    }

    public class Score : Observer
    {
        //The box gameobject which will do something
        Text ScoreText;
        //What will happen when this box gets an event
        ScoreEvents Events;

        public Score(Text scoretext, ScoreEvents events)
        {
            this.ScoreText = scoretext;
            this.Events = events;
        }

        //What the box will do if the event fits it (will always fit but you will probably change that on your own)
        public override void OnNotify()
        {
            SetScore(Events.GetDamage());
        }

        //The box will always jump in this case
        void SetScore(float score)
        {
            ScoreText.text = "Tu puntuacion es: " + score;
        }
    }
    //Events
    public abstract class ScoreEvents
    {
        public abstract float GetDamage();
    }


    public class OneHit : ScoreEvents
    {
        public override float GetDamage()
        {
            return 30f;
        }
    }


}
