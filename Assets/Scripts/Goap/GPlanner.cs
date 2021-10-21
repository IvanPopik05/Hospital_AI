using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Node 
{
    public Node parent; // Родительский нод 
    public float cost; // Цена действия
    public Dictionary<string, int> state; // Состояния
    public GAction action; // Действие

    public Node(Node parent, float cost, Dictionary<string, int> allStates, GAction action)
    {
        this.parent = parent;
        this.cost = cost;
        state = new Dictionary<string, int>(allStates);
        this.action = action;
    }
    public Node(Node parent, float cost, Dictionary<string, int> allStates, Dictionary<string, int> allBeliefs, GAction action)
    {
        this.parent = parent;
        this.cost = cost;
        state = new Dictionary<string, int>(allStates);
        foreach (var b in allBeliefs)
        {
            if (!state.ContainsKey(b.Key)) 
            {
                state.Add(b.Key,b.Value);
            }
        }
        this.action = action;
    }
}
public class GPlanner
{
    public Queue<GAction> Plan(List<GAction> actions, Dictionary<string, int> goal, WorldStates beliefStates) 
    {
        List<GAction> usableActions = new List<GAction>(); // Действия, которые можно использовать
        foreach (var a in actions)
        {
            if (a.IsAchievable()) 
            {
                usableActions.Add(a);
            }
        }
        List<Node> leaves = new List<Node>(); // Список нодов
        Node start = new Node(null,0,GWorld.Instance.GetWorld().GetStates(),beliefStates.GetStates(),null); // Самый первый нод
        bool success = BuildGraph(start, leaves, usableActions, goal); // План построен, если true

        if (!success) 
        {
            Debug.Log("Нет плана");
            return null;
        }

        Node cheapest = null; // Самый дешёвый нод

        foreach (var leaf in leaves)
        {
            if (cheapest == null)
            {
                cheapest = leaf;
            }
            else 
            {
                if (leaf.cost < cheapest.cost) 
                {
                    cheapest = leaf;
                }
            }
        }

        List<GAction> result = new List<GAction>();
        Node n = cheapest;
        while (n != null)
        {
            if (n.action != null) 
            {
                result.Insert(0,n.action);
            }
            n = n.parent;
        }

        Queue<GAction> queue = new Queue<GAction>();
        foreach (var a in result)
        {
            queue.Enqueue(a); // Добавляет элемент в последнюю очередь
        }

        Debug.Log("План готов");

        foreach (var a in queue)
        {
            Debug.Log($"Q: {a.actionName}");
        }
        return queue;
    }

    public bool BuildGraph(Node parent, List<Node> leaves, List<GAction> usableActions, Dictionary<string, int> goal) 
    {
        bool foundPath = false; // Путь пока не найден
        foreach (var action in usableActions)
        {
            if(action.IsAchievableGiven(parent.state)) // Если действие достижимо, учитывая переданные родительские состояния
            {
                Dictionary<string, int> currentState = new Dictionary<string, int>(parent.state);
                foreach (var eff in action.effectsDictionary)
                {
                    if (!currentState.ContainsKey(eff.Key)) 
                    {
                        currentState.Add(eff.Key,eff.Value);
                    }
                }
                Node node = new Node(parent,parent.cost + action.cost,currentState,action);
                if (GoalAchieved(goal, currentState))  // Если цель достигнута, то процесс завершён
                {
                    leaves.Add(node);
                    foundPath = true;
                }
                else 
                {
                    List<GAction> subSet = ActionSubset(usableActions, action); // Убирает действие с используемых действий. Чтобы не было зациклено
                    bool found = BuildGraph(node,leaves,subSet,goal);
                    if (found)
                        foundPath = true;
                }
            }
        }
        return foundPath;
    }
    private bool GoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> state) 
    {
        foreach (var g in goal)
        {
            if (!state.ContainsKey(g.Key))
                return false;
        }
        return true;
    }
    private List<GAction> ActionSubset(List<GAction> actions,GAction removeMe) 
    {
        List<GAction> subset = new List<GAction>();
        foreach (var action in actions)
        {
            if (!action.Equals(removeMe))
            {
                subset.Add(action);
            }
        }
        return subset;
    }
}
