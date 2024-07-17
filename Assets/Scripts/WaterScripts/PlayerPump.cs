using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPump : MonoBehaviour
{
    [SerializeField] GameObject player;
    public float JumpBustSize = 0.1f;
    public Action<EWaterProperty> ActionWaterProperty;
    EWaterProperty currentPropery = EWaterProperty.None;
    private PlayerMove playerMove;
    IWaterPump squeezeIntercat;
    IWaterIneract waterIneract;
    public int PumpForce = 30;
    private bool isPump = false;
    private float squareV = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerMove = player.GetComponent<PlayerMove>();
        ActionWaterProperty += SetPropertyPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        InteractWaterPump();
    }
    private void FixedUpdate()
    {
        if(squareV <= 0) SetProperty(EWaterProperty.None);  
    }
    private void InteractWaterPump()
    {
        if (squeezeIntercat != null && Input.GetKey(KeyCode.F))
        {
            if (!isPump)
            {
                isPump = true;
                StartCoroutine(Squeeze());
            }
        }
        else if(Input.GetKeyUp(KeyCode.F))
        {
            StopCoroutine(Squeeze());
            isPump = false;
        }
    }
    private void SetProperty(EWaterProperty property)
    {
        currentPropery = property;
        ActionWaterProperty?.Invoke(EWaterProperty.None);
        ActionWaterProperty?.Invoke(property);
    }

    private void SetPropertyPlayer(EWaterProperty waterProperty)
    {
        if (waterProperty == EWaterProperty.Slime)
        {
            playerMove.HaveWallJumping = true;
        }
        else if (waterProperty == EWaterProperty.Gasoline)
        {
            playerMove.HaveDoubleJump = true;
        } else
        {
            playerMove.HaveDoubleJump = false;
            playerMove.HaveWallJumping = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        WaterP(collision);

        WaterF(collision);

    }
    private void WaterF(Collider2D collision)
    {
        IWaterPump i = collision.GetComponent<IWaterPump>();
        if (i != null && squeezeIntercat == null)
        {
            squeezeIntercat = i;
            squeezeIntercat.SetPropertyWater(currentPropery);
        }
    }
    private void WaterP(Collider2D collision)
    {
        IWaterIneract i = collision.gameObject.GetComponent<IWaterIneract>();

        if (i != null && waterIneract == null)
        {
            waterIneract = i;
            if(waterIneract.Property != currentPropery)
            {
                SetProperty(waterIneract.Property);
            }
            if (isPump == false)
            {
                isPump = true;
                StartCoroutine(Pupm());
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        StopCoroutine(Pupm());
        waterIneract?.ResizeSquare();
        StopCoroutine(Squeeze());
        isPump = false;
        waterIneract = null;
        squeezeIntercat = null;

    }
    IEnumerator Squeeze()
    {
        while (squareV > 0 && isPump)
        {

            yield return new WaitForSeconds(0.1f);
            if (squeezeIntercat != null)
            {
                if (player != null && squareV > 0)
                {
                    player.transform.localScale = new Vector3(player.transform.localScale.x - 0.01f, player.transform.localScale.y - 0.01f, player.transform.localScale.z);
                    playerMove.JumpForce -= JumpBustSize;
                    squareV -= squeezeIntercat.Fresh(PumpForce, squareV);
                    Debug.Log($"рейсыхи {squareV}");
                }
                else
                {
                    StopCoroutine(Squeeze());
                    isPump = false;
                    SetProperty(EWaterProperty.None);
                }
                if (player.transform.localScale.x < 1 || player.transform.localScale.y < 1)
                {
                    player.transform.localScale = Vector3.one;
                }
                if(squareV < 0f)
                {
                    squareV = 0f;
                }
            }

        }
    }
    IEnumerator Pupm()
    {
        yield return new WaitForSeconds(1);
        if(waterIneract != null)
        {

            float i = waterIneract.Pump(PumpForce);     
            squareV += i;

            if (i == 0f) { 
                StopCoroutine(Pupm()); 
                isPump = false;
            }
            if(player != null)
            {
                player.transform.localScale = new Vector3(player.transform.localScale.x +0.01f, player.transform.localScale.y + 0.01f, player.transform.localScale.z);
                playerMove.JumpForce += JumpBustSize;
            }
        }
    }
}
