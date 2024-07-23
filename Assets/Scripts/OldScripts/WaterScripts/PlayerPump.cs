using System;
using System.Collections;
using UnityEngine;

public class PlayerPump : MonoBehaviour
{
    [SerializeField] GameObject player;
    public static Action<EWaterProperty> ActionWaterProperty;
    [SerializeField] EWaterProperty currentPropery = EWaterProperty.None;
    private PlayerMove playerMove;
    IWaterPump squeezeIntercat;
    IWaterIneract waterIneract;
    public int PumpForce = 1;
    public float HeightForce = 1;
    public float JumpForceV = 1;
    private bool isPump = false;
    [SerializeField] private float squareV = 0;
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
        }
    }
    private void WaterP(Collider2D collision)
    {
        IWaterIneract i = collision.gameObject.GetComponent<IWaterIneract>();

        if (i != null && waterIneract == null)
        {
            waterIneract = i;
            if(currentPropery == EWaterProperty.None)
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
                squeezeIntercat.SetPropertyWater(currentPropery);
                if (player != null && squareV > 0 && squeezeIntercat.SetPropertyWater(currentPropery))
                {
                    player.transform.localScale = new Vector3(player.transform.localScale.x - 0.01f* HeightForce, player.transform.localScale.y - 0.01f* HeightForce, player.transform.localScale.z);
                    playerMove.JumpForce -= JumpForceV;
                    squareV -= squeezeIntercat.Fresh(PumpForce, squareV);
                }
                else
                {
                    StopCoroutine(Squeeze());
                    isPump = false;
                }
                if (player.transform.localScale.x < 1 || player.transform.localScale.y < 1)
                {
                    player.transform.localScale = Vector3.one;
                }
                if(squareV < 0f)
                {
                    squareV = 0f;
                    SetProperty(EWaterProperty.None);
                }
            }

        }
    }
    IEnumerator Pupm()
    {
        yield return new WaitForSeconds(0.2f);
        if(waterIneract != null && (waterIneract.Property == currentPropery || currentPropery == EWaterProperty.None))
        {

            float i = waterIneract.Pump(PumpForce);
            squareV += i;

            if (i == 0f) {
                StopCoroutine(Pupm());
                isPump = false;
            }
            if(player != null)
            {
                player.transform.localScale = new Vector3(player.transform.localScale.x +0.01f* HeightForce, player.transform.localScale.y + 0.01f* HeightForce, player.transform.localScale.z);
                playerMove.JumpForce += JumpForceV;
            }
        }
    }
}
