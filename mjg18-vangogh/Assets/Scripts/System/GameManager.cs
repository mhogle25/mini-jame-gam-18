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
    [SerializeField] private Splatterer splatterer = null;

    private GameInfo gameInfo = null;
    private Action state = null;

    public PlayerController Player => this.gameInfo?.player;
    public float? TimeRemaining => this.gameInfo?.timeRemaining;

    public static GameManager Instance => GameManager.instance;
    private static GameManager instance = null;

    private Dictionary<PaintColor, ColorInfo> colorInfo = new()
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
        if (this.gameInfo is not null)
            Destroy(this.gameInfo.player);

        this.gameInfo = new GameInfo
        {
            player = InstantiatePlayer(this.playerStartingPosition),
            timeRemaining = this.precombatDuration
        };
        this.state = StatePrecombat;
    }

    private void SetStateCombat()
    {
        this.gameInfo.timeRemaining = this.combatDuration;
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

    private void Countdown() => this.gameInfo.timeRemaining -= Time.deltaTime;
    #endregion
}
