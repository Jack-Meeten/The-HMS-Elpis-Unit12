using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    private Rigidbody _rB;
    public float force;
    public float rotateForce;

    private Vector3 targetForce;

    public GameObject player;
    public GameObject _camera;
    public GameObject cart;
    public Transform spawnPoint;
    public Transform camPoint;

    private bool isActive;

    private void Start()
    {
        _rB = GetComponent<Rigidbody>();
    }

    public void Update()
    {

        if (Input.GetKey(KeyCode.W) && isActive)
        {
            MoveForward();
        }
        if (Input.GetKey(KeyCode.A) && isActive)
        {
            RotateCart(-rotateForce, -6);
        }
        if (Input.GetKey(KeyCode.D) && isActive)
        {
            RotateCart(rotateForce, 6);
        }
        if (isActive)
        {
            _camera.transform.position = camPoint.position;
            _camera.transform.rotation = camPoint.rotation;
            _camera.GetComponentInChildren<Transform>().rotation = camPoint.rotation;
            _camera.transform.GetChild(0).transform.rotation = camPoint.rotation;

            Debug.Log(_rB.velocity.magnitude);
            if (_rB.velocity.magnitude > 10)
            {
                _rB.velocity = _rB.velocity.normalized * 10;
            }
        }
        if (isActive && Input.GetKey(KeyCode.E))
        {
            exit();
        }
    }

    private void MoveForward()
    {
        _rB.AddRelativeForce(new Vector3(0, 0, force));
    }
    
    private void RotateCart(float rotate, float drift)
    {
        _rB.AddRelativeTorque(new Vector3(0, rotate, 0));
        //_rB.AddRelativeForce(new Vector3(drift, 0, 0));
    }

    private void enter()
    {
        player.SetActive(false);
        isActive = true;
        _camera.GetComponent<MoveCamera>().enabled = false;
    }

    private void exit()
    {
        player.SetActive(true);
        player.transform.position = spawnPoint.position;
        isActive = false;
        _camera.GetComponent<MoveCamera>().enabled = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            enter();
        }
    }
}
