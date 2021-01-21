using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<Player> m_allPlayers;
    public List<Player> PlayerInstances = new List<Player>();
    public List<Material> playerColors;
    public Player m_activePlayer;
    private MapGeneration m_mapGeneration;

    private UIManager m_uiManager;
    public Player ActivePlayer
    {
        get { return m_activePlayer; }
        private set
        {
            m_activePlayer = value;
            RoundChanged?.Invoke(m_activePlayer);
        }
    }

    private void Start()
    {
        m_uiManager = Injector.GetInstance<UIManager>() ?? throw new MissingComponentOnStartException(nameof(m_uiManager));
        m_mapGeneration = FindObjectOfType<MapGeneration>() ?? throw new MissingComponentOnStartException(nameof(m_mapGeneration));
        m_allPlayers.ForEach(p =>
        {
            var aux = Instantiate(p.gameObject);
            var ps = aux.GetComponent<Player>();            
            PlayerInstances.Add(ps);
            var idx = PlayerInstances.IndexOf(ps);
            ps.SetMaterial(playerColors[idx]);
            ps.SetName($"Player {idx}");
        });
        ActivePlayer = PlayerInstances[Random.Range(0, m_allPlayers.Count)];
        EventSubscription();
    }

    private void OnDestroy()
    {
        EventUnsubscription();
    }

    public void EventSubscription()
    {
        InteractibleGameObject.RaiseOnClick += EntityClicked;
    }

    public void EventUnsubscription()
    {
        InteractibleGameObject.RaiseOnClick -= EntityClicked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_mapGeneration.SpawnPlayers(PlayerInstances.Select(p => p.gameObject).ToList());
        }
    }

    void EndRound()
    {
        RoundUp();
    }

    void RoundUp()
    {
        ActivePlayer = PlayerInstances[(m_allPlayers.IndexOf(m_activePlayer) + 1 % m_allPlayers.Count)];
    }

    public delegate void OnRoundChanged(Player plr);

    public static event OnRoundChanged RoundChanged;

    private void EntityClicked(object sender, OnClickEventArgs obj)
    {
        if (ActivePlayer.Units.Contains(sender)) {
            m_uiManager.ShowCommandPanel(sender as Unit, ActivePlayer.m_actions);
        } else m_uiManager.ShowInfoPanel(sender as Unit);
    }

    private void Awake()
    {
        Injector.RegisterContainer<GameController>(this);
    }
}
