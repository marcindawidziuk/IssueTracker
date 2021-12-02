using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssueTracker.Data;
using IssueTracker.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace IssueTracker.Services
{
    public interface ILabelsService
    {
        Task<List<LabelDto>> GetLabelsForProject(int projectId);
        Task<List<LabelDto>> SearchLabels(int projectId, string searchTerm);
        Task<int> AddLabel(AddLabelDto dto);
        Task UpdateLabel(UpdateLabelDto dto);
    }

    public class LabelDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
    }

    public class AddLabelDto
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
    }
    
    public class UpdateLabelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
    public class LabelsService : ILabelsService
    {
        private readonly IContextFactory _contextFactory;
        public LabelsService(IContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<LabelDto>> GetLabelsForProject(int projectId)
        {
            await using var db = _contextFactory.Create();

            return await db.Labels
                .Where(x => x.ProjectId == projectId)
                .Select(x => new LabelDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();


        }

        public async Task<List<LabelDto>> SearchLabels(int projectId, string searchTerm)
        {
            await using var db = _contextFactory.Create();

            return await db.Labels
                .Where(x => x.ProjectId == projectId)
                .Where(x => string.IsNullOrEmpty(searchTerm) 
                            || x.Name.ToLower().Contains(searchTerm.ToLower()))
                .Select(x => new LabelDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();
        }

        public async Task<int> AddLabel(AddLabelDto dto)
        {
            await using var db = _contextFactory.Create();

            var label = new Label
            {
                Name = dto.Name,
                ProjectId = dto.ProjectId
            };
            db.Labels.Add(label);
            await db.SaveChangesAsync();
            return label.Id;
        }

        public async Task UpdateLabel(UpdateLabelDto dto)
        {
            await using var db = _contextFactory.Create();

            var label = await db.Labels.SingleAsync(x => x.Id == dto.Id);
            label.Name = dto.Name;
            await db.SaveChangesAsync();
        }
    }
}