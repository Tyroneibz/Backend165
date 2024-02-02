using MongoDB.Driver;
using Backend165.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend165.Services
{
    public class OrderService
    {
        private readonly IMongoCollection<SkiService> _skiServices;

        public OrderService(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("SkiServiceDB");
            _skiServices = database.GetCollection<SkiService>("SkiOrders");
        }

        // Alle Aufträge abrufen
        public async Task<List<SkiService>> GetAllAsync()
        {
            return await _skiServices.Find(_ => true).ToListAsync();
        }

        // Einen Auftrag nach ID abrufen
        public async Task<SkiService> GetByIdAsync(string id)
        {
            return await _skiServices.Find(service => service.Id == id).FirstOrDefaultAsync();
        }

        // Einen neuen Auftrag hinzufügen
        public async Task CreateAsync(SkiService skiService)
        {
            await _skiServices.InsertOneAsync(skiService);
        }

        // Auftragsdaten aktualisieren
        public async Task UpdateAsync(string id, SkiService skiService)
        {
            await _skiServices.ReplaceOneAsync(service => service.Id == id, skiService);
        }

        // Einen Auftrag löschen
        public async Task DeleteAsync(string id)
        {
            await _skiServices.DeleteOneAsync(service => service.Id == id);
        }
    }
}
