using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public int health;
    public int damage;
    public float cooldown;
    private float timer = 0f;

    public Creature[] creatures;

    public void Damage(Creature attacker, Creature defender)
    {
        //Debug.Log(timer);
        if(timer > attacker.cooldown)
        {
            defender.health -= attacker.damage;
            timer = 0f;
            Debug.Log("hit: " + defender.health);
        }
        timer += (Time.deltaTime*10); 
    }

    public Creature[] GetAllCreatures()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Dummy");
        for (int i = 0; i < objects.Length; i++){
            creatures[i] = objects[i].GetComponent<Creature>();
        }
        return creatures;
    }

    private void Update()
    {
        if(health == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
