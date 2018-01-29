using System.Collections.Generic;
using Blog.WriteSide.Model.ReadSide;
using Core.CQRS.Query;

namespace Blog.ReadSide.Query
{
    public class GetSectionList : IQuery<IEnumerable<SectionDetailsRecord>>
    {
        
    }
}