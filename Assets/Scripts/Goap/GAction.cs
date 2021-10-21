using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public abstract class GAction : MonoBehaviour
{
    public string actionName = "Action";
    public float cost = 1f; // Цена действия. Объект будет выполнять самое дешёвое
    public GameObject target; // Цель места
    public string targetTag; // Игровой объект
    public float duration = 0; // Продолжительность нахождения в определённом месте
    public WorldState[] preConditions; // Предварительные условия
    public WorldState[] afterEffects; // эффекты  
    public NavMeshAgent agent;

    public Dictionary<string, int> preConditionsDictionary;
    public Dictionary<string, int> effectsDictionary;

    public WorldStates agentBeliefs; // Убеждения агента (Внутреннний набор состояний агента) 
    public GInventory inventory;
    public WorldStates beliefs;

    public bool running = false; // Выполняет другое действие

    public GAction()
    {
        preConditionsDictionary = new Dictionary<string, int>();
        effectsDictionary = new Dictionary<string, int>();
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        if (preConditions != null) 
        {
            foreach (var w in preConditions)
            {
                preConditionsDictionary.Add(w.key,w.value);
            }
        }
        if (afterEffects != null) 
        {
            foreach (var w in afterEffects)
            {
                effectsDictionary.Add(w.key,w.value);
            }
        }

        inventory = GetComponent<GAgent>().inventory;
        beliefs = GetComponent<GAgent>().beliefs;
    }

    // Может ли данная цель быть достигнута
    public bool IsAchievable() 
    {
        return true;
    }
    public bool IsAchievableGiven(Dictionary<string, int> conditions) 
    {
        foreach (var p in preConditionsDictionary)
        {
            if (!conditions.ContainsKey(p.Key)) 
            {
                return false;
            }
        }
        return true;
    }

    public abstract bool PrePreform(); // Предварительное выполнение
    public abstract bool PostPreform(); // Последующее выполнение
}
