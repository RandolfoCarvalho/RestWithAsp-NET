using RestWithAspNet.Data.VO;
using RestWithAspNet.Data.Converter.Implementations;
using RestWithAspNet.Model;
using RestWithAspNet.Repository.Generic;
using RestWithAspNet.Repository;
using RestWithAspNet.Hypermedia.Utils;


namespace RestWithAspNet.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {

        private readonly IPersonRepository _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public List<PersonVO> FindAll()
        {
            //por questões de legibilidade ficará "PersonVOs"
            return _converter.Parse(_repository.FindAll());
        }
        public PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page) 
        {
            var offset = page > 0 ? (page - 1) * pageSize : 0;
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = (pageSize < 1) ? 1 : pageSize;
            string query = @"
                    from Person p where p.name like '%LEO%'
                    order by 
                        p.name asc limite 10 offset 1";
            
            string countQuery = "";
            int totalResults = _repository.GetCount(countQuery);
            var persons = _repository.FindWithPagedSearch(query);

            return new PagedSearchVO<PersonVO> { 
                CurrentPage = offset,
                List = _converter.Parse(persons),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults,
            };

        }
        public PersonVO FindById(long id)
        {
            //logica para puxar do banco de dados
            return _converter.Parse( _repository.FindById(id));
        }
        public List<PersonVO> FindByName(string firstName, string secondName)
        {
            return _converter.Parse(_repository.FindByName(firstName, secondName));
        }
        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }
        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);

        }
        public PersonVO Disable(int id)
        {
            var personEntity = _repository.Disable(id);
            return _converter.Parse(personEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public PagedSearchVO<PersonVO> FindWithPagedSearch(string Name, string sortDirection, int pageSize, int page)
        {
            throw new NotImplementedException();
        }
    }
}
