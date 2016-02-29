using UnityEngine;
using System.Collections;

//Utility class for velocityRange
[System.Serializable]
public class VelocityRange
{
    public float velMin, velMax;

    //Constructor
    public VelocityRange(float velMin, float velMax)
    {
        this.velMin = velMin;
        this.velMax = velMax;
    }
}

//CoolGuyController class
public class CoolGuyController : MonoBehaviour {
    //Public Instance Variables
    public float moveForce;
    public float jumpForce;
    public Transform camera;
    public GameController gameController;

    public VelocityRange velocityRange;
    //public Transform groundCheck;


    //pRIVATE iNSTANCE vARIABLES
    private Rigidbody2D _rigidBody2D;
    private Transform _transform;
    private Animator _animator;   //may not use

    private AudioSource[] _audioSources;
    private AudioSource _coinSound;
    private AudioSource _jumpSound;

    private float _movingValue = 0;
    private bool _isFacingRight;
    private bool _isGrounded = true;
    private float _jump;



    // Use this for initialization
    void Start ()
    {
        
        this.velocityRange = new VelocityRange(300f, 1000f);
        this._rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        this._transform = gameObject.GetComponent<Transform>();
        //        this._animator = gameObject.GetComponent<Animator>();

        //this.moveForce = 50f;
        //this.jumpForce = 350f;
        this._isFacingRight = true;

        //place coolGuy in starting position
        this._spawn();

        this._audioSources = gameObject.GetComponents<AudioSource> ();
        this._coinSound = this._audioSources[0];
        this._jumpSound = this._audioSources[1];    
    }
	
	void FixedUpdate ()
    {
        Vector3 currentPosition = new Vector3(this._transform.position.x, this._transform.position.y, -1f);
        this.camera.position = currentPosition;

        //this._isGrounded = Physics2D.Linecast(this._transform.position,
        //                                      this.groundCheck.position,
        //                                      1 << LayerMask.NameToLayer("Ground"));

        //Debug.DrawLine(this._transform.position, this.groundCheck.position);


        float forceX = 0f;
        float forceY = 0f;

        float absVelX = Mathf.Abs(this._rigidBody2D.velocity.x);
        float absVelY = Mathf.Abs(this._rigidBody2D.velocity.y);



        //ensuring if player is grounded before any movement check
        if (this._isGrounded)
        {
            //check if player is moving
            this._movingValue = Input.GetAxis("Horizontal");
            this._jump = Input.GetAxis("Vertical");

            if (this._movingValue != 0)
            {
                //coolGuy moving
                if (this._movingValue > 0)
                {
                    //right
                    if (absVelX < this.velocityRange.velMax)
                    {
                        forceX = this.moveForce;
                        this._isFacingRight = true;
                        this._flip();
                    }
                }
                else if (this._movingValue < 0)
                {
                    //left
                    if (absVelX < this.velocityRange.velMax)
                    {
                        forceX = -this.moveForce;
                        this._isFacingRight = false;
                        this._flip();
                    }
                }
            }
            else if (this._jump > 0)
            {
                if (absVelY < this.velocityRange.velMax)
                {
                    forceY = this.jumpForce;
                    this._jumpSound.Play();
                }
            }
        }

        this._rigidBody2D.AddForce(new Vector2(forceX, forceY));
    }

    void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Coin"))
        {
            this.gameController.ScoreValue += 10;
            this._coinSound.Play();
        }
        if (otherCollider.gameObject.CompareTag("Death"))
        {
            this._spawn();
            this.gameController.livesValue--;
        }
    }
    void OnCollisionStay2D(Collision2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Platform"))
        {
            this._isGrounded = true;
        }
    }


    //private method
    private void _flip()
    {
        if (this._isFacingRight)
        {
            this._transform.localScale = new Vector2(.0154f, .0112f);
        }
        else
        {
            this._transform.localScale = new Vector2(-.0154f, .0112f);
        }
    }


    public void _spawn()
    {
        this._transform.position = new Vector3(-4.67f, 0.93f, 0);
    }
}
