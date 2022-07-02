using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VideoRental.Models;
using System.Data.Entity;

namespace VideoRental.Controllers.API
{
    public class CustomersController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET /api/Customers
        [HttpGet]
        public IHttpActionResult GetAllCustomers(string query = null)
        {
            //var customerListDto = _context.Customers.Include(c => c.MemberShipType).ToList().Select(Mapper.Map<Customer, CustomerDto>);

            var customerQuery = _context.Customers.Include(c => c.MemberShipType);
            if (!string.IsNullOrWhiteSpace(query))
                customerQuery = customerQuery.Where(c => c.Name.Contains(query));

            var customerListDto = customerQuery.ToList().Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customerListDto); //200
        }


        //Get /api/Customers/1
        [HttpGet]
        public IHttpActionResult GetCustomer(int Id)
        {
            var customerFromDb = _context.Customers.FirstOrDefault(c => c.Id == Id);
            if (customerFromDb == null)
                return NotFound();
            return Ok(Mapper.Map<Customer, CustomerDto>(customerFromDb));//200
        }

        //POST /api/Customers - /api/Customers/1
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid || customerDto == null)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();
            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" + customerDto.Id), customerDto); //201
        }

        //PUT /api/Customrs/1
        [HttpPut]
        public void UpdateCustomer(int Id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            if (Id != customerDto.Id || customerDto == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerFromDb = _context.Customers.FirstOrDefault(c => c.Id == Id);

            if (customerFromDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(customerDto, customerFromDb);

            //customerFromDb.Name = customer.Name;
            //customerFromDb.MemberShipTypeId = customer.MemberShipTypeId;
            //customerFromDb.DateOfBirth = customer.DateOfBirth;
            //customerFromDb.IsSubscribeToNewsLetter = customer.IsSubscribeToNewsLetter;
            _context.SaveChanges();

        }

        [HttpDelete]
        public void DeleteCustomer(int Id)
        {
            var customerFromDb = _context.Customers.FirstOrDefault(c => c.Id == Id);

            if (customerFromDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(customerFromDb);
            _context.SaveChanges();
        }
    }
}
