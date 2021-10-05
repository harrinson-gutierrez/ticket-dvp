using Dapper;

namespace Domain.Common 
{ 
    public abstract class BaseEntity<T>
    {
        [Key]
        public virtual T id { get; set; }
    }
}
