using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]

public class GameConstants : ScriptableObject
{
    // For Scoring system
    int currentScore;
    int currentPlayerHealth;

    // For Reset values
    Vector3 goombaSpawnPointStart = new Vector3(2.5f, -0.45f, 0); // Hardcoded location
    // Other reset values

    // For EnemyController.cs
    public float maxOffset = 5.0f;
    public float enemyPatrolTime = 2.0f;
    public float groundSurface = -1.0f;

    // For ConsumableMushroom.cs
    public int consumeTimeStep = 10;
    public int consumeLargestScale = 4;
    
    // For BreakBrick.cs
    public int breakTimeStep = 30;
    public int breakDebrisTorque = 10;
    public int breakDebrisForce = 10;
    
    // For Debris.cs
    public int spawnNumberOfDebris = 10;
    
    // For Rotator.cs
    public int rotatorRotateSpeed = 6;
    
    // For testing
    public int testValue;
}
