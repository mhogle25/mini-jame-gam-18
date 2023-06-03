using UnityEngine;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerController playerPrefab;
    [SerializeField] private Vector2 playerStartingPosition;
    [Header("Time")]
    [SerializeField] private float precombatDuration = 100f;
    [SerializeField] private float combatDuration = 1000f;
    [Header("Static Utilities")]
    [SerializeField] private PaintSplatterer splatterer = null;

    private GameInfo gameInfo = new();
    private Action state = null;

    public PlayerController Player => this.gameInfo.Player;
    public float TimeRemaining => this.gameInfo.TimeRemaining;
    public PaintSplatterer Splatterer => this.splatterer;

    public static GameManager Instance => GameManager.instance;
    private static GameManager instance = null;

    private readonly Dictionary<PaintColor, ColorInfo> colorInfo = new()
    {
        { PaintColor.Red,   new ColorInfo { Hue = Color.red } },
        { PaintColor.Green, new ColorInfo { Hue = Color.green } },
        { PaintColor.Blue,  new ColorInfo { Hue = Color.blue } },
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
        if (this.Player != null)
            Destroy(this.Player);

        this.gameInfo = new GameInfo
        {
            Player = InstantiatePlayer(this.playerStartingPosition),
            TimeRemaining = this.precombatDuration
        };
        this.state = StatePrecombat;
    }

    private void SetStateCombat()
    {
        this.gameInfo.TimeRemaining = this.combatDuration;
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

    private void Countdown() => this.gameInfo.TimeRemaining -= Time.deltaTime;
    #endregion
}
