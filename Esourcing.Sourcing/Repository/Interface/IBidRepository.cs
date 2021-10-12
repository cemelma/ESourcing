using Esourcing.Sourcing.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Esourcing.Sourcing.Repository.Interface
{
    public interface IBidRepository
    {
        Task SendBid(Bid bid);
        Task<List<Bid>> GetBidByAuctionId(string id);
        Task<Bid> GetWinnerBid(string id);
    }
}
