using System.Collections.Generic;
using UnityEngine;

namespace FubarOps.SquadManagement
{
    public class SquadManager : MonoBehaviour
    {
        public static SquadManager Instance { get; private set; }
        public List<Squad> squads;


        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("SquadManager already exists!");
            }
        }

        //void Start()
        //{
        //    foreach (Squad squad in squads)
        //    {
        //        foreach (GameObject soldier in squad.soldiers)
        //        {

        //        }
        //    }
        //}
    }
}
