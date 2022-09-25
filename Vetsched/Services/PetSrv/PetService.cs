using AutoMapper;
using Loader.infrastructure.GenericRepository;
using Vetsched.Data.DBContexts;
using Vetsched.Data.Dtos.Request;
using Vetsched.Data.Dtos.Response;
using Vetsched.Data.Entities;
using Vetsched.Data.Enums;
using Vetsched.Helper.Conversions;

namespace Vetsched.Services.PetSrv
{
    public class PetService : IPetService
    {
        private readonly IRepository<Pet, VetschedContext> _repository;
        private readonly IMapper _mapper;
        private readonly IConversionHelper _conversionHelper;
        public PetService(
            IRepository<Pet, VetschedContext> repository,
            IMapper mapper,
            IConversionHelper conversionHelper
            )
        {
            _mapper = mapper;
            _repository = repository;
            _conversionHelper = conversionHelper;
        }
        public async Task<PetCreateResponseDto> CreatePet(PetCreateRequestDto request)
        {
            var pet = _mapper.Map<Pet>(request);
            var createdPet = await _repository.AddAsync(pet);
            var response = _mapper.Map<PetCreateResponseDto>(createdPet);
            return response;
        }

        public async Task<bool> DeletePet(Guid PetId)
        {
            var response = await _repository.DeleteAsync(PetId);
            return true;
        }

        public async Task<PetResponseDto> GetPet(Guid PetId)
        {
            var pet = await _repository.GetByIdAsync(PetId);
            var response = _mapper.Map<PetResponseDto>(pet);
            return response;
        }

        public async Task<List<PetResponseDto>> GetPets(Guid PetLoverId)
        {
            var pets = await _repository.GetMany(x => x.PetLoverId == PetLoverId);
            var response = _mapper.Map<List<PetResponseDto>>(pets);
            return response;
        }

        public async Task<List<PetMinimalResponseDto>> GetPetsMinimal(Guid PetLoverId)
        {
            var pets = await _repository.GetMany(x => x.PetLoverId == PetLoverId);
            var response = _mapper.Map<List<PetMinimalResponseDto>>(pets);
            return response;
        }

        public async Task<bool> UpdatePet(PetUpdateRequestDto request)
        {
            bool shouldUpdate = false;
            var pet = await _repository.GetByIdAsync(request.Id);
            if(request.Name is not null)
            {
                if(request.Name != pet.Name)
                {
                    shouldUpdate = true;
                    pet.Name = request.Name;
                }
            }
            if (request.Title is not null)
            {
                if (request.Title != pet.Title)
                {
                    shouldUpdate = true;
                    pet.Title = (PetTittle)request.Title;
                }
            }
            if (request.Breed is not null)
            {
                if (request.Breed != pet.Breed)
                {
                    shouldUpdate = true;
                    pet.Breed = request.Breed;
                }
            }
            if (request.Age is not null)
            {
                if (request.Age != pet.Age)
                {
                    shouldUpdate = true;
                    pet.Age = (byte)request.Age;
                }
            }
            if (request.Sex is not null)
            {
                if (request.Sex != pet.Sex)
                {
                    shouldUpdate = true;
                    pet.Sex = (Gender)request.Sex;
                }
            }
            if (request.Microchiped is not null)
            {
                if (request.Microchiped != pet.Microchiped)
                {
                    shouldUpdate = true;
                    pet.Microchiped = (bool)request.Microchiped;
                }
            }
            if (request.Vaccination is not null)
            {
                if (request.Vaccination != pet.Vaccination)
                {
                    shouldUpdate = true;
                    pet.Vaccination = (bool)request.Vaccination;
                }
            }
            if (request.Sepcies is not null)
            {
                if (request.Sepcies != pet.Sepcies)
                {
                    shouldUpdate = true;
                    pet.Sepcies = request.Sepcies;
                }
            }
            if (request.LastVistDescription is not null)
            {
                if (request.LastVistDescription != pet.LastVistDescription)
                {
                    shouldUpdate = true;
                    pet.Sepcies = request.Sepcies;
                }
            }
            if (request.VaccineRecieved is not null)
            {
                if (request.VaccineRecieved != pet.VaccineRecieved)
                {
                    shouldUpdate = true;
                    pet.VaccineRecieved = request.VaccineRecieved;
                }
            }
            if (request.DueVaccine is not null)
            {
                if (request.DueVaccine != pet.Sepcies)
                {
                    shouldUpdate = true;
                    pet.Sepcies = request.Sepcies;
                }
            }
            if (request.Allergies is not null)
            {
                shouldUpdate = true;
                pet.Allergies = _conversionHelper.ConvertJObjectToJsonElement(request.Allergies);
            }
            if (request.Medications is not null)
            {
                shouldUpdate = true;
                pet.Medications = _conversionHelper.ConvertJObjectToJsonElement(request.Medications);
            }
            if (request.Details is not null)
            {
                shouldUpdate = true;
                pet.Details = _conversionHelper.ConvertJObjectToJsonElement(request.Details);
            }
            if (shouldUpdate)
            {
                await _repository.UpdateAsync(pet);
            }
            return shouldUpdate;
        }

    }
}
