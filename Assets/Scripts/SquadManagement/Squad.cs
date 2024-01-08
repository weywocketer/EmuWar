using System;
using System.Collections.Generic;
using UnityEngine;

namespace FubarOps.SquadManagement
{
    [Serializable]
    public class Squad
    {
        public string callsign;
        public List<GameObject> soldiers; // soldiers[0] is always the Squad Leader
    }
}