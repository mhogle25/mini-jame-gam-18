using UnityEngine;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerController playerPrefab;
    [SerializeField] private Vector2 playerStartingPosition;
    [Header("Time")]
    [SerializeField] private float precombatDuration = 10f;
    [SerializeField] private float combatDuration = 100f;
    [Header("Static Utilities")]
    [SerializeField] private PaintSplatterer splatterer = null;

    private Action state = null;
    private PlayerController player = null;
    private float timeRemaining = 0f;

    public PlayerController Player => this.player;
    public float TimeRemaining => this.timeRemaining;
    public PaintSplatterer Splatterer => this.splatterer;

    public static GameManager Instance => GameManager.instance;
    private static GameManager instance = null;

    private readonly Dictionary<PaintColor, ColorInfo> colorInfo = new()
    {
        { PaintColor.Red,   new ColorInfo { Hue = Color.red, Damage = 5 } },
        { PaintColor.Green, new ColorInfo { Hue = Color.green, HurtRadius = 1.5f } },
        { PaintColor.Blue,  new ColorInfo { Hue = Color.blue, Stuns = true } },
    };

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        GameManager.instance = this;
    }

    private void Start()
    {
        SetStatePrecombat();
    }

    private void Update()
    {
        this.state?.Invoke();
    }

    public ColorInfo GetColorInfo(PaintColor paintColor)
    {
        if (this.colorInfo.ContainsKey(paintColor))
            return null;

        return this.colorInfo[paintColor];
    }

    #region States
    private void StatePrecombat()
    {
        if (this.TimeRemaining < 0)
            SetStateCombat();
        Countdown();
    }

    private void StateCombat()
    {
        if (this.TimeRemaining < 0)
            SetStatePostcombat();
        Countdown();
    }

    private void StatePostcombat()
    {

    }

    private void SetStatePrecombat()
    {
        Debug.Log("State Precombat");
        if (this.Player != null)
            Destroy(this.Player);

        this.player = InstantiatePlayer(this.playerStartingPosition);
        this.timeRemaining = this.precombatDuration;
        this.state = StatePrecombat;
    }

    private void SetStateCombat()
    {
        Debug.Log("State Combat");
        this.timeRemaining = this.combatDuration;
        this.state = StatePrecombat;
    }

    private void SetStatePostcombat()
    {

    }
    #endregion

    #region Game Info
    private PlayerController InstantiatePlayer(Vector2 startingPosition)
    {
        PlayerController player = Instantiate(this.playerPrefab);
        player.transform.position = startingPosition;
        return player;
    }

    private void Countdown() => this.timeRemaining -= Time.deltaTime;
    #endregion
}