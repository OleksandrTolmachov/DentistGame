using System.Linq;
using TMPro;
using UnityEngine;

public class TopStats : MonoBehaviour
{
    [SerializeField] private GameObject _parentContent;
    [SerializeField] private GameObject _stat;
    private GameClient _gameClient;
    private bool _revealed = false;
    private const int UsernameMaxLength = 9;

    public ServerOptions ServerOptions;

    private void Start()
    {
        _gameClient = new GameClient(ServerOptions);
    }

    public async void Reveal()
    {
        if (!_revealed)
        {
            var stats = (await _gameClient.GetAllPlayers()).OrderByDescending(stat => stat.FinishedLevels);
            foreach (var player in stats)
            {
                var gameObject = Instantiate(_stat, _parentContent.transform.position, Quaternion.identity, 
                    _parentContent.transform);
                var texts = gameObject.GetComponentsInChildren<TMP_Text>();

                var username = player.Username;
                if(username.Length > UsernameMaxLength)
                {
                    username = username.Substring(0, UsernameMaxLength) + "...";
                }
                if (PlayerPrefs.GetString("username") == player.Username)
                {
                    texts[0].text = "(You)" + (username.Length > 6 ? username.Substring(0, 6) + "..." : username);
                }
                else
                {
                    texts[0].text = username;
                }
                texts[1].text = player.FinishedLevels.ToString();
            }
            _revealed = true;
        }
    }
}
