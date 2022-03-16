namespace GhostWriter.Application.Common.Models.Shared
{
    public class LookupInputModel : PaginationModel
    {
        public string Search { get; set; }
        public OrderColumn OrderColumn { get; set; }
    }

    public class OrderColumn
    {
        public string OrderByColumn { get; set; }
        public bool OrderByAsc { get; set; }
    }
}
