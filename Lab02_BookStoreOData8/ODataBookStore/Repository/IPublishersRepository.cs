using ODataBookStore.DTOs;
using ODataBookStore.Models;

namespace ODataBookStore.Repository
{
    public interface IPublishersRepository
    {
        List<Publisher> GetAllPublisher();
        PublisherRespond AddPublisher(PublisherRespond publisherRespond);
        List<PublisherRespond> DeletePubliser(string key);

        PublisherRespond GetPublisherByID(string key);

        PublisherRespond UpdatePublisher(string key, PublisherRespond publisherRespond);
    }
}
