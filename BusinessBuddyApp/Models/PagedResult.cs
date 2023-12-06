namespace BusinessBuddyApp.Models
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }
        public int TotalItemCount { get; set; }

        public PagedResult(List<T> items, int totalCount, int pageSize, int pageNumber)
        {
            Items = items;
            TotalItemCount = totalCount;
            ItemsFrom = pageSize * pageNumber - pageSize + 1;
            ItemsTo = pageSize * pageNumber;
            TotalPages = (int)Math.Ceiling(Convert.ToDouble(items.Count / pageSize));
        }
    }
}
