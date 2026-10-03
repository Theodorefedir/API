using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using VideoGames.BLL.Dtos;
using VideoGames.DAL.Entities;
using VideoGames.DAL.Repositories;

namespace VideoGames.BLL.Services
{
    public class DeveloperService
    {
        private readonly DeveloperRepository _developerRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateDeveloperDto> _createValidator;

        public DeveloperService(
            DeveloperRepository developerRepository,
            IMapper mapper,
            IValidator<CreateDeveloperDto> createValidator)
        {
            _developerRepository = developerRepository;
            _mapper = mapper;
            _createValidator = createValidator;
        }

        public async Task<List<DeveloperDto>> GetAllAsync(CancellationToken ct = default)
        {
            var developers = await _developerRepository
                .GetAll()
                .Include(d => d.Games)
                .OrderBy(d => d.Id)
                .ToListAsync(ct);

            return _mapper.Map<List<DeveloperDto>>(developers);
        }

        public async Task<DeveloperDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var developer = await _developerRepository.GetByIdAsync(id, ct);
            if (developer is null) return null;

            return _mapper.Map<DeveloperDto>(developer);
        }

        public async Task<DeveloperDto> CreateAsync(CreateDeveloperDto dto, CancellationToken ct = default)
        {
            var validation = await _createValidator.ValidateAsync(dto, ct);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }

            var developer = _mapper.Map<Developer>(dto);
            await _developerRepository.CreateAsync(developer, ct);
            return _mapper.Map<DeveloperDto>(developer);
        }

        public async Task<bool> UpdateAsync(UpdateDeveloperDto dto, CancellationToken ct = default)
        {
            var existing = await _developerRepository.GetByIdAsync(dto.Id, ct);
            if (existing is null) return false;

            _mapper.Map(dto, existing);
            await _developerRepository.UpdateAsync(existing, ct);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var existing = await _developerRepository.GetByIdAsync(id, ct);
            if (existing is null) return false;

            await _developerRepository.DeleteAsync(existing, ct);
            return true;
        }
    }
}