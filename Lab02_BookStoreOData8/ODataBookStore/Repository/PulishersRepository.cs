using AutoMapper;
using ODataBookStore.DTOs;
using ODataBookStore.Models;

namespace ODataBookStore.Repository
{
    public class PulishersRepository : IPublishersRepository
    {
        private readonly As2Context _context;
        private readonly IMapper mapper;

        public PulishersRepository(As2Context context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        public PublisherRespond AddPublisher(PublisherRespond publisherRespond)
        {
            Publisher publisher = mapper.Map<Publisher>(publisherRespond);
            _context.Publishers.Add(publisher);
            _context.SaveChanges();
            PublisherRespond publisherRespond1 = mapper.Map<PublisherRespond>(publisher);
            return publisherRespond1;
        }

        public List<PublisherRespond> DeletePubliser(string key)
        {
            
            var publisher = _context.Publishers.FirstOrDefault(b => b.PubId == key.Trim());
            if (publisher != null)
            {
                _context.Remove(publisher);
                _context.SaveChanges();
                List<Publisher> publishers = _context.Publishers.ToList();
                List<PublisherRespond> publisherResponds = mapper.Map<List<PublisherRespond>>(publishers);
                return publisherResponds;
            }
            return null;
        }

        public List<Publisher> GetAllPublisher()
        {
            var publisherList = _context.Publishers.ToList();
            return publisherList;
        }

        public PublisherRespond GetPublisherByID(string key)
        {
            
            var publisher = _context.Publishers.FirstOrDefault(b => b.PubId == key.Trim());
            if (publisher != null)
            {
                PublisherRespond publisherRespond = mapper.Map<PublisherRespond>(publisher);
                return publisherRespond;
            }
            return null;
        }

        public PublisherRespond UpdatePublisher(string key, PublisherRespond publisherRespond)
        {
            
            var publisher = _context.Publishers.FirstOrDefault(b => b.PubId == key.Trim());
            if (publisher != null)
            {
                publisher.PublisherName = publisherRespond.PublisherName;
                publisher.City = publisherRespond.City;             
                publisher.State = publisherRespond.State;
                publisher.Country = publisherRespond.Country;
                _context.Update(publisher);
                _context.SaveChanges();
                PublisherRespond publisherRespond1 = mapper.Map<PublisherRespond>(publisher);
                return publisherRespond1;
            }
            return null;
        }
    }
}
