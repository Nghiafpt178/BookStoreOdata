using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ODataBookStore.DTOs;
using ODataBookStore.Models;
using ODataBookStore.Repository;
using ODataBookStore.Services;

namespace ODataBookStore.Controllers
{

    public class PublishersController : ODataController
    {
        private readonly IPublishersRepository publishersRepository;

        public PublishersController(IPublishersRepository publishersRepository)
        {
            this.publishersRepository = publishersRepository;
        }


        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            List<Publisher> publishers = publishersRepository.GetAllPublisher();
            return Ok(publishers);
        }

        [EnableQuery]
        public IActionResult Post([FromBody] PublisherRespond publisher)
        {
            PublisherRespond publisherRespond = publishersRepository.AddPublisher(publisher);
            return Ok(new Respond<PublisherRespond>()
            {
                IsSuccess = true,
                Message = "Create new book success!",
                Data = publisherRespond
            });
        }

        [EnableQuery]
        [HttpDelete]
        public IActionResult Delete([FromODataUri] string key)
        {
            List<PublisherRespond> deletedPublisher = publishersRepository.DeletePubliser(key);
            if (deletedPublisher == null)
            {
                return NotFound();
            }
            return Ok(new Respond<List<PublisherRespond>>()
            {
                IsSuccess = true,
                Message = "Delete book success!",
                Data = deletedPublisher
            });

        }

        [EnableQuery]
        public IActionResult Get(string key)
        {
            PublisherRespond publisherRespond = publishersRepository.GetPublisherByID(key);
            return Ok(publisherRespond);
        }

        [EnableQuery]
        [HttpPut]
        public IActionResult Put([FromODataUri] string key, [FromBody] PublisherRespond publisherRespond)
        {
            PublisherRespond publisherRespond1 = publishersRepository.UpdatePublisher(key, publisherRespond);
            if (publisherRespond1 == null)
            {
                return NotFound();
            }
            return Ok(new Respond<PublisherRespond>()
            {
                IsSuccess = true,
                Message = "Update book success!",
                Data = publisherRespond1
            });
        }
    }
}
