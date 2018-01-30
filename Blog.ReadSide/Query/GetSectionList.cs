using System.Collections.Generic;
using Blog.ReadSide.Model;
using Core.CQRS.Query;

namespace Blog.ReadSide.Query
{
    public class GetSectionList : IQuery<IEnumerable<SectionDetailsRecord>>
    {
        
    }
}