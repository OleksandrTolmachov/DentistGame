using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegistrationForm : MonoBehaviour
{
    [SerializeField] private TMP_InputField _username;
    [SerializeField] private TMP_InputField _password;
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _ratings;
    private GameClient _gameClient;

    public ServerOptions options;

    private async void Start()
    {
        _gameClient = new GameClient(options);
        if (await _gameClient.IsConnectionAvailable() && !PlayerPrefs.HasKey("token"))
        {
            _panel.SetActive(true);
            _ratings.interactable = true;
        }
    }


    public async void TryRegister()
    {
        if (string.IsNullOrWhiteSpace(_username.text) || string.IsNullOrWhiteSpace(_password.text))
            return;

        await _gameClient.Register(_username.text, _password.text);
        _panel.SetActive(false);
    }
}
