using TestGetechnologies.API.DataAccess;
using TestGetechnologies.API.DbConfig.Entities;

namespace TestGetechnologies.API.Business
{
    public class Directorio
    {
        private readonly PersonaRepository _personaRepository;

        public Directorio(PersonaRepository personaRepository)
        {
            this._personaRepository = personaRepository;
        }

        public List<Persona> FindPersonas()
        {
            var personas = _personaRepository.GetAll().ToList();
            return personas;
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
            if (persona is null)
            {
                return null;
            }

            Persona result = await _personaRepository.Create(persona);
            return result;
        }

        public void UpdatePersona(Persona persona)
        {
            _personaRepository.Update(persona);
        }
    }
}
