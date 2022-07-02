using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VideoRental.Dtos;

namespace VideoRental.Models
{
    public class CustomerDto
    {
        public int Id { get; set; }       
        public string Name { get; set; }    
        public byte MemberShipTypeId { get; set; }
        public MemberShipTypeDto MemberShipType { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsSubscribeToNewsLetter { get; set; }
    }
}