using UnityEngine;

public class Movement : MonoBehaviour
{    
    [SerializeField] float thrustSpeed = 10f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
       if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * Time.deltaTime * thrustSpeed);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainBooster.Stop();
    }

    void RotateLeft()
    {
        ApplyRotation(rotationSpeed);
            if (!rightBooster.isPlaying)
            {
                rightBooster.Play();
            }
    }

    void RotateRight()
    {
        ApplyRotation(-rotationSpeed);
            if (!leftBooster.isPlaying)
            {
                leftBooster.Play();
            }
    }

    void StopRotating()
    {
        rightBooster.Stop();
        leftBooster.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //freezing rotation to manually rotate
        transform.Rotate(rotationThisFrame * Vector3.forward * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
