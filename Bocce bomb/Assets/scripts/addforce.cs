using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class addforce : MonoBehaviour
{
    Vector3 gyroAccel;
    Vector3 deviceAccel;

    Rigidbody rigidB;
    public Slider greenUp;//used to indicate a good flick by user
    public Slider redDown;//used to show a bad flick or downward force
    public Slider blueLeft;//displays force given left
    public Slider blueRight;//displays force given right
    public Text X;
    public Text Y;
    public Text Z;
    public Text gyroX;
    public Text gyroY;
    public Text gyroZ;
    public int multiX;//force multipliers
    public int multiY;
    public int multiZ;
    public int torqueForce;
    public Camera main;
    public ParticleSystem burst;
    public levelManager lvlmngr;
    public bool playing;
    public int taps;
   
    bool motionStopped;//used to determine end of flick
    float touchTimer;//used to track touch time
    public float touchLimit = 1000;
    // Use this for initialization
    void Start()
    {
        taps = 0;
        playing = false;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToPortraitUpsideDown = false;
        rigidB = GetComponent<Rigidbody>();
        Input.gyro.enabled = true;
        rigidB.constraints = RigidbodyConstraints.FreezePosition;
    }

    // Update is called once per frame
    void Update()
    {
        int fingerCount = 0;
        gyroX.text = "X: " + Input.gyro.rotationRateUnbiased.x.ToString();
        gyroY.text = "Y: " + Input.gyro.rotationRateUnbiased.y.ToString();
        gyroZ.text = "Z: " + Input.gyro.rotationRateUnbiased.z.ToString();
        //		X.text = transform.position.x.ToString();
        //		Y.text = transform.position.y.ToString();
        //		Z.text = transform.position.z.ToString();

        foreach (Touch touch in Input.touches)
        {

            if (touch.phase == TouchPhase.Began)
            {
                //acceleration = transform.position;
                gyroAccel = Vector3.zero;
                deviceAccel = Vector3.zero;
                touchTimer = 0;//tracking length of touch phase
                motionStopped = false;
                greenUp.gameObject.SetActive(true);
                redDown.gameObject.SetActive(true);
                blueLeft.gameObject.SetActive(true);
                blueRight.gameObject.SetActive(true);
            }

            if (touch.phase != TouchPhase.Ended && touchTimer < touchLimit)
            {
                fingerCount++;

                if (fingerCount == 1)
                {
                    //					int i = 0;
                    //					while (i < Input.accelerationEventCount) {
                    //						AccelerationEvent ACC = Input.GetAccelerationEvent (i);
                    //						X.text = ACC.acceleration.x.ToString ();
                    //						Y.text = ACC.acceleration.y.ToString ();
                    //						Z.text = ACC.acceleration.z.ToString ();
                    //						acceleration.y += ACC.acceleration.y * ACC.deltaTime;
                    //							//acceleration.Normalize ();
                    //					
                    //						i++;
                    //					}
                    //foreach(AccelerationEvent accEvent in Input.accelerationEvents)
                    //{
                                               
                    //        deviceAccel += accEvent.acceleration * accEvent.deltaTime;
                        
                    //}
                    gyroAccel += Input.gyro.rotationRateUnbiased;
                    touchTimer += Time.deltaTime;
                    if (gyroAccel.x > 0)
                    {
                        greenUp.value = gyroAccel.x;
                    }
                    else {
                        redDown.value = -gyroAccel.x;
                    }
                    if(gyroAccel.z < 0)
                    {
                        blueRight.value = -gyroAccel.z;
                    }
                    else
                    {
                        blueLeft.value = gyroAccel.z;
                    }
                    //if(Mathf.Abs(Input.gyro.rotationRateUnbiased.x) < .0001) { motionStopped = true; }
                    

                }

            }
            else {
                if (lvlmngr.playing)
                {
                    rigidB.constraints = RigidbodyConstraints.None;
                   // rigidB.AddForce((gyroAccel.y - gyroAccel.z) * multiX, gyroAccel.x * multiY, Mathf.Abs(gyroAccel.x * multiZ));//(lateral, vertical, forward)
                  //rigidB.AddForce(((gyroAccel.y - gyroAccel.z)* multiX)+(deviceAccel.x * multiX), (gyroAccel.x* multiY)+(deviceAccel.z * multiY), Mathf.Abs((gyroAccel.x * multiZ)+(deviceAccel.y * multiZ)));//(lateral, vertical, forward)
                    //rigidB.AddForce(-gyroAccel.z* multiX, gyroAccel.x * multiY, Mathf.Abs(gyroAccel.x * multiZ));//(lateral, vertical, forward)<-this one works okay
                    rigidB.AddForce(-gyroAccel.z * multiX, gyroAccel.x * multiY, Mathf.Abs(gyroAccel.x * multiZ));//(lateral, vertical, forward)
                    rigidB.AddTorque(main.transform.forward * -gyroAccel.y * torqueForce);
                    touchTimer = 0;
                    greenUp.value = 0;
                    redDown.value = 0;
                    blueLeft.value = 0;
                    blueRight.value = 0;
                    greenUp.gameObject.SetActive(false);
                    redDown.gameObject.SetActive(false);
                    blueRight.gameObject.SetActive(false);
                    blueLeft.gameObject.SetActive(false);


                }

            }
        }
    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Finish")
        {
            lvlmngr.score++;
            burst.Play();
            GetComponent<AudioSource>().Play();
            col.gameObject.GetComponent<buldingSupervisor>().playExplosion();

        }
        lvlmngr.bombs--;
        GetComponent<AudioSource>().Play();
        lvlmngr.setBocce();
        rigidB.constraints = RigidbodyConstraints.FreezePosition;


    }
}
