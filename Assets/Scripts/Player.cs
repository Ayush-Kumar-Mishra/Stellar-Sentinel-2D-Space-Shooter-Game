using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8f;   //in this _ can used to find private values
    private float _speedMultiplier = 2;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float fireRate = 0.15f;
    private float _canFire = 1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _shieldVisualizer;
    private int _score;
    private UIManager _uiManager;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _lefttEngine;
    [SerializeField]
    private AudioClip _laserAudio;
    private AudioSource _audioSource;
    

    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;


    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }

        if(_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL.");
        }

        if(_audioSource == null)
        {
            Debug.LogError("Audio Source is NULL.");
        }
        else
        {
            _audioSource.clip = _laserAudio;
        }
    }


    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

    }

    void CalculateMovement()
    {
        //"movement Vertical and Horizontal"
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        /*transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);*/

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        
        float posX = transform.position.x;
        float posY = transform.position.y;

        transform.position = new Vector3(Mathf.Clamp(posX, -8, 8),Mathf.Clamp(posY, -3, -3), 0);
    }

    void FireLaser()
    {
        _canFire = Time.time + fireRate;

        if (_isTripleShotActive == true)
        {
            _canFire += 0.01f;
            Instantiate(_tripleShotPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
        else
        {        
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }

        _audioSource.Play();
    }

    public void Damage()
    {
        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }

        _lives--;

        if (_lives == 2)
        {
            _rightEngine.SetActive(true);
        }
        else if(_lives == 1)
        {
            _lefttEngine.SetActive(true);
        }

        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
        
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(tripleShotPowerDownRoutine());
    }

    IEnumerator tripleShotPowerDownRoutine()
    {
        while (_isTripleShotActive == true)
        {
            yield return new WaitForSeconds(5.0f);
            _isTripleShotActive = false;
        }
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(speedBoostPowerDownRoutine());
    }

    IEnumerator speedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
        StartCoroutine(shieldActivePowerDownRoutine());
    }

    IEnumerator shieldActivePowerDownRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        _shieldVisualizer.SetActive(false);
        _isShieldActive = false;
    }

    public void AddScore(int points = 10)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

}
