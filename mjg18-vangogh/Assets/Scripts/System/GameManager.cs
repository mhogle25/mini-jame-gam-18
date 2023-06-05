using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerController playerPrefab;
    [SerializeField] private Vector2 playerStartingPosition;
    [Header("Enemy")]
    [SerializeField] private int maxEnemies = 100;
    [Header("Time")]
    [SerializeField] private float precombatDuration = 10f;
    [SerializeField] private float combatDuration = 100f;
    [Header("Static Utilities")]
    [SerializeField] private PaintSplatterer splatterer = null;
    [Header("UI")]
    [SerializeField] private Image colorIcon = null;
    [SerializeField] private Sprite magentaBucketIcon = null;
    [SerializeField] private Sprite cyanBucketIcon = null;
    [SerializeField] private Sprite yellowBucketIcon = null;
    [SerializeField] private TextMeshProUGUI precombatCountdown = null;
    [SerializeField] private TextMeshProUGUI combatCountdown = null;
    [SerializeField] private TextMeshProUGUI postcombatMessage = null;

    private Action state = null;
    private PlayerController player = null;
    private float timeRemaining = 0f;
    private readonly List<EntityController> enemyBank = new();

    public PlayerController Player => this.player;
    public float TimeRemaining => this.timeRemaining;
    public PaintSplatterer Splatterer => this.splatterer;
    public bool InCombat => this.state == StateCombat;
    public bool ExceededEnemyLimits => this.enemyBank.Count > this.maxEnemies;

    public static GameManager Instance => GameManager.instance;
    private static GameManager instance = null;

    private readonly Dictionary<PaintColor, ColorInfo> colorInfo = new()
    {
        { PaintColor.Magenta, new ColorInfo { Hue = Color.magenta/*new Color32(222, 146, 222, 255)*/, Damage = 3 } },
        { PaintColor.Yellow,  new ColorInfo { Hue = Color.yellow/*new Color32(212, 205, 110, 255)*/,  Damage = 2, PlayerSpeed = 2f } },
        { PaintColor.Cyan,    new ColorInfo { Hue = Color.cyan/*new Color32(128, 209, 209, 255)*/,    Damage = 2, SplatCount = 10, SplatRadius = 1.5f } }
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
        if (!this.colorInfo.ContainsKey(paintColor))
            return null;

        return this.colorInfo[paintColor];
    }

    public void UpEnemyCount(EntityController enemy)
    {
        this.enemyBank.Add(enemy);
    }

    public void DownEnemyCount(EntityController enemy)
    {
        this.enemyBank.Remove(enemy);
    }

    public void SetPlayerColor(PaintColor color)
    {
        this.Player.SetColor(color);
        SetColorIcon(color);
    }

    public void PlayAgain()
    {
        this.postcombatMessage.gameObject.SetActive(false);
        this.splatterer.ResetSplatters();
        SetStatePrecombat();
    }

    public void Exit()
    {
        SceneManager.LoadScene("Title Screen");
    }

    #region States
    private void StatePrecombat()
    {
        if (this.TimeRemaining < 0)
            SetStateCombat();
        Countdown();
        this.precombatCountdown.text = $"{GetRemainingSeconds()}";
    }

    private void StateCombat()
    {
        if (this.TimeRemaining < 0)
            SetStatePostcombat();
        Countdown();

        string extraZero = string.Empty;
        if (GetRemainingSeconds() < 10)
        {
            extraZero = "0";
        }
        this.combatCountdown.text = $"{GetRemainingMinutes()}:{extraZero}{GetRemainingSeconds()}";
    }

    private void SetStatePrecombat()
    {
        Debug.Log("State Precombat");
        if (this.Player != null)
            Destroy(this.Player.gameObject);

        this.precombatCountdown.enabled = true;
        this.player = InstantiatePlayer(this.playerStartingPosition);
        this.timeRemaining = this.precombatDuration;

        this.state = StatePrecombat;
    }

    private void SetStateCombat()
    {
        Debug.Log("State Combat");
        this.timeRemaining = this.combatDuration;
        this.precombatCountdown.enabled = false;
        this.combatCountdown.enabled = true;
        this.Player.SetStateMove();
        this.state = StateCombat;
    }

    private void SetStatePostcombat()
    {
        this.state = null;
        this.combatCountdown.enabled = false;
        this.postcombatMessage.gameObject.SetActive(true);
        this.postcombatMessage.text = $"Finished!\n<color=#{ColorUtility.ToHtmlStringRGBA(GetColorInfo(PaintColor.Magenta).Hue)}>{this.splatterer.MagentaScore()}</color>, <color=#{ColorUtility.ToHtmlStringRGBA(GetColorInfo(PaintColor.Yellow).Hue)}>{this.splatterer.YellowScore()}</color>, <color=#{ColorUtility.ToHtmlStringRGBA(GetColorInfo(PaintColor.Cyan).Hue)}>{this.splatterer.CyanScore()}</color>";

        foreach (EntityController enemy in this.enemyBank)
        {
            Destroy(enemy.gameObject);
        }

        this.enemyBank.Clear();
    }

    private void SetColorIcon(PaintColor color)
    {
        this.colorIcon.sprite = GetBucketIcon(color);
    }

    private Sprite GetBucketIcon(PaintColor color)
    {
        return color switch
        {
            PaintColor.Magenta => this.magentaBucketIcon,
            PaintColor.Cyan => this.cyanBucketIcon,
            PaintColor.Yellow => this.yellowBucketIcon,
            _ => throw new Exception("huh?")
        };
    }

    private int GetRemainingMinutes()
    {
        return Mathf.FloorToInt(this.timeRemaining / 60);
    }

    private int GetRemainingSeconds()
    {
        return Mathf.FloorToInt(timeRemaining % 60);
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