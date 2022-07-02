using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoRental.Models;
using VideoRental.ViewModels;
using System.Data.Entity;
using VideoRental.Utility;

namespace VideoRental.Controllers
{
    
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Customer
        public ActionResult Index()
        {
            //var customerList = _context.Customers.Include(c=>c.MemberShipType).ToList();
            //return View(customerList);
            if (User.IsInRole(SD.Role_Admin))
                return View("List");
            return View("ReadOnly");
            //return View();
        }

        [Authorize(Roles = SD.Role_Admin)]
        public ViewResult New()
        {
            CustomerFormVM customerFormVM = new CustomerFormVM();
            customerFormVM.Customer = new Customer();
            customerFormVM.MemberShipTypes = _context.MemberShipTypes.ToList();
            return View("CustomerForm", customerFormVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_Admin)]
        public ActionResult Save(Customer customer)
        {
            if (customer == null)
                return HttpNotFound();
            if (!ModelState.IsValid)
            {
                CustomerFormVM customerFormVM = new CustomerFormVM();
                customerFormVM.Customer = customer;
                customerFormVM.MemberShipTypes = _context.MemberShipTypes.ToList();
                return View("CustomerForm", customerFormVM);
            }

            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.FirstOrDefault(c => c.Id == customer.Id);
                if (customerInDb == null)
                    return HttpNotFound();
                customerInDb.Name = customer.Name;
                customerInDb.MemberShipTypeId = customer.MemberShipTypeId;
                customerInDb.DateOfBirth = customer.DateOfBirth;
                customerInDb.IsSubscribeToNewsLetter = customer.IsSubscribeToNewsLetter;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = SD.Role_Admin)]
        public ActionResult Edit(int id)
        {
            if (id == 0)
                return HttpNotFound();
            var customerInDb = _context.Customers.FirstOrDefault(c => c.Id == id);
            if (customerInDb == null)
                return HttpNotFound();
            CustomerFormVM customerFormVM = new CustomerFormVM();
            customerFormVM.Customer = customerInDb;
            customerFormVM.MemberShipTypes = _context.MemberShipTypes.ToList();
            return View("CustomerForm", customerFormVM);
        }

        [Authorize(Roles = SD.Role_Admin)]
        public ActionResult Details(int id)
        {
            var customerInDb = _context.Customers.Include(c=>c.MemberShipType).FirstOrDefault(c => c.Id == id);
            if (customerInDb == null)
                return HttpNotFound();
            return View(customerInDb);
        }

        [Authorize(Roles = SD.Role_Admin)]
        public ActionResult Delete(int id)
        {
            var customerInDb = _context.Customers.FirstOrDefault(c => c.Id == id);
            if (customerInDb == null)
                return HttpNotFound();

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}