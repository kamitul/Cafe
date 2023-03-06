namespace Map
{
    public interface IBuilder
    {
        void BuildBackground();
        void BuildMisc();
        MapMatrix GetProduct();
    }
}