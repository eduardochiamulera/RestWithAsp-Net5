using System.Collections.Generic;

namespace RestWithASPNETUdemy.Hypermedia.Abstract
{
    public interface ISupporstHyperMedia
    {
        List<HyperMediaLink> Links { get; set; }
    }
}
