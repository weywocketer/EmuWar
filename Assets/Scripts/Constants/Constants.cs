using System.Collections.Generic;
using UnityEngine;

namespace FubarOps.Constants
{
    public static class Formations
    {
        public static readonly List<Vector2> line = new()
        {
            new Vector2(0,  0),
            new Vector2(0,  1),
            new Vector2(0, -1),
            new Vector2(0,  2),
            new Vector2(0, -2),
            new Vector2(0,  3),
            new Vector2(0, -3),
            new Vector2(0,  4),
            new Vector2(0, -4)
        };

        public static readonly List<Vector2> wedge = new()
        {
            new Vector2( 0,  0),
            new Vector2(-1,  1),
            new Vector2(-1, -1),
            new Vector2(-2,  2),
            new Vector2(-2, -2),
            new Vector2(-3,  3),
            new Vector2(-3, -3),
            new Vector2(-4,  4),
            new Vector2(-4, -4)
        };

        public static readonly List<Vector2> column = new()
        {
            new Vector2( 0, 0),
            new Vector2(-1, 0),
            new Vector2(-2, 0),
            new Vector2(-3, 0),
            new Vector2(-4, 0),
            new Vector2(-5, 0),
            new Vector2(-6, 0),
            new Vector2(-7, 0),
            new Vector2(-8, 0)
        };

    }

    public enum Side
    {
        soldiers,
        emus,
        civilian
    }

    public enum InitialState
    {
        emuWander,
        emuFlockToTarget,
        soldierWander,
    }

    public enum PlayerMode
    {
        normal,
        usingBinoculars,
        usingMap
    }

    public static class AttackRangeSquaredValues
    {
        public static float shortRange = 400; // 20
        public static float normalRange = 2500; // 50
        public static float longRange = 4900; // 70
    }

    public static class EmuSpeed
    {
        public static float walking = 10;
        public static float running = 20;
    }


    public static class BehaviourWeights
    {
        public static (float Seek, float Flee, float Arrive, float ObstacleAvoidance, float Wander, float Flocking, float Hide, float OffsetPursuit) emuSeek =
            (Seek: 1, Flee: 0, Arrive: 0, ObstacleAvoidance: 0, Wander: 0, Flocking: 0, Hide: 0, OffsetPursuit: 0);

        public static (float Seek, float Flee, float Arrive, float ObstacleAvoidance, float Wander, float Flocking, float Hide, float OffsetPursuit) emuFlee =
            (Seek: 0, Flee: 1, Arrive: 0, ObstacleAvoidance: 0, Wander: 0, Flocking: 0, Hide: 0, OffsetPursuit: 0);

        public static (float Seek, float Flee, float Arrive, float ObstacleAvoidance, float Wander, float Flocking, float Hide, float OffsetPursuit) emuFlockWander =
            (Seek: 0, Flee: 0, Arrive: 0, ObstacleAvoidance: 3, Wander: 5, Flocking: 0.5f, Hide: 0, OffsetPursuit: 0);

        public static (float Seek, float Flee, float Arrive, float ObstacleAvoidance, float Wander, float Flocking, float Hide, float OffsetPursuit) soldierOffsetPursuit =
            (Seek: 0, Flee: 0, Arrive: 0, ObstacleAvoidance: 0, Wander: 0, Flocking: 0, Hide: 0, OffsetPursuit: 1);
    }
}