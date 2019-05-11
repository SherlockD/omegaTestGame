using Scripts.GameScreen.Player;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Scripts.GameScreen
{
    public class CoinsSpawner : MonoBehaviour
    {
        [SerializeField] private Collider _area;
        [SerializeField] private GameObject _coinObject;
        [SerializeField] private AIPlayer _aiPlayer;
        [SerializeField] private int _spawnTime;
        [SerializeField] private int _coinDestroyTime;

        [Header("UI")] [SerializeField] private Text _coinNumText;

        [Header("Coordinates")] [SerializeField]
        private int _spawnY;

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

            _minX = _area.bounds.min.x;
            _minZ = _area.bounds.min.z;

            _maxX = _area.bounds.max.x;
            _maxZ = _area.bounds.max.z;

            _timer.CallOnTimerEnd += SpawnCoin;

            _timer.StartTimer(_spawnTime, Timer.Mode.Сontinued);

            _aiPlayer.CallOnCollideWithCoin += () =>
            {
                _coinsNum++;
                _coinNumText.text = _coinsNum.ToString();
            };
        }

        private void SpawnCoin()
        {
            var spawnPosition = new Vector3(Random.Range(_minX, _maxX), _spawnY, Random.Range(_minZ, _maxZ));
            var instantiate = Instantiate(_coinObject, spawnPosition, Quaternion.identity, transform)
                .GetComponent<Coin>();
            instantiate.Initialize(_coinDestroyTime, _freezingY);
        }
    }
}