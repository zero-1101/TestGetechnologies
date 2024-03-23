using TestGetechnologies.API.DataAccess;
using TestGetechnologies.API.DbConfig.Entities;

namespace TestGetechnologies.API.Business
{
    public class Directorio
    {
        private readonly IPersonaRepository _personaRepository;

        public Directorio(IPersonaRepository personaRepository)
        {
            this._personaRepository = personaRepository;
        }

        public int GetTotalRows() => _personaRepository.GetAll().Count();

        public List<Persona> FindPersonas(int? pageNumber)
        {
            var personas = _personaRepository.GetAll();

            if (pageNumber is null)
            {
                return personas.ToList();
            }
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            int pageSize = Constants.PageSize;
            personas = personas.Skip(pageSize * (pageNumber.Value - 1)).Take(pageSize);
            
            return personas.ToList();
        }

        public async Task<Persona?> GetById(int id)
        {
            Persona? persona = await _personaRepository.GetById(id);
            return persona;
        }

        public Persona? FindPersonaByIdentification(string identification)
        {
            Persona? persona = _personaRepository.GetAll().SingleOrDefault(p => p.Identificacion == identification);
            return persona;
        }

        public async Task<bool> DeletePersonaByIdentification(string identification)
        {
            Persona? persona = _personaRepository.GetAll().SingleOrDefault(p => p.Identificacion == identification);

            if (persona is null)
            {
                return false;
            }

            var result = await _personaRepository.Delete(persona.Id);
            return result;
        }

        public async Task<Persona?> CreatePersona(Persona persona)
        {
            Persona result = await _personaRepository.Create(persona);
            return result;
        }

        public async Task<bool> UpdatePersona(Persona persona)
        {
            Persona personaUpdate = await _personaRepository.GetById(persona.Id);

            if (personaUpdate is null)
            {
                return false;
            }

            personaUpdate.Nombre = persona.Nombre;
            personaUpdate.ApellidoPaterno = persona.ApellidoPaterno;
            personaUpdate.ApellidoMaterno = persona.ApellidoMaterno;
            personaUpdate.Identificacion = persona.Identificacion;

            _personaRepository.Update(personaUpdate);

            return true;
        }
    }
}
