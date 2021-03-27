using Config;
using Player;
using UnityEngine;

public class MainGameController : MonoBehaviour
{
    public GameConfig config;
    void Start()
    {
        var PlayerModel = new PlayerModel(config);
        Debug.Log(PlayerModel.playerHealth);
        Debug.Log(PlayerModel.playerPower);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
