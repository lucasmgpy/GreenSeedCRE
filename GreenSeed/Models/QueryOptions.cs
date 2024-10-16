using System.Linq.Expressions;

namespace GreenSeed.Models
{
    public class QueryOptions<T> where T : class
    {

        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, bool>> Where { get; set; }

        public bool HasWhere => Where != null;

        private string[] includes = Array.Empty<string>();
        public string OrderByDirection { get; set; } = "ASC";
        public bool IsOrderByDescending => OrderByDirection.Equals("DESC", StringComparison.OrdinalIgnoreCase);


        public string Includes
        {
            set => includes = value.Replace(" ", "").Split(',');
        }

        public string[] GetIncludes() => includes;

        public bool HasOrderBy => OrderBy != null;
    }
}