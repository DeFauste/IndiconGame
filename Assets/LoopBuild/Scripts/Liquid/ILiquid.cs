namespace Assets.Scripts.Liquid
{
    public interface ILiquid
    {
        EWaterProperty Property { get; set; }
        bool SetPropertyWater(EWaterProperty property);
        float GetSquare();
        float Pump(int forcePump);
        float Squeeze(int forcePump, float square);
    }
}
