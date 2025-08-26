using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private GameObject[] weapons;
    private int weaponIndex = 0;

    [SerializeField]
    private Transform shootTransform;

    [SerializeField]
    private float shootInterval = 0.05f; //
    private float lastShotTiime = 0f; //마지막으로 미사일을 쏜 시각

    // Update is called once per frame
    void Update()
    {
        // float horizontalInput = Input.GetAxisRaw("Horizontal");
        // //float verticalInput = Input.GetAxisRaw("Vertical");
        // Vector3 moveTo = new Vector3(horizontalInput, 0f, 0f);
        // transform.position += moveTo * moveSpeed * Time.deltaTime;

        // Vector3 moveTo = new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
        // if (Input.GetKey(KeyCode.LeftArrow)){
        //     transform.position -= moveTo;
        // }
        // else if (Input.GetKey(KeyCode.RightArrow)) {
        //     transform.position += moveTo;
        // }

        Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        float toX = Mathf.Clamp(mousePos.x, -2.35f, 2.35f);
        transform.position = new Vector3(toX, transform.position.y, transform.position.z);

        if (GameManager.instance.isGameOver == false) {
            Shoot();
        }

        
    }

    void Shoot()
    {
        if (Time.time - lastShotTiime > shootInterval)
        {
            Instantiate(weapons[weaponIndex], shootTransform.position, Quaternion.identity);
            lastShotTiime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")
        {
            GameManager.instance.GameSetOver();
            Destroy(gameObject);

        }
        else if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            GameManager.instance.IncreaseCoin();
        }
    }

    public void Upgrade()
    {
        weaponIndex += 1;
        if (weaponIndex >= weapons.Length)
        {
            weaponIndex = weapons.Length - 1;
        }
    }
}
