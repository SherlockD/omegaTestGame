using UnityEngine;
using UnityEngine.UI;


public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private Collider _aria;
    [SerializeField] private GameObject _coinObject;
    [SerializeField] private AIPlayer _aiPlayer;
    [SerializeField] private int _spawnTime;
    [SerializeField] private int _coinDestroyTime;

    [Header("UI")] 
    [SerializeField] private Text _coinNumText;
    
    [Header("Coordinates")] 
    [SerializeField] private int _spawnY;
    [SerializeField] private float _freezingY;
    
    private Timer _timer;
    
    private float _minX;
    private float _maxX;
    private float _minZ;
    private float _maxZ;

    private int _coinsNum;
    
    private void Awake()
    {
        _timer = GetComponent<Timer>();
        
        _minX = _aria.bounds.min.x;
        _minZ = _aria.bounds.min.z;

        _maxX = _aria.bounds.max.x;
        _maxZ = _aria.bounds.max.z;

        _timer.CallOnTimerEnd += SpawnCoin;
        
        _timer.StartTimer(_spawnTime,Timer.Mode.Сontinued);

        _aiPlayer.CallOnCollideWithCoin += () =>
        {
            _coinsNum++;
            _coinNumText.text = _coinsNum.ToString();
        };
    }

    private void SpawnCoin()
    {
        var spawnPosition = new Vector3(Random.Range(_minX,_maxX),_spawnY,Random.Range(_minZ,_maxZ));
        var instantiate = Instantiate(_coinObject,spawnPosition,Quaternion.identity,transform).GetComponent<Coin>();
        instantiate.Initialize(_coinDestroyTime,_freezingY);
    }
}
