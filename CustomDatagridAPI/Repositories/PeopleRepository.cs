using CustomDatagridAPI.Models;

namespace CustomDatagridAPI.Repositories
{
    public class PeopleRepository
    {
        private readonly MyContext context = new();

        internal List<Person> GetPeople(int page, int pageSize, string? search, string sort, string direction)
        {
            List<Person> people = context.TbPeople.ToList();
            people = direction == "ASC"
                ? people.OrderBy(x => x.GetType().GetProperty(sort).GetValue(x)).ToList()
                : people.OrderByDescending(x => x.GetType().GetProperty(sort).GetValue(x)).ToList();
            if (string.IsNullOrEmpty(search))
            {
                return people.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            List<Person> returnPeople = new();
            foreach (Person person in people.ToList())
            {
                if (person.GetType().GetProperties().Any(x => x.GetValue(person, null).ToString().Contains(search))) { returnPeople.Add(person); };
            }
            return returnPeople.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public int GetTotalPages(int pageSize, string? search)
        {
            int rows = 0;
            if (string.IsNullOrEmpty(search)) { rows = context.TbPeople.Count(); }
            else
            {
                List<Person> returnPeople = new();
                foreach (Person person in context.TbPeople.ToList())
                {
                    if (person.GetType().GetProperties().Any(x => x.GetValue(person, null).ToString().Contains(search))) { returnPeople.Add(person); };
                }
                rows = returnPeople.Count;
            }
            return (int)Math.Ceiling((double)(rows < 1 ? 1 : rows) / pageSize);
        }
    }
}
