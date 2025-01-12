using EuroBackend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace EuroBackend.Services
{
    public class PredictionService
    {
        private readonly IMongoCollection<Prediction> _predictions;

        public PredictionService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _predictions = database.GetCollection<Prediction>(mongoDBSettings.Value.CollectionName);
        }

        public async Task CreateAsync(Prediction prediction)
        {
            await _predictions.InsertOneAsync(prediction);
            return;
        }

        public async Task UpdateAsync(string id, Prediction prediction) =>
            await _predictions.ReplaceOneAsync(p => p.Id == id, prediction);


        //nowe funkcje
        public async Task<List<Prediction>> GetAsync() =>
            await _predictions.Find(_ => true).ToListAsync();

        public async Task<Prediction> GetAsync(string group, int matchIndex) =>
            await _predictions.Find(p => p.Group == group && p.MatchIndex == matchIndex).FirstOrDefaultAsync();

        public async Task AddPrediction(string group, int matchIndex, MatchResult matchResult)
        {
            var prediction = await _predictions.Find(p => p.Group == group && p.MatchIndex == matchIndex).FirstOrDefaultAsync();
            if (prediction == null)
            {
                prediction = new Prediction
                { Group = group, MatchIndex = matchIndex, UsersPredictions = new List<MatchResult> { matchResult } };
                await CreateAsync(prediction);
            }
            else { 
                prediction?.UsersPredictions.Add(matchResult); // dodalem tu ?
                await UpdateAsync(prediction.Id, prediction);
            }
        }
    }
}
