using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SubGoal
{
    public Dictionary<string, int> sGoals; // Количество подцелей
    public bool remove; // Удаление цели после выполнения

    public SubGoal(string k, int i, bool r)
    {
        sGoals = new Dictionary<string, int>();
        sGoals.Add(k, i);
        remove = r;
    }
}
public class GAgent : MonoBehaviour
{
    public List<GAction> actions = new List<GAction>(); // Количество действий
    public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
    public GInventory inventory = new GInventory();
    public WorldStates beliefs = new WorldStates();

    GPlanner planner; // Планировщик
    Queue<GAction> actionQueue; // Очередь действий
    public GAction currentAction; // Текущее действие
    public SubGoal currentGoal; // Текущая цель

    Vector3 destination = Vector3.zero;
    public void Start()
    {
        GAction[] acts = GetComponents<GAction>();
        foreach (var a in acts)
        {
            actions.Add(a);
        }
    }

    bool invoked = false;
    void CompleteAction() 
    {
        currentAction.running = false;
        currentAction.PostPreform();
        invoked = false;
    }
    private void LateUpdate()
    {
        if (currentAction != null && currentAction.running) 
        {
            float distanceToTarget = Vector3.Distance(destination,transform.position);
            //Debug.Log($"{currentAction.agent.hasPath}, {distanceToTarget}");
            if (distanceToTarget < 2f) 
            {
                if (!invoked) 
                {
                    Invoke("CompleteAction",currentAction.duration);
                    invoked = true;
                }
            }
            return;
        }
        if (planner == null || actionQueue == null) 
        {
            planner = new GPlanner();

            var sortedGoals = goals.OrderByDescending(x => x.Value);
            //var sort = from entry in goals orderby entry.Value descending select entry;

            foreach (var sg in sortedGoals)
            {
                actionQueue = planner.Plan(actions,sg.Key.sGoals,beliefs);
                if (actionQueue != null) 
                {
                    currentGoal = sg.Key; // Текущая цель поставлена
                    break;
                }
            }
        }

        if (actionQueue != null && actionQueue.Count == 0) // Если очередь действий закончилась
        {
            if (currentGoal.remove) 
            {
                goals.Remove(currentGoal);
            }
            planner = null;
        }

        if (actionQueue != null && actionQueue.Count > 0) 
        {
            currentAction = actionQueue.Dequeue();

            if (currentAction.PrePreform())
            {
                if (currentAction.target == null && currentAction.targetTag != "")
                {
                    currentAction.target = GameObject.FindGameObjectWithTag(currentAction.targetTag);
                }
                if (currentAction.target != null)
                {
                    currentAction.running = true;
                    destination = currentAction.target.transform.position;
                    Transform dest = currentAction.target.transform.Find("Destination");
                    if(dest != null)
                        destination = dest.position;
                    currentAction.agent.SetDestination(destination);
                }
            }
            else 
            {
                actionQueue = null;
            }
        }
    }
}
