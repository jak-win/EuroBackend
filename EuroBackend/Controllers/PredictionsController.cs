using System;
using Microsoft.AspNetCore.Mvc;
using EuroBackend.Services;
using EuroBackend.Models;

namespace EuroBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PredictionsController: Controller
    {
        private readonly PredictionService _predictionService;

        public PredictionsController(PredictionService predictionService)
        {
            _predictionService = predictionService;
        }

        //tu roznica filmik a czat
        [HttpGet("{group}/{matchIndex}")]
        public async Task<IActionResult> Get(string group, int matchIndex)
        {
            var prediction = await _predictionService.GetAsync(group, matchIndex);
            if (prediction == null) { 
                return NotFound();
            }
            return Ok(prediction);
        }
        
        //roznica
        [HttpPost("{group}/{matchIndex}")]
        public async Task<IActionResult> Post(string group, int matchIndex, [FromBody] MatchResult matchResult)
        {
            await _predictionService.AddPrediction(group, matchIndex, matchResult);   // roznica
            return Ok(new {Success = true});
        }

        //??? idk czy g nie bylo w czacie
     //   [HttpPut("{group}/{matchIndex}")]
     //   public async Task<IActionResult> AddToPredictions(string group, int matchIndex)
     //   {

     //   }
    }
}
