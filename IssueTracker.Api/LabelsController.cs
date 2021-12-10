using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssueTracker.Api.Controllers;
using IssueTracker.Data;
using IssueTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace IssueTracker.Api
{
    public class LabelsController : AuthorisedController
    {
        private readonly ILabelsService _labelsService;
        public LabelsController(ILabelsService labelsService)
        {
            _labelsService = labelsService;
        }

        [HttpGet("labels-for-project")]
        public async Task<List<LabelDto>> GetLabelsForProject(int projectId)
        {
            return await _labelsService.GetLabelsForProject(projectId);
        }
        
        [HttpGet("search-labels-for-project")]
        public async Task<List<LabelDto>> SearchLabels(int projectId, string searchTerm)
        {
            return await _labelsService.SearchLabels(projectId, searchTerm);
        }

        [HttpPost("add")]
        public async Task<int> AddLabel([FromBody] AddLabelDto dto)
        {
            return await _labelsService.AddLabel(dto);
        }

        [HttpPost("update")]
        public async Task UpdateLabel(UpdateLabelDto dto)
        {
            await _labelsService.UpdateLabel(dto);
        }

        [HttpGet("label-colours")]
        public List<LabelColourOption> GetLabelColours()
        {
            return Enum.GetValues<LabelColour>().Select(x => new LabelColourOption
            {
                Name = x.ToString(),
                LabelColour = x
            }).ToList();
        }
    }
}