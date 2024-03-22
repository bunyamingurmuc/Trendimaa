using System.Reflection.Metadata.Ecma335;
using Trendeimaa.Entities.Interface;

namespace Trendeimaa.Entities
{
    public class Search:BaseEntity
    {
        public Search()
        {
            SearchRelateds= new List<SearchRelated>();
        }
        public string Word { get; set; }
        public List<SearchRelated> SearchRelateds { get; set; }
    }
}
