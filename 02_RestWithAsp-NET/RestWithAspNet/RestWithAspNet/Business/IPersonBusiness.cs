using RestWithAspNet.Data.VO;
using RestWithAspNet.Hypermedia.Utils;
using RestWithAspNet.Model;

namespace RestWithAspNet.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        PersonVO Update(PersonVO person);
        PersonVO FindById(long id);
        List<PersonVO> FindByName(string firstName, string secondName);
        PagedSearchVO<PersonVO> FindWithPagedSearch(string Name, string sortDirection, int pageSize, int page);
        List<PersonVO> FindAll();
        PersonVO Disable(int id);
        void Delete(long id);
    }
}
