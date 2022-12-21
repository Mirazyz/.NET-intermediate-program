namespace Adapter
{
    public class CustomElements : IContainer<int>, IElements<int>
    {
        public IEnumerable<int> Items => GetElements();

        public int Count => Items.Count();

        public IEnumerable<int> GetElements()
        {
            return new List<int>
            {
                1,
                2,
                3,
                4,
                5
            };
        }
    }
}
