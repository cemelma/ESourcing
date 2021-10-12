using Esourcing.Sourcing.Data.Inteface;
using Esourcing.Sourcing.Entities;
using Esourcing.Sourcing.Repository.Interface;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Esourcing.Sourcing.Repository
{
    public class BidRepository : IBidRepository
    {
        private readonly ISourcingContext _context;

        public BidRepository(ISourcingContext context)
        {
            _context = context;
        }

        public async Task<List<Bid>> GetBidByAuctionId(string id)
        {
            FilterDefinition<Bid> filter = Builders<Bid>.Filter.Eq(a => a.AuctionId, id);
            List<Bid> bids = await _context.Bids.Find(filter).ToListAsync();
            bids = bids.OrderByDescending(d => d.CreatedAt)
                .GroupBy(d => d.SellerUserName)
                .Select(d => new Bid
                {
                    AuctionId = d.FirstOrDefault().AuctionId,
                    Price = d.FirstOrDefault().Price,
                    CreatedAt = d.FirstOrDefault(). CreatedAt,
                    SellerUserName = d.FirstOrDefault().SellerUserName,
                    ProductId = d.FirstOrDefault().ProductId,
                    Id = d.FirstOrDefault().Id
                }).ToList();

            return bids;
        }

        public async Task<Bid> GetWinnerBid(string id)
        {
            List<Bid> bids = await GetBidByAuctionId(id);

            return bids.OrderByDescending(d => d.Price).FirstOrDefault();
        }

        public async Task SendBid(Bid bid)
        {
            await _context.Bids.InsertOneAsync(bid);
        }
    }
}
