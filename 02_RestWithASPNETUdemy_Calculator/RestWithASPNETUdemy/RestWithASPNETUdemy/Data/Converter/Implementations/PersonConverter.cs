using RestWithASPNETUdemy.Data.Converter.Contract;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Data.Converter.Implementations
{
    public class PersonConverter : IParser<PersonVO, Person>, IParser<Person, PersonVO>
    {
        public Person Parse(PersonVO origem)
        {
            if (origem == null) { return null; }

            return new Person
            {
                Id = origem.Id,
                FirstName = origem.FirstName,
                Address = origem.Address,
                Genre = origem.Genre,
                LastName = origem.LastName
            };
        }

        public PersonVO Parse(Person origem)
        {
            if (origem == null){ return null; }

            return new PersonVO
            {
                Id = origem.Id,
                FirstName = origem.FirstName,
                Address = origem.Address,
                Genre = origem.Genre,
                LastName = origem.LastName
            };
        }

        public List<PersonVO> Parse(List<Person> origem)
        {
            if (origem == null) { return null; }

            return origem.Select(item => Parse(item)).ToList();
        }

        public List<Person> Parse(List<PersonVO> origem)
        {
            return origem.Select(item => Parse(item)).ToList();
        }
    }
}
