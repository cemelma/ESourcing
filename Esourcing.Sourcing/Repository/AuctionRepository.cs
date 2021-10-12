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
    public class AuctionRepository : IAuctionRepository
    {
        private readonly ISourcingContext _context;

        public AuctionRepository(ISourcingContext context)
        {
            _context = context;
        }

        public async Task Create(Auction auction)
        {
            await _context.Auctions.InsertOneAsync(auction);
        }

        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Auction> filter = Builders<Auction>.Filter.Eq(d => d.Id, id);
            DeleteResult deleteResult = await _context.Auctions.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<Auction> GetAuction(string id)
        {
            return await _context.Auctions.Find(d => d.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Auction> GetAuctionByName(string name)
        {
            FilterDefinition<Auction> filter = Builders<Auction>.Filter.Eq(d => d.Name, name);
            return await _context.Auctions.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Auction>> GetAuctions()
        {
            return await _context.Auctions.Find(a => true).ToListAsync();
        }

        public async Task<bool> Update(Auction auction)
        {
            var updateResult = await _context.Auctions.ReplaceOneAsync(d => d.Id.Equals(auction.Id), auction);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
