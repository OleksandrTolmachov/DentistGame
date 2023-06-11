using System.Linq;
using TMPro;
using UnityEngine;

public class TopStats : MonoBehaviour
{
    [SerializeField] private GameObject _parentContent;
    [SerializeField] private GameObject _stat;
    private GameClient _gameClient;
    private bool _revealed = false;

    public ServerOptions ServerOptions;

    private void Start()
    {
        _gameClient = new GameClient(ServerOptions);
    }

    public async void Reveal()
    {
        if (!_revealed)
        {
            var stats = (await _gameClient.GetAllPlayers()).OrderByDescending(stat => stat.FinishedLevels).Take(6);
            foreach (var player in stats)
            {
                var gameObject = Instantiate(_stat, _parentContent.transform.position, Quaternion.identity, 
                    _parentContent.transform);
                var texts = gameObject.GetComponentsInChildren<TMP_Text>();
                if (PlayerPrefs.GetString("username") == player.Username)
                {
                    texts[0].text = player.Username + "(You)";
                }
                else
                {
                    texts[0].text = player.Username;
                }
                texts[1].text = player.FinishedLevels.ToString();
            }
            _revealed = true;
        }
    }
}
