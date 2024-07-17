using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPump : MonoBehaviour
{
    [SerializeField] GameObject player;
    public Action<EWaterProperty> ActionWaterProperty;
    EWaterProperty currentPropery = EWaterProperty.None;
    private PlayerMove playerMove;
    IWaterPump _interacteble;
    IWaterIneract waterIneract;
    public int PumpForce = 30;
    public float V = 0;
    private bool isPump = false;
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
    private void InteractWaterPump()
    {
        if (_interacteble != null && Input.GetKey(KeyCode.F))
        {
            if (!isPump)
            {
                isPump = true;
                StartCoroutine(Squeeze());
            }
        }
        else if(Input.GetKeyUp(KeyCode.F))
        {
            Debug.Log("Out");
            StopCoroutine(Squeeze());
            isPump = false;
            Debug.Log("Stop");

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
            Debug.Log("Slime");
            playerMove.HaveWallSliding = true;
            playerMove.HaveWallJumping = true;
        }
        else if (waterProperty == EWaterProperty.Gasoline)
        {
            Debug.Log("Gasoline");
            playerMove.HaveDoubleJump = true;
        } else
        {
            Debug.Log("None");
            playerMove.HaveDoubleJump = false;
            playerMove.HaveWallSliding = false;
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
        if (i != null && _interacteble == null)
        {
            _interacteble = i;
            _interacteble.SetPropertyWater(currentPropery);
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
        StopCoroutine(Squeeze());
        isPump = false;
        waterIneract = null;
        _interacteble = null;

    }
    IEnumerator Squeeze()
    {
        while (V > 0 && isPump)
        {

            yield return new WaitForSeconds(0.1f);
            if (_interacteble != null)
            {
                Debug.Log("Squeeze");
                if (player != null && V > 0)
                {
                    V -= 1;
                    player.transform.localScale = new Vector3(player.transform.localScale.x - 0.01f, player.transform.localScale.y - 0.01f, player.transform.localScale.z);

                    _interacteble.Pump(PumpForce);
                }
                else
                {
                    Debug.Log("Squeeze Stop");
                    StopCoroutine(Squeeze());
                    isPump = false;
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
            if (i == 0f) { StopCoroutine(Pupm()); isPump = false; }
            V += i;
            if(player != null)
            {
                player.transform.localScale = new Vector3(player.transform.localScale.x +0.01f, player.transform.localScale.y + 0.01f, player.transform.localScale.z);
            }
        }
    }
}
