using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceQueue
{
    private Queue<GameObject> que = new Queue<GameObject>();
    private string tag;
    private string modState;

    public ResourceQueue(string tag, string modState, WorldStates w)
    {
        this.tag = tag;
        this.modState = modState;

        if (tag != "")
        {
            GameObject[] resources = GameObject.FindGameObjectsWithTag(tag);
            foreach (var r in resources)
            {
                que.Enqueue(r);
            }
        }
        if (modState != "")
        {
            w.ModifyState(modState, que.Count);
        }
    }

    public void AddResource(GameObject r)
    {
        que.Enqueue(r);
    }
    public void RemoveResource(GameObject r)
    {
        que = new Queue<GameObject>(que.Where(q => q != r));
    }
    public GameObject RemoveResource() 
    {
        if (que.Count == 0)
            return null;
        return que.Dequeue();
    }
}
public sealed class GWorld 
{
    private static readonly GWorld instance = new GWorld();
    private static WorldStates world;
    private static ResourceQueue patients;
    private static ResourceQueue cubicles;
    private static ResourceQueue offices;
    private static ResourceQueue toilets;
    private static ResourceQueue puddles;
    private static Dictionary<string, ResourceQueue> resources = new Dictionary<string, ResourceQueue>();
    public static GWorld Instance 
    {
        get { return instance; }
    }
    static GWorld()
    {
        world = new WorldStates();
        patients = new ResourceQueue("","",world);
        resources.Add("patients",patients);
        cubicles = new ResourceQueue("Cubicle", "FreeCubicle", world);
        resources.Add("cubicles", cubicles);
        offices = new ResourceQueue("Office", "FreeOffice", world);
        resources.Add("offices", offices);
        toilets = new ResourceQueue("Toilet", "FreeToilet", world);
        resources.Add("toilets", toilets);
        puddles = new ResourceQueue("Puddle","FreePuddle",world);
        resources.Add("puddles", puddles);
        Time.timeScale = 3;
    }
    public ResourceQueue GetQueue(string q) 
    {
        return resources[q];
    }
    private GWorld(){}

    public WorldStates GetWorld() 
    {
        return world;
    }
}
