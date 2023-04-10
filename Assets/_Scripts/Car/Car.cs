using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{


    private float _deceleration = -400f;
    private float _gravity = 5f;
    private bool _isDead = false;
    private int _direction;
    public float AngleCar = 0f;
    public float MaxAngle;
    public float Acceleration = 500f;
    public float MaxSpeed = -800f;
    public float MaxBackSpeed = 600f;
    public float WheelSize;

    [Header ("Car Elements")]
    [SerializeField] WheelJoint2D[] _wheelJoints;
    JointMotor2D frontWheelMotor;
    JointMotor2D backWheelMotor;
    [SerializeField] LayerMask _wheelLayerMask;
    [SerializeField] AudioSource _engineAudSrc;
    [SerializeField] private bool _grounded;
    [SerializeField] private bool _bothGrounded;
    public LayerMask GroundLayerMask;

    public Transform BWheel;
    public Transform FWheel;

    public Rigidbody2D backRB;
    public Rigidbody2D frontRB;
    public Rigidbody2D carRB;

    [Header("Effects")]
    [SerializeField] GameObject _lightTrailEffect;
    [SerializeField] GameObject _smokeTrailEffect;
    [SerializeField] GameObject _idleSmokeEffect;



    

    [SerializeField] private bool _engineWorking = false;

    // Start is called before the first frame update
    void Start()
    {
        
        _wheelJoints = gameObject.GetComponents<WheelJoint2D>();
        frontWheelMotor = _wheelJoints[0].motor;
        backWheelMotor = _wheelJoints[1].motor;
        _direction = (int)(Mathf.Abs(transform.localScale.x)/transform.localScale.x);
        SetEffectSizes();
    }

    private void FixedUpdate()
    {
        SetCarGrounded();     
        ClampCarAngle();
        CarMovement();
    }
    public void StartCar()
    {
        _engineWorking = true;
        _smokeTrailEffect.SetActive(true);
        _lightTrailEffect.SetActive(true);
        _idleSmokeEffect.SetActive(false);
        _engineAudSrc.Play();
    }

    public void  StopCar()
    {
        _engineWorking = false;
        _smokeTrailEffect.SetActive(false);
        _engineAudSrc.Stop();
        
        frontWheelMotor.motorSpeed = 0; backWheelMotor.motorSpeed = 0;
        _wheelJoints[0].motor = backWheelMotor; _wheelJoints[1].motor = frontWheelMotor;

        backRB.velocity = new Vector2(0,0); frontRB.velocity = new Vector2(0, 0);
        carRB.velocity = Vector2.zero;
        
    }


    void ClampCarAngle()
    {
        AngleCar = transform.localEulerAngles.z;

        if (AngleCar > 100) AngleCar = AngleCar - 360;
        /*
        Vector3 CarEulerAngles = transform.rotation.eulerAngles;

        CarEulerAngles.z = (transform.eulerAngles.z > 180) ? transform.eulerAngles.z - 360 : transform.eulerAngles.z;
        CarEulerAngles.z = Mathf.Clamp(CarEulerAngles.z,-MaxAngle,MaxAngle);
        transform.rotation = Quaternion.Euler(CarEulerAngles);
        */
        }




    void CarMovement()
    {
        if (_engineWorking && _grounded)
        {
            backWheelMotor.motorSpeed = Mathf.Clamp(backWheelMotor.motorSpeed - (Acceleration - _gravity * Mathf.PI) * Time.deltaTime * _direction, MaxSpeed, MaxBackSpeed);
        }

        if (!_grounded && backWheelMotor.motorSpeed < 0)
        {
            backWheelMotor.motorSpeed = Mathf.Clamp(backWheelMotor.motorSpeed - (_deceleration - _gravity * Mathf.PI) * Time.deltaTime * _direction, MaxSpeed, MaxBackSpeed);
        }
        if (!_grounded && backWheelMotor.motorSpeed > 0)
        {
            backWheelMotor.motorSpeed = Mathf.Clamp(backWheelMotor.motorSpeed - (-_deceleration - _gravity * Mathf.PI) * Time.deltaTime * _direction, MaxSpeed, MaxBackSpeed);
        }

        frontWheelMotor = backWheelMotor;

        _wheelJoints[0].motor = backWheelMotor;
        _wheelJoints[1].motor = frontWheelMotor;
    }

    void SetCarGrounded()
    {

        if (!_grounded)
        {
            _grounded = Physics2D.OverlapCircle(BWheel.transform.position, WheelSize, GroundLayerMask);
            if (_grounded)
            {
                FeedBackManager.Instance.CarLandingFeedBack.PlayFeedbacks(BWheel.position);
            }
        }
        else
        {
            _grounded = Physics2D.OverlapCircle(BWheel.transform.position, WheelSize, GroundLayerMask);
        }
      
        _bothGrounded = _grounded && Physics2D.OverlapCircle(FWheel.transform.position, WheelSize, GroundLayerMask);
    }

    void SetEffectSizes()
    {
        _lightTrailEffect.transform.localScale *= new Vector2(_direction,1);
        _smokeTrailEffect.transform.localScale *= new Vector2(_direction, 1);
        _idleSmokeEffect.transform.localScale *= new Vector2(_direction, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.layer != _wheelLayerMask && !collision.gameObject.CompareTag("Dodger"))
        {
            if (_isDead) return; 
            FeedBackManager.Instance.CarExplodeFeedBack.PlayFeedbacks(transform.position);
            LevelManager.Instance.PlayerLostLevel();
            StopCar();
            _isDead = true;
        }
    }
}
