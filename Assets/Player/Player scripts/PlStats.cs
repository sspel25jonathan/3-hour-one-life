using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlStats : MonoBehaviour
{

    [Header("Player Info")]
    PlMovement PlMovement;

    [Header("Player Stats")]
    int health = 30;
    int hunger;
    int thirst;
    int maxhealth = 30;
    int maxhunger = 30;
    int maxthirst = 30;
    int age = 0;
    public bool rePopulate = false;

    [Header("Timers")]
    float yearTimer;
    float hungerTimer ;
    float thirstTimer;


    void Awake()
    {
        PlMovement = GetComponent<PlMovement>();
        StartCoroutine(Ageing());
        

        int gender = Random.Range(0, 2);

    }

    void Update()
    {
        if (health <= 0)
        {
            PlayerDeath();
        }


        if (age <= 10)
        {
            maxhealth = 30;
            maxhunger = 30;
            maxthirst = 30;
        }
        else if (age > 10 && age <= 20)
        {
            maxhealth = 60;
            maxhunger = 60;
            maxthirst = 60;
        }
        else if (age > 20 && age <= 50)
        {
            maxhealth = 100;
            maxhunger = 100;
            maxthirst = 100;
            rePopulate = true;
        } else if (age > 60)
        {
            maxhunger = 70;
            maxthirst = 70;
            maxhealth = 70;
            rePopulate = false;
        }

        

    }
    public IEnumerator Ageing()
    {
        while (true)
        {
            if (Time.time >= yearTimer)
        {
            age += 1;
            yearTimer = Time.time + 240;
        }
        yield return null;
        }
        
    }

    public IEnumerator HungerAndthirst()
    {
        while (true)
        {

            if (hunger > 0 && Time.time >= hungerTimer)
            {
                hunger -= 1;
                hungerTimer = 180 + Random.Range(0, 60) + Time.time;
            }
            if (thirst > 0 && Time.time >= thirstTimer)
            {
                thirst -= 1;
                thirstTimer = 180 + Random.Range(0, 60) + Time.time;
            }
            yield return null;

        }

    }




    void PlayerDeath()
    {

        GetComponent<PlMovement>().enabled = false;

    }
}
